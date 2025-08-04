import React from 'react';
import { XAxis, YAxis, CartesianGrid, Tooltip, Legend, ResponsiveContainer, LineChart, Line } from 'recharts';
import {ShoppingCart, Package, DollarSign, PlusCircle, ArrowUpRight, ArrowDownRight } from 'lucide-react';
import { AppContainer, MainContent, DashboardGrid, StatsGrid, ChartCard, Card, PrimaryButton, SecondaryButton, StatusBadge} from './styles.js';
import Header from '../../components/Header/index.jsx';
import SideBar from '../../components/Sidebar/index.jsx';
import StatCard from '../../components/StatCard/index.jsx';
import { Navigate, useNavigate } from 'react-router-dom';

const dailySalesData = [
  { name: 'Seg', faturamento: 1200, lucro: 450 }, { name: 'Ter', faturamento: 1900, lucro: 700 }, { name: 'Qua', faturamento: 1500, lucro: 550 }, { name: 'Qui', faturamento: 2800, lucro: 1100 }, { name: 'Sex', faturamento: 2390, lucro: 950 }, { name: 'Sáb', faturamento: 3490, lucro: 1400 }, { name: 'Dom', faturamento: 980, lucro: 350 },
];
const recentSales = [
  { id: 'VENDA-00125', customer: 'Ana Clara', amount: 125.50, status: 'Concluída' }, { id: 'VENDA-00124', customer: 'Marcos Silva', amount: 89.90, status: 'Concluída' }, { id: 'VENDA-00123', customer: 'Cliente Balcão', amount: 45.00, status: 'Concluída' }, { id: 'VENDA-00122', customer: 'Juliana Costa', amount: 210.00, status: 'Pendente' },
];
const statCardsData = {
  vendasHoje: { value: 'R$ 1.250,75', change: '+15%', isPositive: true }, lucroHoje: { value: 'R$ 480,30', change: '+12%', isPositive: true }, ticketMedio: { value: 'R$ 83,38', change: '-2%', isPositive: false }, baixoEstoque: { value: '8', change: 'itens', isPositive: null },
};

export default function Home() {
  const navigate = useNavigate();
  return (
    <>
      <AppContainer>
        <SideBar />
        <MainContent>
          <Header />

          <DashboardGrid>
            <StatsGrid>
              <StatCard title="Vendas Hoje" value={statCardsData.vendasHoje.value} change={statCardsData.vendasHoje.change} isPositive={statCardsData.vendasHoje.isPositive} icon={<DollarSign size={24} style={{ color: '#9ca3af' }} />} />
              <StatCard title="Lucro Hoje" value={statCardsData.lucroHoje.value} change={statCardsData.lucroHoje.change} isPositive={statCardsData.lucroHoje.isPositive} icon={<ArrowUpRight size={24} style={{ color: '#9ca3af' }} />} />
              <StatCard title="Ticket Médio" value={statCardsData.ticketMedio.value} change={statCardsData.ticketMedio.change} isPositive={statCardsData.ticketMedio.isPositive} icon={<ShoppingCart size={24} style={{ color: '#9ca3af' }} />} />
              <StatCard title="Itens em Baixo Estoque" value={statCardsData.baixoEstoque.value} change={statCardsData.baixoEstoque.change} isPositive={statCardsData.baixoEstoque.isPositive} icon={<Package size={24} style={{ color: '#9ca3af' }} />} />
            </StatsGrid>

            <div style={{ display: 'grid', gridTemplateColumns: 'repeat(1, 1fr)', gap: '2rem', '@media (minWidth: 1280px)': { gridTemplateColumns: 'repeat(3, 1fr)' } }}>
              <ChartCard>
                <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: '1rem' }}>
                  <h3 style={{ fontSize: '1.25rem', fontWeight: 'bold', color: '#1f2937' }}>Fluxo de Vendas</h3>
                  <div style={{ display: 'flex', alignItems: 'center', gap: '0.5rem' }}>
                    <button style={{ padding: '0.25rem 0.75rem', fontSize: '0.875rem', fontWeight: 600, color: 'white', backgroundColor: '#4f46e5', borderRadius: '0.5rem', border: 'none' }}>7 Dias</button>
                    <button style={{ padding: '0.25rem 0.75rem', fontSize: '0.875rem', fontWeight: 600, color: '#374151', backgroundColor: '#e5e7eb', borderRadius: '0.5rem', border: 'none' }}>Mês</button>
                  </div>
                </div>
                <div style={{ width: '100%', height: 350 }}>
                  <ResponsiveContainer>
                    <LineChart data={dailySalesData} margin={{ top: 5, right: 20, left: -10, bottom: 5 }}>
                      <CartesianGrid strokeDasharray="3 3" stroke="#e5e7eb" />
                      <XAxis dataKey="name" stroke="#6b7280" />
                      <YAxis stroke="#6b7280" />
                      <Tooltip contentStyle={{ backgroundColor: '#fff', border: '1px solid #e5e7eb', borderRadius: '0.5rem' }}/>
                      <Legend />
                      <Line type="monotone" dataKey="faturamento" stroke="#4f46e5" strokeWidth={3} name="Faturamento" />
                      <Line type="monotone" dataKey="lucro" stroke="#10b981" strokeWidth={2} name="Lucro" />
                    </LineChart>
                  </ResponsiveContainer>
                </div>
              </ChartCard>

              <div style={{ display: 'flex', flexDirection: 'column', gap: '2rem' }}>
                <Card>
                  <h3 style={{ fontSize: '1.25rem', fontWeight: 'bold', color: '#1f2937', marginBottom: '1rem' }}>Ações Rápidas</h3>
                  <div style={{ display: 'flex', flexDirection: 'column', gap: '0.75rem' }}>
                    <PrimaryButton onClick={() => navigate('/Sales/Create')}>
                      <PlusCircle size={24} style={{ marginRight: '0.75rem' }} /> Nova Venda
                    </PrimaryButton>
                    <SecondaryButton onClick={() => navigate('/Products/Create')}>Adicionar Produto</SecondaryButton>
                    <SecondaryButton onClick={() => navigate('/Stock')}>Ver Estoque</SecondaryButton>
                  </div>
                </Card>
                <Card style={{ flexGrow: 1 }}>
                  <h3 style={{ fontSize: '1.25rem', fontWeight: 'bold', color: '#1f2937', marginBottom: '1rem' }}>Vendas Recentes</h3>
                  <div style={{ display: 'flex', flexDirection: 'column', gap: '0.75rem' }}>
                    {recentSales.map(sale => (
                      <div key={sale.id} style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                        <div>
                          <p style={{ fontWeight: 600, color: '#1f2937' }}>{sale.customer}</p>
                          <p style={{ fontSize: '0.875rem', color: '#6b7280' }}>{sale.id}</p>
                        </div>
                        <div style={{ textAlign: 'right' }}>
                          <p style={{ fontWeight: 'bold', color: '#1f2937' }}>R$ {sale.amount.toFixed(2)}</p>
                          <StatusBadge status={sale.status}>{sale.status}</StatusBadge>
                        </div>
                      </div>
                    ))}
                  </div>
                </Card>
              </div>
            </div>
          </DashboardGrid>
        </MainContent>
      </AppContainer>
    </>
  );
}