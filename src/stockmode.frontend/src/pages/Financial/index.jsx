import React from 'react';
import { BarChart, Bar, XAxis, YAxis, CartesianGrid, Tooltip, Legend, ResponsiveContainer } from 'recharts';
import { DollarSign, PlusCircle, MinusCircle, ArrowUpRight, ArrowDownRight, TrendingUp, TrendingDown } from 'lucide-react';
import {
  PageContainer,
  PageHeader,
  TitleContainer,
  Title,
  ButtonGroup,
  PrimaryButton,
  SecondaryButton,
  StatsGrid,
  Card,
  Table, Th, Td, Tr,
  TransactionType,
} from './styles';
import { useNavigate } from 'react-router-dom';

// --- DADOS MOCK (Numa aplicação real, viriam da sua API) ---
const financialStats = {
  faturamentoMes: { value: 'R$ 18.750,00', change: '+8.2%', isPositive: true },
  lucroBruto: { value: 'R$ 7.200,50', change: '+5.1%', isPositive: true },
  contasPagar: { value: 'R$ 2.150,00', label: 'Vencendo em 7 dias' },
  contasReceber: { value: 'R$ 890,00', label: 'Atrasado' },
};

const cashFlowData = [
  { day: '24/07', Entradas: 1200, Saídas: 400 },
  { day: '25/07', Entradas: 1900, Saídas: 1100 },
  { day: '26/07', Entradas: 1500, Saídas: 200 },
  { day: '27/07', Entradas: 2800, Saídas: 1500 },
  { day: '28/07', Entradas: 2390, Saídas: 800 },
  { day: '29/07', Entradas: 3490, Saídas: 1200 },
  { day: '30/07', Entradas: 980, Saídas: 300 },
];

const recentTransactions = [
  { id: 1, date: '2025-07-30', description: 'Venda #VENDA-00125', type: 'Entrada', amount: 125.50 },
  { id: 2, date: '2025-07-30', description: 'Pagamento Fornecedor ABC', type: 'Saída', amount: -350.00 },
  { id: 3, date: '2025-07-29', description: 'Venda #VENDA-00124', type: 'Entrada', amount: 89.90 },
  { id: 4, date: '2025-07-28', description: 'Conta de Energia', type: 'Saída', amount: -180.75 },
];

// --- COMPONENTES REUTILIZÁVEIS ---
const StatCard = ({ title, value, change, isPositive, label, icon }) => (
  <Card>
    <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: '0.5rem' }}>
      <h3 style={{ fontSize: '1rem', fontWeight: 500, color: '#6b7280' }}>{title}</h3>
      {icon}
    </div>
    <div>
      <p style={{ fontSize: '1.875rem', fontWeight: 'bold', color: '#1f2937' }}>{value}</p>
      <div style={{ display: 'flex', alignItems: 'center', fontSize: '0.875rem', marginTop: '0.25rem' }}>
        {change && (isPositive ? <ArrowUpRight style={{ height: '1rem', width: '1rem', color: '#10b981' }} /> : <ArrowDownRight style={{ height: '1rem', width: '1rem', color: '#ef4444' }} />)}
        <span style={{ marginLeft: '0.25rem', color: isPositive ? '#059669' : '#dc2626' }}>
          {change || label}
        </span>
      </div>
    </div>
  </Card>
);

const Financial = () => {
  const formatCurrency = (value) => value.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });
  const formatDate = (dateString) => new Intl.DateTimeFormat('pt-BR', {timeZone: 'UTC'}).format(new Date(dateString));
  var navigate = useNavigate();
  return (
    <PageContainer>
      <PageHeader>
        <TitleContainer>
          <DollarSign size={32} color="#4f46e5" />
          <Title>Painel Financeiro</Title>
        </TitleContainer>
        <ButtonGroup>
          <SecondaryButton onClick={() => navigate("/expense/register")}>
            <MinusCircle size={20} />
            Registrar Despesa
          </SecondaryButton>
          <PrimaryButton onClick={() => navigate("/revenue/register")}>
            <PlusCircle size={20} />
            Registrar Receita
          </PrimaryButton>
        </ButtonGroup>
      </PageHeader>

      <StatsGrid>
        <StatCard title="Faturamento no Mês" value={financialStats.faturamentoMes.value} change={financialStats.faturamentoMes.change} isPositive={financialStats.faturamentoMes.isPositive} icon={<TrendingUp size={24} style={{ color: '#9ca3af' }} />} />
        <StatCard title="Lucro Bruto (Mês)" value={financialStats.lucroBruto.value} change={financialStats.lucroBruto.change} isPositive={financialStats.lucroBruto.isPositive} icon={<ArrowUpRight size={24} style={{ color: '#9ca3af' }} />} />
        <StatCard title="Contas a Pagar" value={financialStats.contasPagar.value} label={financialStats.contasPagar.label} isPositive={false} icon={<ArrowDownRight size={24} style={{ color: '#9ca3af' }} />} />
        <StatCard title="Contas a Receber" value={financialStats.contasReceber.value} label={financialStats.contasReceber.label} isPositive={true} icon={<ArrowUpRight size={24} style={{ color: '#9ca3af' }} />} />
      </StatsGrid>

      <div style={{ display: 'grid', gridTemplateColumns: '1fr', gap: '2rem', marginTop: '2rem' }}>
        <Card>
          <h3 style={{ fontSize: '1.25rem', fontWeight: 'bold', color: '#1f2937', marginBottom: '1.5rem' }}>Fluxo de Caixa (Últimos 7 dias)</h3>
          <div style={{ height: '350px' }}>
            <ResponsiveContainer width="100%" height="100%">
              <BarChart data={cashFlowData} margin={{ top: 5, right: 20, left: -10, bottom: 5 }}>
                <CartesianGrid strokeDasharray="3 3" stroke="#e5e7eb" />
                <XAxis dataKey="day" stroke="#6b7280" />
                <YAxis stroke="#6b7280" tickFormatter={(value) => `R$ ${value / 1000}k`} />
                <Tooltip contentStyle={{ backgroundColor: '#fff', border: '1px solid #e5e7eb', borderRadius: '0.5rem' }} formatter={(value) => formatCurrency(value)} />
                <Legend />
                <Bar dataKey="Entradas" fill="#10b981" radius={[4, 4, 0, 0]} />
                <Bar dataKey="Saídas" fill="#ef4444" radius={[4, 4, 0, 0]} />
              </BarChart>
            </ResponsiveContainer>
          </div>
        </Card>

        <Card>
          <h3 style={{ fontSize: '1.25rem', fontWeight: 'bold', color: '#1f2937', marginBottom: '1rem' }}>Transações Recentes</h3>
          <Table>
            <thead>
              <tr>
                <Th>Data</Th>
                <Th>Descrição</Th>
                <Th>Tipo</Th>
                <Th>Valor</Th>
              </tr>
            </thead>
            <tbody>
              {recentTransactions.map(t => (
                <Tr key={t.id}>
                  <Td>{formatDate(t.date)}</Td>
                  <Td>{t.description}</Td>
                  <Td><TransactionType type={t.type}>{t.type}</TransactionType></Td>
                  <Td style={{ fontWeight: 500, textAlign: 'right' }}>{formatCurrency(t.amount)}</Td>
                </Tr>
              ))}
            </tbody>
          </Table>
        </Card>
      </div>
    </PageContainer>
  );
};

export default Financial;
