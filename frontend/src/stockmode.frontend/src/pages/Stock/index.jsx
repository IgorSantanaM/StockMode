import React, { useState, useMemo, useEffect } from 'react';
import { Layers, PlusCircle, Edit } from 'lucide-react';
import {
  PageContainer,
  PageHeader,
  TitleContainer,
  Title,
  PrimaryButton,
  Card,
  FilterContainer,
  Input,
  Select,
  Table,
  Th,
  Td,
  Tr,
  StockStatusBadge,
  ActionButton,
} from './styles';
import { useNavigate } from 'react-router-dom';
import api from '../../services/api';
import { LoadingContainer } from '../../util/LoadingContainer';

const Stock = () => {
  const [searchTerm, setSearchTerm] = useState('');
  const [statusFilter, setStatusFilter] = useState('Todos');
  const [allVariations, setAllVariations] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const navigate = useNavigate();

  useEffect(() => {
    fetchStockData();
  }, []);

  const fetchStockData = async () => {
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
      
      // Flatten products and their variations into a single array
      const variations = [];
      products.forEach(product => {
        if (product.variations && product.variations.length > 0) {
          product.variations.forEach(variation => {
            variations.push({
              id: variation.id,
              productId: product.id,
              productName: product.name,
              variationName: variation.name,
              sku: variation.sku || 'N/A',
              costPrice: variation.costPrice || 0,
              salePrice: variation.salePrice || 0,
              stockQuantity: variation.stockQuantity || 0
            });
          });
        }
      });

      setAllVariations(variations);
    } catch (err) {
      console.error('Error fetching stock data:', err);
      setError('Erro ao carregar dados de estoque. Por favor, tente novamente.');
    } finally {
      setLoading(false);
    }
  };

  const getStatus = (stock) => {
    if (stock === 0) return 'Esgotado';
    if (stock <= 10) return 'Baixo Estoque';
    return 'Em Estoque';
  };

  const filteredVariations = useMemo(() => {
    return allVariations.filter(variation => {
      const searchMatch = 
        variation.productName.toLowerCase().includes(searchTerm.toLowerCase()) ||
        variation.variationName.toLowerCase().includes(searchTerm.toLowerCase()) ||
        variation.sku.toLowerCase().includes(searchTerm.toLowerCase());
      
      const statusMatch = statusFilter === 'Todos' || getStatus(variation.stockQuantity) === statusFilter;
      
      return searchMatch && statusMatch;
    });
  }, [searchTerm, statusFilter]);

  const formatCurrency = (value) => {
    return value.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });
  };

  if (loading) {
    return <LoadingContainer>Carregando estoque...</LoadingContainer>;
  }

  if (error) {
    return (
      <PageContainer>
        <PageHeader>
          <TitleContainer>
            <Layers size={32} color="#4f46e5" />
            <Title>Controle de Estoque</Title>
          </TitleContainer>
        </PageHeader>
        <Card>
          <div style={{ textAlign: 'center', padding: '2rem', color: '#ef4444' }}>
            <p>{error}</p>
            <button 
              onClick={fetchStockData} 
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
        </Card>
      </PageContainer>
    );
  }

  return (
    <PageContainer>
      <PageHeader>
        <TitleContainer>
          <Layers size={32} color="#4f46e5" />
          <Title>Controle de Estoque</Title>
        </TitleContainer>
        <PrimaryButton onClick={() => navigate("/ReceiveStock")}>
          <PlusCircle size={20} />
          Receber Mercadoria
        </PrimaryButton>
      </PageHeader>

      <Card>
        <FilterContainer>
          <Input 
            type="text" 
            placeholder="Buscar por produto, variação ou SKU..." 
            value={searchTerm}
            onChange={(e) => setSearchTerm(e.target.value)}
            style={{flexGrow: 1}}
          />
          <Select value={statusFilter} onChange={(e) => setStatusFilter(e.target.value)}>
            <option value="Todos">Todos os Status</option>
            <option value="Em Estoque">Em Estoque</option>
            <option value="Baixo Estoque">Baixo Estoque</option>
            <option value="Esgotado">Esgotado</option>
          </Select>
        </FilterContainer>

        <Table>
          <thead>
            <tr>
              <Th>Item</Th>
              <Th>SKU</Th>
              <Th>Preço de Custo</Th>
              <Th>Preço de Venda</Th>
              <Th>Quantidade</Th>
              <Th>Status</Th>
              <Th>Ações</Th>
            </tr>
          </thead>
          <tbody>
            {filteredVariations.map((variation) => (
              <Tr key={variation.id}>
                <Td>
                  <p style={{ fontWeight: 500 }}>{variation.productName}</p>
                  <p style={{ fontSize: '0.8rem', color: '#6b7280' }}>{variation.variationName}</p>
                </Td>
                <Td style={{ color: '#6b7280' }}>{variation.sku}</Td>
                <Td>{formatCurrency(variation.costPrice)}</Td>
                <Td style={{ fontWeight: 500 }}>{formatCurrency(variation.salePrice)}</Td>
                <Td style={{ fontWeight: 'bold' }}>{variation.stockQuantity}</Td>
                <Td>
                  <StockStatusBadge stock={variation.stockQuantity}>
                    {getStatus(variation.stockQuantity)}
                  </StockStatusBadge>
                </Td>
                <Td>
                  <div style={{display: 'flex', justifyContent: 'flex-end'}}>
                    <ActionButton title="Ajustar Estoque">
                      <Edit size={18} />
                    </ActionButton>
                  </div>
                </Td>
              </Tr>
            ))}
          </tbody>
        </Table>
        {filteredVariations.length === 0 && (
          <div style={{ padding: '2rem', textAlign: 'center', color: '#6b7280' }}>
            {allVariations.length === 0 
              ? 'Nenhum produto com variações cadastrado no estoque.'
              : 'Nenhum item encontrado para os filtros selecionados.'}
          </div>
        )}
      </Card>
    </PageContainer>
  );
};

export default Stock;
