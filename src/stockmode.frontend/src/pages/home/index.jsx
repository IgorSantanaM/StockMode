import React, { useState } from 'react';
import { BarChart, Bar, XAxis, YAxis, CartesianGrid, Tooltip, Legend, ResponsiveContainer, LineChart, Line } from 'recharts';
import { Home, ShoppingCart, Package, DollarSign, Users, Settings, LogOut, ChevronDown, PlusCircle, Search, Calendar, ArrowUpRight, ArrowDownRight, Zap } from 'lucide-react';
import { AppContainer, SidebarContainer, SidebarHeader, LogoText, Nav, MainContent, Header, SearchInputContainer, SearchInput, UserMenu, DashboardGrid, StatsGrid, ChartCard, Card, PrimaryButton, SecondaryButton, StatusBadge, NavItemLink} from './styles.jsx';

const StatCardComponent = ({ title, value, change, isPositive, icon }) => (
  <Card>
    <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: '0.5rem' }}>
      <h3 style={{ fontSize: '1rem', fontWeight: 500, color: '#6b7280' }}>{title}</h3>
      {icon}
    </div>
    <div>
      <p style={{ fontSize: '1.875rem', fontWeight: 'bold', color: '#1f2937' }}>{value}</p>
      {change && (
        <div style={{ display: 'flex', alignItems: 'center', fontSize: '0.875rem', marginTop: '0.25rem' }}>
          {isPositive !== null ? (isPositive ? <ArrowUpRight style={{ height: '1rem', width: '1rem', color: '#10b981' }} /> : <ArrowDownRight style={{ height: '1rem', width: '1rem', color: '#ef4444' }} />) : null}
          <span style={{ marginLeft: '0.25rem', color: isPositive === null ? '#6b7280' : (isPositive ? '#059669' : '#dc2626') }}>
            {change}
          </span>
        </div>
      )}
    </div>
  </Card>
);

const NavItemComponent = ({ icon, label, active }) => (
  <NavItemLink href="#" active={active}>
    {icon}
    <span>{label}</span>
  </NavItemLink>
);
const dailySalesData = [
  { name: 'Seg', faturamento: 1200, lucro: 450 }, { name: 'Ter', faturamento: 1900, lucro: 700 }, { name: 'Qua', faturamento: 1500, lucro: 550 }, { name: 'Qui', faturamento: 2800, lucro: 1100 }, { name: 'Sex', faturamento: 2390, lucro: 950 }, { name: 'Sáb', faturamento: 3490, lucro: 1400 }, { name: 'Dom', faturamento: 980, lucro: 350 },
];
const recentSales = [
  { id: 'VENDA-00125', customer: 'Ana Clara', amount: 125.50, status: 'Concluída' }, { id: 'VENDA-00124', customer: 'Marcos Silva', amount: 89.90, status: 'Concluída' }, { id: 'VENDA-00123', customer: 'Cliente Balcão', amount: 45.00, status: 'Concluída' }, { id: 'VENDA-00122', customer: 'Juliana Costa', amount: 210.00, status: 'Pendente' },
];
const statCardsData = {
  vendasHoje: { value: 'R$ 1.250,75', change: '+15%', isPositive: true }, lucroHoje: { value: 'R$ 480,30', change: '+12%', isPositive: true }, ticketMedio: { value: 'R$ 83,38', change: '-2%', isPositive: false }, baixoEstoque: { value: '8', change: 'itens', isPositive: null },
};


