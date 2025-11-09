import React, { useState, useEffect } from 'react';
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
import api from '../../services/api';
import { LoadingContainer } from '../../util/LoadingContainer';

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
  const navigate = useNavigate();
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [financialStats, setFinancialStats] = useState({
    faturamentoMes: { value: 'R$ 0,00', change: '+0%', isPositive: true },
    lucroBruto: { value: 'R$ 0,00', change: '+0%', isPositive: true },
    contasPagar: { value: 'R$ 0,00', label: 'Vencendo em 7 dias' },
    contasReceber: { value: 'R$ 0,00', label: 'Atrasado' },
  });
  const [cashFlowData, setCashFlowData] = useState([]);
  const [recentTransactions, setRecentTransactions] = useState([]);

  const formatCurrency = (value) => value.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });
  const formatDate = (dateString) => new Intl.DateTimeFormat('pt-BR', {timeZone: 'UTC'}).format(new Date(dateString));

  useEffect(() => {
    const fetchFinancialData = async () => {
      try {
        setLoading(true);
        setError(null);

        // Calculate date range for current month and last 7 days
        const now = new Date();
        const firstDayOfMonth = new Date(now.getFullYear(), now.getMonth(), 1);
        const lastDayOfMonth = new Date(now.getFullYear(), now.getMonth() + 1, 0);
        const sevenDaysAgo = new Date(now.getTime() - 7 * 24 * 60 * 60 * 1000);

        // Fetch sales data for current month
        const monthSalesResponse = await api.get('/sales', {
          params: {
            startDate: firstDayOfMonth.toISOString(),
            endDate: lastDayOfMonth.toISOString(),
            status: 'Completed', 
            page: 1,
            pageSize: 1000
          }
        });

        // Fetch sales for last 7 days for cash flow
        const weekSalesResponse = await api.get('/sales', {
          params: {
            startDate: sevenDaysAgo.toISOString(),
            endDate: now.toISOString(),
            page: 1,
            pageSize: 1000
          }
        });

        const monthSales = monthSalesResponse.data?.items || [];
        const weekSales = weekSalesResponse.data?.items || [];

        // Calculate monthly revenue
        const monthlyRevenue = monthSales.reduce((sum, sale) => sum + (sale.finalPrice || 0), 0);

        // Calculate stats
        const stats = {
          faturamentoMes: {
            value: formatCurrency(monthlyRevenue),
            change: '+8.2%', // TODO: Calculate actual revenue change
            isPositive: true
          },
          lucroBruto: {
            value: formatCurrency(monthlyRevenue * 0.35), // Assuming 35% profit margin
            change: '+5.1%', // TODO: Calculate actual profit change
            isPositive: true
          },
          contasPagar: {
            value: 'R$ 0,00', // This would come from expenses API
            label: 'Vencendo em 7 dias'
          },
          contasReceber: {
            value: 'R$ 0,00', // This would come from pending payments API
            label: 'Atrasado'
          }
        };

        setFinancialStats(stats);

        // Process cash flow data for last 7 days
        const cashFlow = {};
        for (let i = 6; i >= 0; i--) {
          const date = new Date(now.getTime() - i * 24 * 60 * 60 * 1000);
          const dateStr = date.toLocaleDateString('pt-BR', { day: '2-digit', month: '2-digit' });
          cashFlow[dateStr] = { day: dateStr, Entradas: 0, Saídas: 0 };
        }

        weekSales.forEach(sale => {
          if (sale.status === 'Completed' || sale.status === 1) {
            const saleDate = new Date(sale.saleDate);
            const dateStr = saleDate.toLocaleDateString('pt-BR', { day: '2-digit', month: '2-digit' });
            if (cashFlow[dateStr]) {
              cashFlow[dateStr].Entradas += sale.finalPrice || 0;
            }
          }
        });

        setCashFlowData(Object.values(cashFlow));

        // Get recent transactions (last 10 sales)
        const recentSales = weekSales
          .sort((a, b) => new Date(b.saleDate) - new Date(a.saleDate))
          .slice(0, 10)
          .map(sale => ({
            id: sale.id,
            date: sale.saleDate,
            description: `Venda #${sale.id.toString().padStart(6, '0')}`,
            type: 'Entrada',
            amount: sale.finalPrice || 0
          }));

        setRecentTransactions(recentSales);
      } catch (err) {
        console.error('Error fetching financial data:', err);
        setError('Erro ao carregar dados financeiros. Por favor, tente novamente.');
      } finally {
        setLoading(false);
      }
    };

    fetchFinancialData();
  }, []);

  if (loading) {
    return <LoadingContainer>Carregando dados financeiros...</LoadingContainer>;
  }

  if (error) {
    return (
      <PageContainer>
        <div style={{ textAlign: 'center', padding: '2rem', color: '#ef4444' }}>
          <p>{error}</p>
        </div>
      </PageContainer>
    );
  }

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
          {recentTransactions.length === 0 ? (
            <p style={{ textAlign: 'center', color: '#6b7280', padding: '2rem' }}>Nenhuma transação encontrada</p>
          ) : (
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
          )}
        </Card>
      </div>
    </PageContainer>
  );
};

export default Financial;
