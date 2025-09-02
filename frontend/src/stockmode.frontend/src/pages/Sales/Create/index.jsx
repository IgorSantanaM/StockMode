import React, { useState, useMemo, useEffect } from 'react';
import { Search, Plus, Minus, Trash2 } from 'lucide-react';
import {
  PageContainer, LeftPanel, RightPanel, SearchContainer, Input,
  SearchResultsContainer, ProductCard, ProductImage, ProductInfo, ProductName, ProductSku, ProductPrice,
  SectionTitle, SaleItemsList, SaleItemCard, QuantityControl, QuantityButton,
  SummaryContainer, SummaryRow, TotalRow, FinalizeButton, Select
} from './styles';
import api from '../../../services/api';
import { toast } from 'react-toastify';

const ITEMS_PER_PAGE = 3;

const NewSale = () => {
  const [searchTerm, setSearchTerm] = useState('');
  const [isLoading, setIsLoading] = useState(true);
  const [ customerIsLoading, setCustomerIsLoading] = useState(true);
  const [ customerSearchTerm, setCustomerSearchTerm] = useState('');
  const [ customers, setCustomers] = useState([]);
  const [variations, setVariations] = useState([]);

  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(0);

  const [saleItems, setSaleItems] = useState([]);
  const [discount, setDiscount] = useState(0);
  const [paymentMethod, setPaymentMethod] = useState('Cartão de Crédito');

  useEffect(() => {
    setCurrentPage(1);
  }, [searchTerm])

  useEffect(() => {
    const fetchVariations = async () => {
      setIsLoading(true);
      try{
        const params = new URLSearchParams();
        if(searchTerm) params.append('name', searchTerm);
        params.append('page', currentPage);
        params.append('pageSize', ITEMS_PER_PAGE);
        const response = await api.get(`/products/variations?${params.toString()}`)

        setVariations(response.data.items || []);
        setTotalPages(response.data.totalPages);
      }catch(error){
        console.error("Failed to fetch variations: ", error);
        toast.error("Falha ao buscar variações.");
        setVariations([]); 
        setTotalPages(0);
      }
      finally{
        setIsLoading(false);
      }
    } 
    const handler = setTimeout(() => {
      fetchVariations();
    }, 500);

  return () => {
      clearTimeout(handler);
    };
  }, [searchTerm, currentPage]);

  useEffect(() =>{
    const fetchCustomers = async () => {
      setCustomerIsLoading(true);
      try{
        const params = new URLSearchParams();
        if(customerSearchTerm) params.append('name', customerSearchTerm);
        params.append('page', currentPage);
        params.append('pageSize', ITEMS_PER_PAGE);

        console.log("bateu!");
        const response = await api.get(`/customers?${params.toString()}`);
        setCustomers(response.data.items);
        setTotalPages(response.data.totalPages);
      }catch(error){
        console.error("Failed to fetch customers: ", error);
        toast.error("Falha ao buscar clientes.");
        setVariations([]); 
        setTotalPages(0);
      }finally{
        setCustomerIsLoading(false);
      }
    } 
    const handler = setTimeout(() => {
      fetchCustomers();
    }, 500);

  return () => {
      clearTimeout(handler);
    };
  }, [customerSearchTerm, currentPage]);


  const updateQuantity = (variationId, newQuantity) => {
    if (newQuantity === 0) {
      removeFromSale(variationId);
      return;
    }
    setSaleItems(saleItems.map(item => 
      item.id === variationId ? { ...item, quantity: newQuantity } : item
    ));
  };

  const addToSale = (variation) => {
    const existingItem = saleItems.find(item => item.id === variation.id);

    if (existingItem) {
      updateQuantity(variation.id, existingItem.quantity + 1);
    } else {
      setSaleItems([...saleItems, { ...variation, quantity: 1 }]);
    }
  };

  const removeFromSale = (variationId) => {
    setSaleItems(saleItems.filter(item => item.id !== variationId));
  };

  const subtotal = useMemo(() => {
    return saleItems.reduce((acc, item) => acc + (item.salePrice * item.quantity), 0);
  }, [saleItems]);

  const finalPrice = useMemo(() => {
    const total = subtotal - discount;
    return total > 0 ? total : 0;
  }, [subtotal, discount]);

  const formatCurrency = (value) => {
    if (typeof value !== 'number' || isNaN(value)) {
      return (0).toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });
    }
    return value.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });
  };

  return (
    <PageContainer>
        <LeftPanel>
          <SearchContainer>
            <Search style={{ position: 'absolute', left: '1rem', top: '50%', transform: 'translateY(-50%)', color: '#9ca3af' }} />
            <Input 
              type="text" 
              placeholder="Buscar variação de produto por nome..." 
              value={searchTerm}
              onChange={(e) => setSearchTerm(e.target.value)}
            />
          </SearchContainer>
          <SearchResultsContainer>
            { totalPages > 0 && !isLoading && variations.map((variation) => (
              <ProductCard key={variation.id} onClick={() => addToSale(variation)}>
                {/* <ProductImage src={variation.image} alt={variation.name} /> */}
                <ProductInfo>
                  <ProductName>{variation.name}</ProductName>
                  <ProductSku>SKU: {variation.sku} | Estoque: {variation.stockQuantity}</ProductSku>
                </ProductInfo>
                <ProductPrice>{formatCurrency(variation.salePrice)}</ProductPrice>
              </ProductCard>
            ))}
          </SearchResultsContainer>

          <SearchContainer>
            <Search style={{ position: 'absolute', left: '1rem', top: '50%', transform: 'translateY(-50%)', color: '#9ca3af' }} />
            <Input 
              type="text" 
              placeholder="Buscar clientes cadastrados por nome..." 
              value={searchTerm}
              onChange={(e) => setCustomerSearchTerm(e.target.value)}
            />
          </SearchContainer>
          <SearchResultsContainer>
            { totalPages > 0 && !isLoading && customers.map((customer) => (
              <ProductCard key={customer.id} onClick={() => addToSale(customer)}>
                {/* <ProductImage src={variation.image} alt={variation.name} /> */}
                <ProductInfo>
                  <ProductName>{customer.name}</ProductName>
                  <ProductSku>Email: {customer.email} | Telefone: {customer.phoneNumber} </ProductSku>
                </ProductInfo>
              </ProductCard>
            ))}
          </SearchResultsContainer>
        </LeftPanel>

      <RightPanel>
        <SectionTitle>Resumo da Venda</SectionTitle>
        <SaleItemsList>
          {saleItems.length === 0 ? (
            <p style={{ textAlign: 'center', color: '#6b7280', marginTop: '2rem' }}>Nenhum item na venda.</p>
          ) : (
            saleItems.map(item => (
              <SaleItemCard key={item.id}>
                <QuantityControl>
                  <QuantityButton onClick={() => updateQuantity(item.id, item.quantity - 1)}>-</QuantityButton>
                  <span>{item.quantity}</span>
                  <QuantityButton onClick={() => updateQuantity(item.id, item.quantity + 1)}>+</QuantityButton>
                </QuantityControl>
                <ProductInfo>
                  <ProductName>{item.name}</ProductName>
                  <ProductPrice>{formatCurrency(item.salePrice)}</ProductPrice>
                </ProductInfo>
                <p style={{ fontWeight: 'bold', minWidth: '80px', textAlign: 'right' }}>{formatCurrency(item.salePrice * item.quantity)}</p>
                <button onClick={() => removeFromSale(item.id)} style={{background: 'none', border: 'none', color: '#9ca3af', marginLeft: '1rem', cursor: 'pointer'}}><Trash2 size={18} /></button>
              </SaleItemCard>
            ))
          )}
        </SaleItemsList>
        <SummaryContainer>
          <SummaryRow>
            <span>Subtotal</span>
            <span>{formatCurrency(subtotal)}</span>
          </SummaryRow>
          <SummaryRow>
            <label htmlFor="discount">Desconto (R$)</label>
            <Input 
              id="discount"
              type="number" 
              step="0.01"
              value={discount}
              onChange={(e) => setDiscount(parseFloat(e.target.value) || 0)}
              style={{ width: '100px', padding: '0.25rem 0.5rem', textAlign: 'right' }} 
            />
          </SummaryRow>
          <SummaryRow>
            <label htmlFor="paymentMethod">Método de Pagamento</label>
            <Select 
              id="paymentMethod"
              value={paymentMethod}
              onChange={(e) => setPaymentMethod(e.target.value)}
            >
              <option>Cartão de Crédito</option>
              <option>Cartão de Débito</option>
              <option>PIX</option>
              <option>Dinheiro</option>
            </Select>
          </SummaryRow>
          <TotalRow>
            <span>Total</span>
            <span>{formatCurrency(finalPrice)}</span>
          </TotalRow>
          <FinalizeButton disabled={saleItems.length === 0}>
            Finalizar Venda
          </FinalizeButton>
        </SummaryContainer>
      </RightPanel>
    </PageContainer>
  );
};

export default NewSale;
