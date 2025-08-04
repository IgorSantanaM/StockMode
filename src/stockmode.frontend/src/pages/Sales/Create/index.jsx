import React, { useState, useMemo, useEffect } from 'react';
import { Search, Plus, Minus, Trash2 } from 'lucide-react';
import {
  PageContainer, LeftPanel, RightPanel, SearchContainer, Input,
  SearchResultsContainer, ProductCard, ProductImage, ProductInfo, ProductName, ProductSku, ProductPrice,
  SectionTitle, SaleItemsList, SaleItemCard, QuantityControl, QuantityButton,
  SummaryContainer, SummaryRow, TotalRow, FinalizeButton, Select
} from './styles';

// --- DADOS MOCK (Em uma aplicação real, viriam da sua API) ---
const searchableProducts = [
  { id: 1, name: 'Camiseta Gola V - Branca M', sku: 'CGV-BR-M', price: 39.90, stock: 50, image: 'https://placehold.co/100x100/FFFFFF/333333?text=Camiseta' },
  { id: 2, name: 'Calça Jeans Skinny - 42', sku: 'CJS-AZ-42', price: 129.90, stock: 25, image: 'https://placehold.co/100x100/336699/FFFFFF?text=Calça' },
  { id: 3, name: 'Vestido Floral - P', sku: 'VF-EA-P', price: 159.90, stock: 15, image: 'https://placehold.co/100x100/FFCCCC/333333?text=Vestido' },
  { id: 4, name: 'Moletom com Capuz - Cinza G', sku: 'MCC-CZ-G', price: 199.90, stock: 30, image: 'https://placehold.co/100x100/CCCCCC/FFFFFF?text=Moletom' },
];

const NewSale = () => {
  const [searchTerm, setSearchTerm] = useState('');
  const [searchResults, setSearchResults] = useState([]);
  const [saleItems, setSaleItems] = useState([]);
  const [discount, setDiscount] = useState(0);
  const [paymentMethod, setPaymentMethod] = useState('Cartão de Crédito');

  useEffect(() => {
    if (searchTerm.trim() === '') {
      setSearchResults([]);
      return;
    }
    const filtered = searchableProducts.filter(p => 
      p.name.toLowerCase().includes(searchTerm.toLowerCase()) ||
      p.sku.toLowerCase().includes(searchTerm.toLowerCase())
    );
    setSearchResults(filtered);
  }, [searchTerm]);

  const addToSale = (product) => {
    const existingItem = saleItems.find(item => item.id === product.id);
    if (existingItem) {
      updateQuantity(product.id, existingItem.quantity + 1);
    } else {
      setSaleItems([...saleItems, { ...product, quantity: 1 }]);
    }
  };

  const updateQuantity = (productId, newQuantity) => {
    if (newQuantity === 0) {
      removeFromSale(productId);
      return;
    }
    setSaleItems(saleItems.map(item => 
      item.id === productId ? { ...item, quantity: newQuantity } : item
    ));
  };

  const removeFromSale = (productId) => {
    setSaleItems(saleItems.filter(item => item.id !== productId));
  };

  const subtotal = useMemo(() => {
    return saleItems.reduce((acc, item) => acc + (item.price * item.quantity), 0);
  }, [saleItems]);

  const finalPrice = useMemo(() => {
    const total = subtotal - discount;
    return total > 0 ? total : 0;
  }, [subtotal, discount]);

  const formatCurrency = (value) => value.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });

  return (
    <PageContainer>
      <LeftPanel>
        <SearchContainer>
          <Search style={{ position: 'absolute', left: '1rem', top: '50%', transform: 'translateY(-50%)', color: '#9ca3af' }} />
          <Input 
            type="text" 
            placeholder="Buscar produto por nome ou SKU..." 
            value={searchTerm}
            onChange={(e) => setSearchTerm(e.target.value)}
          />
        </SearchContainer>
        <SearchResultsContainer>
          {searchResults.map(product => (
            <ProductCard key={product.id} onClick={() => addToSale(product)}>
              <ProductImage src={product.image} alt={product.name} />
              <ProductInfo>
                <ProductName>{product.name}</ProductName>
                <ProductSku>SKU: {product.sku} | Estoque: {product.stock}</ProductSku>
              </ProductInfo>
              <ProductPrice>{formatCurrency(product.price)}</ProductPrice>
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
                  <QuantityButton onClick={() => updateQuantity(item.id, item.quantity - 1)}><Minus size={16} /></QuantityButton>
                  <span>{item.quantity}</span>
                  <QuantityButton onClick={() => updateQuantity(item.id, item.quantity + 1)}><Plus size={16} /></QuantityButton>
                </QuantityControl>
                <ProductInfo>
                  <ProductName>{item.name}</ProductName>
                  <ProductPrice>{formatCurrency(item.price)}</ProductPrice>
                </ProductInfo>
                <p style={{ fontWeight: 'bold', minWidth: '80px', textAlign: 'right' }}>{formatCurrency(item.price * item.quantity)}</p>
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
