import React, { useState, useEffect } from 'react';
import api from '../../services/api';
import { Truck, PlusCircle, Edit, Trash2, Info, SearchCheck } from 'lucide-react';
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
  ActionButton,
  PaginationContainer,
  PaginationButton,
  PageInfo
} from './styles';
import { useNavigate } from 'react-router-dom';
import { toast } from 'react-toastify';

const ITEMS_PER_PAGE = 10;

const Suppliers = () => {
  var navigate = useNavigate();
  const [searchTerm, setSearchTerm] = useState('');
  const [isLoading, setIsLoading] = useState(true);
  const [suppliers, setSuppliers] = useState([]);

  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(0);

  useEffect(() => {
    setCurrentPage(1);
  }, [searchTerm]);

  useEffect(() => {
    const fetchSuppliers = async () => {
      setIsLoading(true);
      try{
        const params = new URLSearchParams()
        if(searchTerm) params.append('name', searchTerm);
        params.append('page', currentPage);
        params.append('pageSize', ITEMS_PER_PAGE);

        const response = await api.get(`/suppliers?${params.toString()}`);

        setSuppliers(response.data.items || []);
        setTotalPages(response.data.totalPages || 0);
      }
      catch(error){
        console.error("Failed to fetch Suppliers: ", error);
        toast.error("Falha ao buscar fornecedores.");
        setSuppliers([]);
        setTotalPages(0);
      }finally{
        setIsLoading(false);
      }
    }

    const handler = setTimeout(() => {
      fetchSuppliers();
    }, 500);

  return () => {
      clearTimeout(handler);
    };
  }, [searchTerm, currentPage]);

  const handlePageChange = (page) => {
    if (page >= 1 && page <= totalPages && page !== currentPage) {
      setCurrentPage(page);
    }
  };

  return (
    <PageContainer>
      <PageHeader>
        <TitleContainer>
          <Truck size={32} color="#4f46e5" />
          <Title>Fornecedores</Title>
        </TitleContainer>
        <PrimaryButton onClick={() => navigate("/suppliers/register")}>
          <PlusCircle size={20} />
          Adicionar Fornecedor
        </PrimaryButton>
      </PageHeader>

      <Card>
        <FilterContainer>
          <Input 
            type="text" 
            placeholder="Buscar por nome ou email do fornecedor..." 
            value={searchTerm}
            onChange={(e) => setSearchTerm(e.target.value)}
          />
        </FilterContainer>

        <Table>
          <thead>
            <tr>
              <Th>Nome do Fornecedor</Th>
              <Th>Contacto Principal</Th>
              <Th>Email</Th>
              <Th>Telefone</Th>
              <Th>Ações</Th>
            </tr>
          </thead>
          <tbody>
            {!isLoading && suppliers.map((supplier) => (
              <Tr key={supplier.id}>
                <Td style={{ fontWeight: 500 }}>{supplier.name}</Td>
                <Td>{supplier.contactPerson}</Td>
                <Td style={{ color: '#6b7280' }}>{supplier.email}</Td>
                <Td>{supplier.phoneNumber}</Td>
                <Td>
                  <div style={{display: 'flex', justifyContent: 'flex-end', gap: '0.5rem'}}>
                    <ActionButton title="Editar Cliente">
                        <Info size={18} />
                    </ActionButton>
                    <ActionButton title="Editar Fornecedor">
                      <Edit size={18} />
                    </ActionButton>
                    <ActionButton title="Excluir Fornecedor">
                      <Trash2 size={18} />
                    </ActionButton>
                  </div>
                </Td>
              </Tr>
            ))}
          </tbody>
        </Table>
        {isLoading && (
          <div style={{ padding: '2rem', textAlign: 'center', color: '#6b7280' }}>
            Carregando...
          </div>
        )}
        {!isLoading && suppliers.length === 0 && (
          <div style={{ padding: '2rem', textAlign: 'center', color: '#6b7280' }}>
            Nenhum cliente encontrado.
          </div>
        )}

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

export default Suppliers;
