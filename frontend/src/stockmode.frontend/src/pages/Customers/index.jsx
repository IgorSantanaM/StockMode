import React, { useState, useEffect } from 'react';
import { Users, PlusCircle, Edit, Trash2, Info } from 'lucide-react';
import {
  PageContainer,
  PageHeader,
  TitleContainer,
  Title,
  PrimaryButton,
  Card,
  FilterContainer,
  Input,
  Table,
  Th,
  Td,
  Tr,
  CustomerInfo,
  ActionButton,
  PaginationContainer, // Import pagination styles
  PaginationButton,  // Import pagination styles
  PageInfo           // Import pagination styles
} from './styles';
import { useNavigate } from 'react-router-dom';
import { toast } from 'react-toastify';
import api from '../../services/api';

const ITEMS_PER_PAGE = 10;

const Customers = () => {
  const navigate = useNavigate();
  const [searchTerm, setSearchTerm] = useState('');
  const [isLoading, setIsLoading] = useState(true);
  const [customers, setCustomers] = useState([]);

  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(0);

  useEffect(() => {
    // When a new search is performed, reset to the first page
    setCurrentPage(1);
  }, [searchTerm]);

  useEffect(() => {
    const fetchCustomers = async () => {
      setIsLoading(true);
      try {
        const params = new URLSearchParams();
        if (searchTerm) params.append('name', searchTerm);
        params.append('page', currentPage);
        params.append('pageSize', ITEMS_PER_PAGE);

        const response = await api.get(`/customers?${params.toString()}`);
        
        setCustomers(response.data.items || []);
        setTotalPages(response.data.totalPages || 0);
      } catch (error) {
        console.error("Failed to fetch customers: ", error);
        toast.error("Falha ao buscar clientes.");
        setCustomers([]); // Clear customers on error
        setTotalPages(0);
      } finally {
        setIsLoading(false);
      }
    };

    // Debounce the API call
    const handler = setTimeout(() => {
      fetchCustomers();
    }, 500);

    return () => {
      clearTimeout(handler);
    };
  }, [searchTerm, currentPage]);

  const formatDate = (dateString) => {
    const date = new Date(dateString);
    return new Intl.DateTimeFormat('pt-BR', { timeZone: 'UTC' }).format(date);
  };

  const handlePageChange = (page) => {
    if (page >= 1 && page <= totalPages && page !== currentPage) {
      setCurrentPage(page);
    }
  };

  const formatCurrency = (value) => {
    return (value || 0).toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });
  };

  return (
    <PageContainer>
      <PageHeader>
        <TitleContainer>
          <Users size={32} color="#4f46e5" />
          <Title>Clientes</Title>
        </TitleContainer>
        <PrimaryButton onClick={() => navigate("/customers/create")}>
          <PlusCircle size={20} />
          Adicionar Cliente
        </PrimaryButton>
      </PageHeader>

      <Card>
        <FilterContainer>
          <Input 
            type="text" 
            placeholder="Buscar por nome ou email..." 
            value={searchTerm}
            onChange={(e) => setSearchTerm(e.target.value)}
          />
        </FilterContainer>

        <Table>
          <thead>
            <tr>
              <Th>Cliente</Th>
              <Th>Contacto</Th>
              <Th>Última Compra</Th>
              <Th>Total Gasto</Th>
              <Th>Ações</Th>
            </tr>
          </thead>
          <tbody>
            {/* CORRECTED: Map over `customers` state */}
            {!isLoading && customers.map((customer) => (
              <Tr key={customer.id}>
                <Td>
                  <CustomerInfo>
                    <div>
                      <p style={{ fontWeight: 500 }}>{customer.name}</p>
                      <p style={{ fontSize: '0.8rem', color: '#6b7280' }}>{customer.email}</p>
                    </div>
                  </CustomerInfo>
                </Td>
                <Td>{customer.phoneNumber}</Td>
                {/* Assuming lastPurchaseDate and totalSpent exist, otherwise handle nulls */}
                <Td>{customer.lastPurchaseDate ? formatDate(customer.lastPurchaseDate) : 'N/A'}</Td>
                <Td style={{ fontWeight: 500 }}>{formatCurrency(customer.totalSpent)}</Td>
                <Td>
                  <div style={{display: 'flex', justifyContent: 'flex-end', gap: '0.5rem'}}>
                    <ActionButton title="Detalhes do Cliente">
                      <Info size={18} />
                    </ActionButton>
                    <ActionButton title="Editar Cliente">
                      <Edit size={18} />
                    </ActionButton>
                    <ActionButton title="Excluir Cliente">
                      <Trash2 size={18} />
                    </ActionButton>
                  </div>
                </Td>
              </Tr>
            ))}
          </tbody>
        </Table>

        {/* ADDED: Loading and Empty States */}
        {isLoading && (
          <div style={{ padding: '2rem', textAlign: 'center', color: '#6b7280' }}>
            Carregando...
          </div>
        )}
        {!isLoading && customers.length === 0 && (
          <div style={{ padding: '2rem', textAlign: 'center', color: '#6b7280' }}>
            Nenhum cliente encontrado.
          </div>
        )}
        
        {/* ADDED: Pagination UI */}
        {!isLoading && totalPages > 0 && (
          <PaginationContainer>
            <PageInfo>
              Página {currentPage} de {totalPages}
            </PageInfo>
            <div style={{display: 'flex', gap: '0.5rem'}}>
              <PaginationButton
                onClick={() => handlePageChange(currentPage - 1)}
                disabled={currentPage === 1}
              >
                Anterior
              </PaginationButton>
              <PaginationButton
                onClick={() => handlePageChange(currentPage + 1)}
                disabled={currentPage === totalPages}
              >
                Próxima
              </PaginationButton>
            </div>
          </PaginationContainer>
        )}
      </Card>
    </PageContainer>
  );
};

export default Customers;