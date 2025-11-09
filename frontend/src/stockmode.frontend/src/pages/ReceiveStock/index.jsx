import React, { useState, useMemo, useEffect } from 'react';
import { Search, Trash2 } from 'lucide-react';
import {
  PageContainer, LeftPanel, RightPanel, SearchContainer, Input,
  SearchResultsContainer, ProductCard, ProductImage, ProductInfo, ProductName, ProductSku,
  SectionTitle, ReceivedItemsList, ReceivedItemCard, QuantityInput, FinalizeButton
} from './styles';
import api from '../../services/api';
import { LoadingContainer } from '../../util/LoadingContainer';

const ReceiveStock = () => {
  const [searchTerm, setSearchTerm] = useState('');
  const [searchResults, setSearchResults] = useState([]);
  const [receivedItems, setReceivedItems] = useState([]);
  const [allVariations, setAllVariations] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [submitting, setSubmitting] = useState(false);

  useEffect(() => {
    fetchProducts();
  }, []);

  const fetchProducts = async () => {
    try {
      setLoading(true);
      setError(null);

      const response = await api.get('/products', {
        params: {
          page: 1,
          pageSize: 1000
        }
      });

      const products = response.data?.items || response.data || [];
      
      // Flatten products and their variations
      const variations = [];
      products.forEach(product => {
        if (product.variations && product.variations.length > 0) {
          product.variations.forEach(variation => {
            variations.push({
              id: variation.id,
              productId: product.id,
              name: `${product.name} - ${variation.name}`,
              productName: product.name,
              variationName: variation.name,
              sku: variation.sku || 'N/A',
              stock: variation.stockQuantity || 0,
              image: product.imageUrl || 'https://placehold.co/100x100/E5E7EB/6B7280?text=Produto'
            });
          });
        }
      });

      setAllVariations(variations);
    } catch (err) {
      console.error('Error fetching products:', err);
      setError('Erro ao carregar produtos. Por favor, tente novamente.');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    if (searchTerm.trim() === '') {
      setSearchResults([]);
      return;
    }
    const filtered = allVariations.filter(p => 
      p.name.toLowerCase().includes(searchTerm.toLowerCase()) ||
      p.sku.toLowerCase().includes(searchTerm.toLowerCase())
    );
    setSearchResults(filtered);
  }, [searchTerm, allVariations]);

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

  const handleSubmit = async () => {
    try {
      setSubmitting(true);
      
      const payload = {
        items: receivedItems.map(item => ({
          variationId: item.id,
          quantityReceived: item.quantityReceived
        }))
      };

      await api.post('/purchaseorders/receive', payload);
      
      alert(`Recebimento de ${totalItems} itens finalizado com sucesso!`);
      
      // Clear form and refresh products
      setReceivedItems([]);
      setSearchTerm('');
      await fetchProducts();
      
    } catch (err) {
      console.error('Error submitting purchase order:', err);
      alert('Erro ao finalizar recebimento. Por favor, tente novamente.');
    } finally {
      setSubmitting(false);
    }
  };

  if (loading) {
    return <LoadingContainer>Carregando produtos...</LoadingContainer>;
  }

  if (error) {
    return (
      <PageContainer>
        <div style={{ textAlign: 'center', padding: '2rem', color: '#ef4444' }}>
          <p>{error}</p>
          <button 
            onClick={fetchProducts} 
            style={{ 
              marginTop: '1rem', 
              padding: '0.5rem 1rem', 
              cursor: 'pointer',
              backgroundColor: '#4f46e5',
              color: 'white',
              border: 'none',
              borderRadius: '0.5rem'
            }}
          >
            Tentar Novamente
          </button>
        </div>
      </PageContainer>
    );
  }

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
          {searchTerm.trim() !== '' && searchResults.length === 0 && (
            <p style={{ textAlign: 'center', color: '#6b7280', marginTop: '2rem', padding: '1rem' }}>
              Nenhum produto encontrado.
            </p>
          )}
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
            <FinalizeButton 
              disabled={receivedItems.length === 0 || receivedItems.some(i => i.quantityReceived <= 0) || submitting} 
              onClick={handleSubmit}
            >
                {submitting ? 'Processando...' : 'Finalizar Recebimento'}
            </FinalizeButton>
        </div>
      </RightPanel>
    </PageContainer>
  );
};

export default ReceiveStock;
