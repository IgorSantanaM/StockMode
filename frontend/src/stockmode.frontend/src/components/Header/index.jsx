import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Search, Calendar, ChevronDown, LogOut } from 'lucide-react';
import { useAuth } from 'react-oidc-context';
import ThemeToggle from '../ThemeToggle';
import {
  HeaderContainer,
  WelcomeMessage,
  HeaderActions,
  SearchInputContainer,
  SearchInput,
  UserMenu,
  UserButton,
  UserAvatar,
  UserName,
  UserDropdown,
  DropdownLink
} from './styles';

const Header = () => {
  const auth = useAuth();
  const [userMenuOpen, setUserMenuOpen] = useState(false);
  const [searchTerm, setSearchTerm] = useState(''); // Estado para guardar o termo de busca
  const navigate = useNavigate();
  
  const handleSearch = (e) => {
    if (e.key === 'Enter' && searchTerm.trim() !== '') {
      // Em uma aplicação real, você navegaria para uma página de resultados.
      console.log('Buscando por:', searchTerm);
      // navigate(`/search?q=${searchTerm}`);
    }
  };

  const handleLogout = async () => {
    try {
      await auth.signoutRedirect();
    } catch (error) {
      console.error('Logout error:', error);
    }
  };

  const userName = auth.user?.profile?.given_name || auth.user?.profile?.name || 'Usuário';
  const userEmail = auth.user?.profile?.email || '';


  return (
    <HeaderContainer>
      <WelcomeMessage>
        <h1>Olá, {userName}!</h1>
      </WelcomeMessage>
      <HeaderActions>
        <SearchInputContainer>
          <Search style={{ position: 'absolute', left: '0.75rem', top: '50%', transform: 'translateY(-50%)', height: '1.25rem', width: '1.25rem', color: '#9ca3af' }} />
          <SearchInput 
            type="text" 
            placeholder="Buscar..."
            value={searchTerm}
            onChange={(e) => setSearchTerm(e.target.value)}
            onKeyDown={handleSearch}
          />
        </SearchInputContainer>
        <ThemeToggle />
        <UserMenu>
          <UserButton onClick={() => setUserMenuOpen(!userMenuOpen)}>
            <UserAvatar src="https://i.pravatar.cc/40" alt="Avatar do Usuário" />
            <UserName>{userName}</UserName>
            <ChevronDown size={20} style={{ color: '#6b7280' }} />
          </UserButton>
          {userMenuOpen && (
            <UserDropdown onMouseLeave={() => setUserMenuOpen(false)}>
              <DropdownLink href="#" onClick={() => navigate("/profile")}>Meu Perfil</DropdownLink>
              <DropdownLink href="#" onClick={() => navigate("/help")}>Ajuda</DropdownLink>
              <div style={{ borderTop: '1px solid #e5e7eb', margin: '0.5rem 0' }}></div>
              <DropdownLink href="#" className="logout" onClick={handleLogout}>
                <LogOut size={16} /> Sair
              </DropdownLink>
            </UserDropdown>
          )}
        </UserMenu>
      </HeaderActions>
    </HeaderContainer>
  );
};

export default Header;
