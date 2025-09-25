import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { 
  ShoppingCart, 
  Plus, 
  Eye, 
  Calendar, 
  Filter,
  Search,
  ChevronLeft,
  ChevronRight
} from 'lucide-react';
import api from '../../services/api';
import {
  PageContainer,
  Container,
  Header,
  Title,
  HeaderActions,
  FiltersSection,
  FilterGroup,
  Input,
  Select,
  Button,
  SalesGrid,
  SaleCard,
  SaleInfo,
  SaleActions,
  StatusBadge,
  Pagination,
  PaginationButton,
  EmptyState
} from './styles';

const SALE_STATUS = {
  1: { label: 'Pendente', color: '#d97706' },
  2: { label: 'Concluída', color: '#059669' },
  3: { label: 'Cancelada', color: '#dc2626' }
};

const Sales = () => {
  const navigate = useNavigate();
  
  const [sales, setSales] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  
  // Filtros
  const [filters, setFilters] = useState({
    startDate: '',
    endDate: '',
    status: '',
    page: 1,
    pageSize: 12
  });
  
  const [totalPages, setTotalPages] = useState(1);

  useEffect(() => {
    loadSales();
  }, [filters]);

  const loadSales = async () => {
    setLoading(true);
    try {
      const params = new URLSearchParams();
      
      if (filters.startDate) params.append('startDate', filters.startDate);
      if (filters.endDate) params.append('endDate', filters.endDate);
      if (filters.status !== '') params.append('status', filters.status);
      params.append('page', filters.page.toString());
      params.append('pageSize', filters.pageSize.toString());
      
      const response = await api.get(`sales?${params.toString()}`);
      
      setSales(response.data?.items || response.data || []);
      
      // Calcular total de páginas baseado na resposta
      const totalItems = response.data?.totalItems || response.data?.length || 0;
      setTotalPages(Math.ceil(totalItems / filters.pageSize));
      
    } catch (err) {
      setError('Erro ao carregar vendas');
      setSales([]);
    } finally {
      setLoading(false);
    }
  };

  const handleFilterChange = (key, value) => {
    setFilters(prev => ({
      ...prev,
      [key]: value,
      page: 1 // Reset para primeira página quando filtros mudarem
    }));
  };

  const handlePageChange = (newPage) => {
    setFilters(prev => ({
      ...prev,
      page: newPage
    }));
  };

  const formatDate = (dateString) => {
    if (!dateString) return 'N/A';
    return new Date(dateString).toLocaleDateString('pt-BR');
  };

  const formatCurrency = (value) => {
    if (value === undefined || value === null) return 'R$ 0,00';
    return `R$ ${value.toFixed(2).replace('.', ',')}`;
  };

  return (
    <PageContainer>
      <Container>
        <Header>
          <div>
            <ShoppingCart size={32} color="#3b82f6" />
            <Title>Vendas</Title>
          </div>
          <HeaderActions>
            <Button onClick={() => navigate('/sales/new')}>
              <Plus size={20} />
              Nova Venda
            </Button>
          </HeaderActions>
        </Header>

        <FiltersSection>
          <FilterGroup>
            <label>Data Inicial</label>
            <Input
              type="date"
              value={filters.startDate}
              onChange={(e) => handleFilterChange('startDate', e.target.value)}
            />
          </FilterGroup>

          <FilterGroup>
            <label>Data Final</label>
            <Input
              type="date"
              value={filters.endDate}
              onChange={(e) => handleFilterChange('endDate', e.target.value)}
            />
          </FilterGroup>

          <FilterGroup>
            <label>Status</label>
            <Select
              value={filters.status}
              onChange={(e) => handleFilterChange('status', e.target.value)}
            >
              <option value="">Todos os Status</option>
              <option value="1">Pendente</option>
              <option value="2">Concluída</option>
              <option value="3">Cancelada</option>
            </Select>
          </FilterGroup>

          <Button onClick={loadSales}>
            <Filter size={16} />
            Filtrar
          </Button>
        </FiltersSection>

        {error && (
          <div style={{ 
            padding: '16px', 
            backgroundColor: '#fecaca', 
            color: '#dc2626', 
            borderRadius: '8px',
            marginBottom: '20px'
          }}>
            {error}
          </div>
        )}

        {loading ? (
          <div style={{ textAlign: 'center', padding: '40px' }}>
            Carregando vendas...
          </div>
        ) : sales.length === 0 ? (
          <EmptyState>
            <ShoppingCart size={64} color="#9ca3af" />
            <h3>Nenhuma venda encontrada</h3>
            <p>Crie sua primeira venda clicando no botão "Nova Venda"</p>
            <Button onClick={() => navigate('/sales/new')}>
              <Plus size={20} />
              Nova Venda
            </Button>
          </EmptyState>
        ) : (
          <>
            <SalesGrid>
              {sales.map(sale => (
                <SaleCard key={sale.id}>
                  <SaleInfo>
                    <div className="sale-header">
                      <h3>Venda #{sale.id}</h3>
                      <StatusBadge status={sale.status}>
                        {SALE_STATUS[sale.status]?.label || 'Desconhecido'}
                      </StatusBadge>
                    </div>
                    
                    <div className="sale-details">
                      <p><strong>Data:</strong> {formatDate(sale.saleDate)}</p>
                      <p><strong>Método:</strong> {getPaymentMethodName(sale.paymentMethod)}</p>
                      <p><strong>Total:</strong> {formatCurrency(sale.finalPrice)}</p>
                      {sale.customerId && <p><strong>Cliente:</strong> #{sale.customerId}</p>}
                      <p><strong>Itens:</strong> {sale.itemsCount || sale.items?.length || 0}</p>
                    </div>
                  </SaleInfo>
                  
                  <SaleActions>
                    <Button onClick={() => navigate(`/sales/manage/${sale.id}`)}>
                      <Eye size={16} />
                      Ver Detalhes
                    </Button>
                  </SaleActions>
                </SaleCard>
              ))}
            </SalesGrid>

            {totalPages > 1 && (
              <Pagination>
                <PaginationButton
                  onClick={() => handlePageChange(filters.page - 1)}
                  disabled={filters.page === 1}
                >
                  <ChevronLeft size={16} />
                  Anterior
                </PaginationButton>
                
                <span>
                  Página {filters.page} de {totalPages}
                </span>
                
                <PaginationButton
                  onClick={() => handlePageChange(filters.page + 1)}
                  disabled={filters.page === totalPages}
                >
                  Próximo
                  <ChevronRight size={16} />
                </PaginationButton>
              </Pagination>
            )}
          </>
        )}
      </Container>
    </PageContainer>
  );
};

const getPaymentMethodName = (method) => {
  const methods = {
    1: 'PIX',
    2: 'Cartão de Crédito',
    3: 'Cartão de Débito', 
    4: 'Dinheiro',
    5: 'Crédito da Loja'
  };
  return methods[method] || 'N/A';
};

export default Sales;