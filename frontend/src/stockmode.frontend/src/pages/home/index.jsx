import React, { useState, useEffect } from 'react';
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
import { useNavigate } from 'react-router-dom';
import api from '../../services/api.jsx';
import { LoadingContainer } from '../../util/LoadingContainer.js';

const SALE_STATUS = {
  1: 'Pendente',
  2: 'Concluída',
  3: 'Cancelada'
};

export default function Home() {
  const navigate = useNavigate();
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  
  const [dailySalesData, setDailySalesData] = useState([]);
  const [recentSales, setRecentSales] = useState([]);
  const [statCardsData, setStatCardsData] = useState({
    vendasHoje: { value: 'R$ 0,00', change: '+0%', isPositive: true, icon: <DollarSign size={20} /> },
    lucroHoje: { value: 'R$ 0,00', change: '+0%', isPositive: true, icon: <ArrowUpRight size={20} /> },
    ticketMedio: { value: 'R$ 0,00', change: '+0%', isPositive: true, icon: <ShoppingCart size={20} /> },
    baixoEstoque: { value: '0', change: 'itens', isPositive: null, icon: <Package size={20} /> },
  });

  const formatCurrency = (value) => value.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });
  const formatDate = (dateString) => new Intl.DateTimeFormat('pt-BR', { timeZone: 'UTC' }).format(new Date(dateString));

  useEffect(() => {
    fetchDashboardData();
  }, []);

  const fetchDashboardData = async () => {
    try {
      setLoading(true);
      setError(null);

      const now = new Date();
      const today = new Date(now.getFullYear(), now.getMonth(), now.getDate());
      const sevenDaysAgo = new Date(now.getTime() - 7 * 24 * 60 * 60 * 1000);

      // Fetch sales for last 7 days
      const weekSalesResponse = await api.get('/sales', {
        params: {
          startDate: sevenDaysAgo.toISOString(),
          endDate: now.toISOString(),
          page: 1,
          pageSize: 1000
        }
      });

      const weekSales = weekSalesResponse.data?.items || weekSalesResponse.data || [];

      // Filter today's sales
      const todaySales = weekSales.filter(sale => {
        const saleDate = new Date(sale.saleDate);
        return saleDate >= today && (sale.status === 2 || sale.status === 'Completed');
      });

      // Calculate today's stats
      const todayRevenue = todaySales.reduce((sum, sale) => sum + (sale.finalPrice || 0), 0);
      const todayProfit = todayRevenue * 0.35; // Assuming 35% profit margin
      const todayTransactions = todaySales.length;
      const avgTicket = todayTransactions > 0 ? todayRevenue / todayTransactions : 0;

      // Fetch products for low stock count
      let lowStockCount = 0;
      try {
        const productsResponse = await api.get('/products', {
          params: { page: 1, pageSize: 1000 }
        });
        const products = productsResponse.data?.items || productsResponse.data || [];
        
        // Count products with low stock (assuming stockQuantity <= 10)
        lowStockCount = products.reduce((count, product) => {
          const variations = product.variations || [];
          const lowStockVariations = variations.filter(v => v.stockQuantity <= 10 && v.stockQuantity > 0);
          return count + lowStockVariations.length;
        }, 0);
      } catch (err) {
        console.warn('Could not fetch products for low stock count:', err);
      }

      // Update stat cards
      setStatCardsData({
        vendasHoje: { 
          value: formatCurrency(todayRevenue), 
          change: `${todayTransactions} vendas`, 
          isPositive: true, 
          icon: <DollarSign size={20} /> 
        },
        lucroHoje: { 
          value: formatCurrency(todayProfit), 
          change: '+35%', 
          isPositive: true, 
          icon: <ArrowUpRight size={20} /> 
        },
        ticketMedio: { 
          value: formatCurrency(avgTicket), 
          change: todayTransactions > 0 ? `${todayTransactions} vendas` : 'Sem vendas', 
          isPositive: true, 
          icon: <ShoppingCart size={20} /> 
        },
        baixoEstoque: { 
          value: lowStockCount.toString(), 
          change: 'itens', 
          isPositive: null, 
          icon: <Package size={20} /> 
        },
      });

      // Process daily sales data for chart
      const dailyData = {};
      const dayNames = ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sáb'];
      
      for (let i = 6; i >= 0; i--) {
        const date = new Date(now.getTime() - i * 24 * 60 * 60 * 1000);
        const dayName = dayNames[date.getDay()];
        dailyData[dayName] = { name: dayName, faturamento: 0, lucro: 0 };
      }

      weekSales.forEach(sale => {
        if (sale.status === 2 || sale.status === 'Completed') {
          const saleDate = new Date(sale.saleDate);
          const dayName = dayNames[saleDate.getDay()];
          if (dailyData[dayName]) {
            dailyData[dayName].faturamento += sale.finalPrice || 0;
            dailyData[dayName].lucro += (sale.finalPrice || 0) * 0.35; // 35% profit margin
          }
        }
      });

      setDailySalesData(Object.values(dailyData));

      // Get recent sales
      const recent = weekSales
        .filter(sale => sale.status === 2 || sale.status === 'Completed')
        .sort((a, b) => new Date(b.saleDate) - new Date(a.saleDate))
        .slice(0, 4)
        .map(sale => ({
          id: `VENDA-${sale.id.toString().padStart(6, '0')}`,
          customer: sale.customerName || 'Cliente Balcão',
          amount: sale.finalPrice || 0,
          status: SALE_STATUS[sale.status] || 'Concluída'
        }));

      setRecentSales(recent);

    } catch (err) {
      console.error('Error fetching dashboard data:', err);
      setError('Erro ao carregar dados do dashboard');
    } finally {
      setLoading(false);
    }
  };

  if (loading) {
    return <LoadingContainer>Carregando dashboard...</LoadingContainer>;
  }

  if (error) {
    return (
      <DashboardGrid>
        <div style={{ textAlign: 'center', padding: '2rem', color: '#ef4444' }}>
          <p>{error}</p>
          <button onClick={fetchDashboardData} style={{ marginTop: '1rem', padding: '0.5rem 1rem', cursor: 'pointer' }}>
            Tentar Novamente
          </button>
        </div>
      </DashboardGrid>
    );
  }

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
              <PrimaryButton onClick={() => navigate("/sales/new")}>
                <PlusCircle size={18} style={{ marginRight: '0.5rem' }} /> Nova Venda
              </PrimaryButton>
              <SecondaryButton onClick={() => navigate("/products/create")}>Adicionar Produto</SecondaryButton>
              <SecondaryButton onClick={() => navigate("/stock")}>Ver Estoque</SecondaryButton>
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