import React from 'react';
import { XAxis, YAxis, CartesianGrid, Tooltip, Legend, ResponsiveContainer, LineChart, Line } from 'recharts';
import { ShoppingCart, Package, DollarSign, PlusCircle, ArrowUpRight, ArrowDownRight } from 'lucide-react';
import { 
    DashboardGrid, 
    StatsGrid, 
    ChartCard, 
    PrimaryButton, 
    SecondaryButton, 
    StatusBadge,
    MainLayoutGrid,
    ActionsAndSalesContainer, ActionCard, SalesCard 
} from './styles.js';
import StatCard from '../../components/StatCard/index.jsx';

const dailySalesData = [
  { name: 'Seg', faturamento: 1200, lucro: 450 }, { name: 'Ter', faturamento: 1900, lucro: 700 }, { name: 'Qua', faturamento: 1500, lucro: 550 }, { name: 'Qui', faturamento: 2800, lucro: 1100 }, { name: 'Sex', faturamento: 2390, lucro: 950 }, { name: 'Sáb', faturamento: 3490, lucro: 1400 }, { name: 'Dom', faturamento: 980, lucro: 350 },
];
const recentSales = [
  { id: 'VENDA-00125', customer: 'Ana Clara', amount: 125.50, status: 'Concluída' }, { id: 'VENDA-00124', customer: 'Marcos Silva', amount: 89.90, status: 'Concluída' }, { id: 'VENDA-00123', customer: 'Cliente Balcão', amount: 45.00, status: 'Concluída' }, { id: 'VENDA-00122', customer: 'Juliana Costa', amount: 210.00, status: 'Pendente' },
];
const statCardsData = {
  vendasHoje: { value: 'R$ 1.250,75', change: '+15%', isPositive: true, icon: <DollarSign size={20} /> },
  lucroHoje: { value: 'R$ 480,30', change: '+12%', isPositive: true, icon: <ArrowUpRight size={20} /> },
  ticketMedio: { value: 'R$ 83,38', change: '-2%', isPositive: false, icon: <ShoppingCart size={20} /> },
  baixoEstoque: { value: '8', change: 'itens', isPositive: null, icon: <Package size={20} /> },
};

export default function Home() {
  return (
    <DashboardGrid>
      <StatsGrid>
        <StatCard title="Vendas Hoje" {...statCardsData.vendasHoje} />
        <StatCard title="Lucro Hoje" {...statCardsData.lucroHoje} />
        <StatCard title="Ticket Médio" {...statCardsData.ticketMedio} />
        <StatCard title="Itens em Baixo Estoque" {...statCardsData.baixoEstoque} />
      </StatsGrid>

      <MainLayoutGrid>
        <ChartCard>
          <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: '1.5rem', flexWrap: 'wrap', gap: '1rem' }}>
            <h3 style={{ fontSize: '1.125rem', fontWeight: '600', color: '#1f2937', margin: 0 }}>Fluxo de Vendas</h3>
            <div style={{ display: 'flex', alignItems: 'center', gap: '0.5rem' }}>
              <button style={{ padding: '0.25rem 0.75rem', fontSize: '0.875rem', fontWeight: 600, color: 'white', backgroundColor: '#4f46e5', borderRadius: '0.5rem', border: 'none', cursor: 'pointer' }}>7 Dias</button>
              <button style={{ padding: '0.25rem 0.75rem', fontSize: '0.875rem', fontWeight: 600, color: '#374151', backgroundColor: '#e5e7eb', borderRadius: '0.5rem', border: 'none', cursor: 'pointer' }}>Mês</button>
            </div>
          </div>
          <div style={{ width: '100%', height: 350 }}>
            <ResponsiveContainer>
              <LineChart data={dailySalesData} margin={{ top: 5, right: 20, left: -10, bottom: 5 }}>
                <CartesianGrid strokeDasharray="3 3" stroke="#e5e7eb" />
                <XAxis dataKey="name" stroke="#6b7280" fontSize="0.75rem" tickLine={false} axisLine={false} />
                <YAxis stroke="#6b7280" fontSize="0.75rem" tickLine={false} axisLine={false} />
                <Tooltip contentStyle={{ backgroundColor: '#fff', border: '1px solid #e5e7eb', borderRadius: '0.5rem' }}/>
                <Legend wrapperStyle={{fontSize: "0.875rem"}} />
                <Line type="monotone" dataKey="faturamento" stroke="#4f46e5" strokeWidth={2} name="Faturamento" dot={{ r: 4 }} activeDot={{ r: 6 }} />
                <Line type="monotone" dataKey="lucro" stroke="#10b981" strokeWidth={2} name="Lucro" dot={{ r: 4 }} activeDot={{ r: 6 }} />
              </LineChart>
            </ResponsiveContainer>
          </div>
        </ChartCard>

        <ActionsAndSalesContainer>
          <ActionCard>
            <h3 style={{ fontSize: '1.125rem', fontWeight: '600', color: '#1f2937', marginBottom: '1rem', marginTop: 0 }}>Ações Rápidas</h3>
            <div style={{ display: 'flex', flexDirection: 'column', gap: '0.75rem' }}>
              <PrimaryButton>
                <PlusCircle size={18} style={{ marginRight: '0.5rem' }} /> Nova Venda
              </PrimaryButton>
              <SecondaryButton>Adicionar Produto</SecondaryButton>
              <SecondaryButton>Ver Estoque</SecondaryButton>
            </div>
          </ActionCard>
          <SalesCard>
            <h3 style={{ fontSize: '1.125rem', fontWeight: '600', color: '#1f2937', marginBottom: '1rem', marginTop: 0 }}>Vendas Recentes</h3>
            <div style={{ display: 'flex', flexDirection: 'column', gap: '1rem' }}>
              {recentSales.map(sale => (
                <div key={sale.id} style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                  <div>
                    <p style={{ fontWeight: 600, color: '#1f2937', margin: 0 }}>{sale.customer}</p>
                    <p style={{ fontSize: '0.875rem', color: '#6b7280', margin: '0.25rem 0 0 0' }}>{sale.id}</p>
                  </div>
                  <div style={{ textAlign: 'right' }}>
                    <p style={{ fontWeight: 'bold', color: '#1f2937', margin: 0 }}>R$ {sale.amount.toFixed(2).replace('.',',')}</p>
                    <StatusBadge status={sale.status}>{sale.status}</StatusBadge>
                  </div>
                </div>
              ))}
            </div>
          </SalesCard>
        </ActionsAndSalesContainer>
      </MainLayoutGrid>
    </DashboardGrid>
  );
}