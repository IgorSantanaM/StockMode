import React, { useState, useMemo } from 'react';
import { Users, PlusCircle, Edit, Trash2 } from 'lucide-react';
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
  Avatar,
  ActionButton,
} from './styles';
import { useNavigate } from 'react-router-dom';

// --- DADOS MOCK (Numa aplicação real, viriam da sua API) ---
const allCustomers = [
  { id: 1, name: 'Ana Clara', email: 'anaclara@email.com', phone: '(67) 99999-1111', lastPurchaseDate: '2025-08-04', totalSpent: 125.50, avatar: 'https://i.pravatar.cc/40?img=1' },
  { id: 2, name: 'Marcos Silva', email: 'marcos.silva@email.com', phone: '(67) 98888-2222', lastPurchaseDate: '2025-08-04', totalSpent: 89.90, avatar: 'https://i.pravatar.cc/40?img=2' },
  { id: 3, name: 'Juliana Costa', email: 'juliana.costa@email.com', phone: '(67) 97777-3333', lastPurchaseDate: '2025-08-03', totalSpent: 210.00, avatar: 'https://i.pravatar.cc/40?img=3' },
  { id: 4, name: 'Ricardo Alves', email: 'ricardo.alves@email.com', phone: '(67) 96666-4444', lastPurchaseDate: '2025-08-02', totalSpent: 75.00, avatar: 'https://i.pravatar.cc/40?img=4' },
  { id: 5, name: 'Beatriz Lima', email: 'beatriz.lima@email.com', phone: '(67) 95555-5555', lastPurchaseDate: '2025-08-01', totalSpent: 350.20, avatar: 'https://i.pravatar.cc/40?img=5' },
];

const Customers = () => {
  const [searchTerm, setSearchTerm] = useState('');
  var navigate = useNavigate();

  const filteredCustomers = useMemo(() => {
    return allCustomers.filter(customer =>
      customer.name.toLowerCase().includes(searchTerm.toLowerCase()) ||
      customer.email.toLowerCase().includes(searchTerm.toLowerCase())
    );
  }, [searchTerm]);

  const formatDate = (dateString) => {
    const date = new Date(dateString);
    return new Intl.DateTimeFormat('pt-BR', {timeZone: 'UTC'}).format(date);
  };

  const formatCurrency = (value) => {
    return value.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });
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
            {filteredCustomers.map((customer) => (
              <Tr key={customer.id}>
                <Td>
                  <CustomerInfo>
                    <div>
                      <p style={{ fontWeight: 500 }}>{customer.name}</p>
                      <p style={{ fontSize: '0.8rem', color: '#6b7280' }}>{customer.email}</p>
                    </div>
                  </CustomerInfo>
                </Td>
                <Td>{customer.phone}</Td>
                <Td>{formatDate(customer.lastPurchaseDate)}</Td>
                <Td style={{ fontWeight: 500 }}>{formatCurrency(customer.totalSpent)}</Td>
                <Td>
                  <div style={{display: 'flex', justifyContent: 'flex-end', gap: '0.5rem'}}>
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
        {filteredCustomers.length === 0 && (
          <div style={{ padding: '2rem', textAlign: 'center', color: '#6b7280' }}>
            Nenhum cliente encontrado.
          </div>
        )}
      </Card>
    </PageContainer>
  );
};

export default Customers;
