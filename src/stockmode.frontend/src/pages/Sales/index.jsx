import React, { useState, useMemo } from 'react';
import { ShoppingCart, PlusCircle } from 'lucide-react';
import {
  PageContainer,
  PageHeader,
  TitleContainer,
  Title,
  PrimaryButton,
  Card,
  FilterContainer,
  Select,
  Input,
  Table,
  Th,
  Td,
  Tr,
  StatusBadge,
  BadgeDot,
} from './styles';
import { useNavigate } from 'react-router-dom';

// --- DADOS MOCK (Em uma aplicação real, viriam da sua API) ---
const allSales = [
  { id: 'VENDA-00125', customer: 'Ana Clara', date: '2025-08-04', amount: 125.50, status: 'Concluída' },
  { id: 'VENDA-00124', customer: 'Marcos Silva', date: '2025-08-04', amount: 89.90, status: 'Concluída' },
  { id: 'VENDA-00123', customer: 'Cliente Balcão', date: '2025-08-03', amount: 45.00, status: 'Concluída' },
  { id: 'VENDA-00122', customer: 'Juliana Costa', date: '2025-08-03', amount: 210.00, status: 'Pendente' },
  { id: 'VENDA-00121', customer: 'Ricardo Alves', date: '2025-08-02', amount: 75.00, status: 'Cancelada' },
  { id: 'VENDA-00120', customer: 'Beatriz Lima', date: '2025-08-01', amount: 350.20, status: 'Concluída' },
];

const Sales = () => {
  const [statusFilter, setStatusFilter] = useState('Todos');
  const [dateFilter, setDateFilter] = useState('');
  var navigate = useNavigate();

  const filteredSales = useMemo(() => {
    return allSales.filter(sale => {
      const statusMatch = statusFilter === 'Todos' || sale.status === statusFilter;
      const dateMatch = dateFilter === '' || sale.date === dateFilter;
      return statusMatch && dateMatch;
    });
  }, [statusFilter, dateFilter]);

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
          <ShoppingCart size={32} color="#4f46e5" />
          <Title>Histórico de Vendas</Title>
        </TitleContainer>
        <PrimaryButton onClick={() => navigate("/sales/create")}>
          <PlusCircle size={20} />
          Nova Venda
        </PrimaryButton>
      </PageHeader>

      <Card>
        <FilterContainer>
          <Select value={statusFilter} onChange={(e) => setStatusFilter(e.target.value)}>
            <option value="Todos">Todos os Status</option>
            <option value="Concluída">Concluída</option>
            <option value="Pendente">Pendente</option>
            <option value="Cancelada">Cancelada</option>
          </Select>
          <Input 
            type="date" 
            value={dateFilter}
            onChange={(e) => setDateFilter(e.target.value)}
          />
        </FilterContainer>

        <Table>
          <thead>
            <tr>
              <Th>ID da Venda</Th>
              <Th>Cliente</Th>
              <Th>Data</Th>
              <Th>Status</Th>
              <Th>Total</Th>
            </tr>
          </thead>
          <tbody>
            {filteredSales.map((sale) => (
              <Tr key={sale.id}>
                <Td style={{ fontWeight: 500, color: '#4f46e5' }}>{sale.id}</Td>
                <Td>{sale.customer}</Td>
                <Td>{formatDate(sale.date)}</Td>
                <Td>
                  <StatusBadge status={sale.status}>
                    <BadgeDot />
                    {sale.status}
                  </StatusBadge>
                </Td>
                <Td style={{ fontWeight: 'bold' }}>{formatCurrency(sale.amount)}</Td>
              </Tr>
            ))}
          </tbody>
        </Table>
        {filteredSales.length === 0 && (
          <div style={{ padding: '2rem', textAlign: 'center', color: '#6b7280' }}>
            Nenhuma venda encontrada para os filtros selecionados.
          </div>
        )}
      </Card>
    </PageContainer>
  );
};

export default Sales;
