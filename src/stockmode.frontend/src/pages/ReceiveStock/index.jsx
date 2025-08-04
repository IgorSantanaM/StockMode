import React, { useState, useMemo, useEffect } from 'react';
import { Search, Trash2 } from 'lucide-react';
import {
  PageContainer, LeftPanel, RightPanel, SearchContainer, Input,
  SearchResultsContainer, ProductCard, ProductImage, ProductInfo, ProductName, ProductSku,
  SectionTitle, ReceivedItemsList, ReceivedItemCard, QuantityInput, FinalizeButton
} from './styles';

// --- DADOS MOCK (Numa aplicação real, viriam da sua API) ---
const searchableProducts = [
  { id: 1, name: 'Camiseta Gola V - Branca M', sku: 'CGV-BR-M', stock: 50, image: 'https://placehold.co/100x100/FFFFFF/333333?text=Camiseta' },
  { id: 2, name: 'Calça Jeans Skinny - 42', sku: 'CJS-AZ-42', stock: 25, image: 'https://placehold.co/100x100/336699/FFFFFF?text=Calça' },
  { id: 3, name: 'Vestido Floral - P', sku: 'VF-EA-P', stock: 15, image: 'https://placehold.co/100x100/FFCCCC/333333?text=Vestido' },
  { id: 4, name: 'Moletom com Capuz - Cinza G', sku: 'MCC-CZ-G', stock: 30, image: 'https://placehold.co/100x100/CCCCCC/FFFFFF?text=Moletom' },
];

const ReceiveStock = () => {
  const [searchTerm, setSearchTerm] = useState('');
  const [searchResults, setSearchResults] = useState([]);
  const [receivedItems, setReceivedItems] = useState([]);

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

  const addToReceipt = (product) => {
    const existingItem = receivedItems.find(item => item.id === product.id);
    if (!existingItem) {
      setReceivedItems([...receivedItems, { ...product, quantityReceived: 1 }]);
    }
    // Opcional: focar no campo de quantidade do item adicionado
  };

  const updateQuantity = (productId, newQuantity) => {
    const quantity = parseInt(newQuantity, 10);
    setReceivedItems(receivedItems.map(item => 
      item.id === productId ? { ...item, quantityReceived: isNaN(quantity) ? 0 : quantity } : item
    ));
  };

  const removeFromReceipt = (productId) => {
    setReceivedItems(receivedItems.filter(item => item.id !== productId));
  };
  
  const totalItems = useMemo(() => {
    return receivedItems.reduce((acc, item) => acc + (item.quantityReceived || 0), 0);
  }, [receivedItems]);

  const handleSubmit = () => {
    // Lógica para enviar os dados para a API (ReceivePurchaseOrderCommand)
    const payload = {
        items: receivedItems.map(item => ({
            variationId: item.id,
            quantityReceived: item.quantityReceived
        }))
    };
    console.log("Enviando para a API:", payload);
    alert(`Recebimento de ${totalItems} itens finalizado!`);
    // Limpar o formulário após o envio
    setReceivedItems([]);
  };

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
            <ProductCard key={product.id} onClick={() => addToReceipt(product)}>
              <ProductImage src={product.image} alt={product.name} />
              <ProductInfo>
                <ProductName>{product.name}</ProductName>
                <ProductSku>SKU: {product.sku} | Estoque Atual: {product.stock}</ProductSku>
              </ProductInfo>
            </ProductCard>
          ))}
        </SearchResultsContainer>
      </LeftPanel>

      <RightPanel>
        <SectionTitle>Itens a Receber</SectionTitle>
        <ReceivedItemsList>
          {receivedItems.length === 0 ? (
            <p style={{ textAlign: 'center', color: '#6b7280', marginTop: '2rem' }}>Selecione os produtos para dar entrada no estoque.</p>
          ) : (
            receivedItems.map(item => (
              <ReceivedItemCard key={item.id}>
                <ProductImage src={item.image} alt={item.name} />
                <ProductInfo>
                  <ProductName>{item.name}</ProductName>
                  <ProductSku>SKU: {item.sku}</ProductSku>
                </ProductInfo>
                <QuantityInput
                  type="number"
                  value={item.quantityReceived}
                  onChange={(e) => updateQuantity(item.id, e.target.value)}
                  min="1"
                />
                <button onClick={() => removeFromReceipt(item.id)} style={{background: 'none', border: 'none', color: '#9ca3af', cursor: 'pointer'}}><Trash2 size={18} /></button>
              </ReceivedItemCard>
            ))
          )}
        </ReceivedItemsList>
        <div style={{marginTop: 'auto'}}>
            <div style={{paddingTop: '1.5rem', borderTop: '1px solid #e5e7eb', display: 'flex', justifyContent: 'space-between', alignItems: 'center', fontWeight: 'bold', fontSize: '1.125rem'}}>
                <span>Total de Itens</span>
                <span>{totalItems}</span>
            </div>
            <FinalizeButton disabled={receivedItems.length === 0 || receivedItems.some(i => i.quantityReceived <= 0)} onClick={handleSubmit}>
                Finalizar Recebimento
            </FinalizeButton>
        </div>
      </RightPanel>
    </PageContainer>
  );
};

export default ReceiveStock;