export default function App() {
  const [userMenuOpen, setUserMenuOpen] = useState(false);
  const today = new Date().toLocaleDateString('pt-BR', { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' });

  return (
    <>
      <AppContainer>
        <SidebarContainer>
          <SidebarHeader>
            <Zap style={{ height: '2rem', width: '2rem', color: '#4f46e5' }} />
            <LogoText>VendeFácil</LogoText>
          </SidebarHeader>
          <Nav>
            <NavItemComponent icon={<Home size={20} />} label="Início" active />
            <NavItemComponent icon={<ShoppingCart size={20} />} label="Vendas" />
            <NavItemComponent icon={<Package size={20} />} label="Produtos" />
            <NavItemComponent icon={<DollarSign size={20} />} label="Financeiro" />
            <NavItemComponent icon={<Users size={20} />} label="Clientes" />
          </Nav>
          <div style={{ padding: '1rem', borderTop: '1px solid #e5e7eb' }}>
            <NavItemComponent icon={<Settings size={20} />} label="Configurações" />
          </div>
        </SidebarContainer>

        <MainContent>
          <Header>
            <div>
              <h1 style={{ fontSize: '1.5rem', fontWeight: 'bold', color: '#1f2937' }}>Olá, Igor!</h1>
              <p style={{ fontSize: '0.875rem', color: '#6b7280', display: 'flex', alignItems: 'center', marginTop: '0.25rem' }}>
                <Calendar size={16} style={{ marginRight: '0.5rem' }} /> {today}
              </p>
            </div>
            <div style={{ display: 'flex', alignItems: 'center', gap: '1.5rem' }}>
              <SearchInputContainer>
                <Search style={{ position: 'absolute', left: '0.75rem', top: '50%', transform: 'translateY(-50%)', height: '1.25rem', width: '1.25rem', color: '#9ca3af' }} />
                <SearchInput type="text" placeholder="Buscar..." />
              </SearchInputContainer>
              <UserMenu>
                <button onClick={() => setUserMenuOpen(!userMenuOpen)} style={{ display: 'flex', alignItems: 'center', gap: '0.5rem', background: 'none', border: 'none', cursor: 'pointer' }}>
                  <img src="https://i.pravatar.cc/40" alt="Avatar do Usuário" style={{ height: '2.5rem', width: '2.5rem', borderRadius: '9999px' }} />
                  <span style={{ fontWeight: 600, color: '#374151' }}>Igor Medeiros</span>
                  <ChevronDown size={20} style={{ color: '#6b7280' }} />
                </button>
                {userMenuOpen && (
                  <UserDropdown>
                    <a href="#" style={{ display: 'block', padding: '0.5rem 1rem', color: '#374151', textDecoration: 'none', '&:hover': { backgroundColor: '#f3f4f6' }}}>Meu Perfil</a>
                    <a href="#" style={{ display: 'block', padding: '0.5rem 1rem', color: '#374151', textDecoration: 'none', '&:hover': { backgroundColor: '#f3f4f6' }}}>Ajuda</a>
                    <div style={{ borderTop: '1px solid #e5e7eb', margin: '0.5rem 0' }}></div>
                    <a href="#" style={{ display: 'flex', alignItems: 'center', padding: '0.5rem 1rem', color: '#dc2626', textDecoration: 'none', '&:hover': { backgroundColor: '#fee2e2' }}}>
                      <LogOut size={20} style={{ marginRight: '0.5rem' }} /> Sair
                    </a>
                  </UserDropdown>
                )}
              </UserMenu>
            </div>
          </Header>

          <DashboardGrid>
            <StatsGrid>
              <StatCardComponent title="Vendas Hoje" value={statCardsData.vendasHoje.value} change={statCardsData.vendasHoje.change} isPositive={statCardsData.vendasHoje.isPositive} icon={<DollarSign size={24} style={{ color: '#9ca3af' }} />} />
              <StatCardComponent title="Lucro Hoje" value={statCardsData.lucroHoje.value} change={statCardsData.lucroHoje.change} isPositive={statCardsData.lucroHoje.isPositive} icon={<ArrowUpRight size={24} style={{ color: '#9ca3af' }} />} />
              <StatCardComponent title="Ticket Médio" value={statCardsData.ticketMedio.value} change={statCardsData.ticketMedio.change} isPositive={statCardsData.ticketMedio.isPositive} icon={<ShoppingCart size={24} style={{ color: '#9ca3af' }} />} />
              <StatCardComponent title="Itens em Baixo Estoque" value={statCardsData.baixoEstoque.value} change={statCardsData.baixoEstoque.change} isPositive={statCardsData.baixoEstoque.isPositive} icon={<Package size={24} style={{ color: '#9ca3af' }} />} />
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
                    <PrimaryButton>
                      <PlusCircle size={24} style={{ marginRight: '0.75rem' }} /> Nova Venda
                    </PrimaryButton>
                    <SecondaryButton>Adicionar Produto</SecondaryButton>
                    <SecondaryButton>Ver Estoque</SecondaryButton>
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