import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Search, Calendar, ChevronDown, LogOut } from 'lucide-react';
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
  const [userMenuOpen, setUserMenuOpen] = useState(false);
  const [searchTerm, setSearchTerm] = useState(''); // Estado para guardar o termo de busca
  const today = new Date().toLocaleDateString('pt-BR', { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' });
  var navigate = useNavigate();
  const handleSearch = (e) => {
    if (e.key === 'Enter' && searchTerm.trim() !== '') {
      console.log('Buscando por:', searchTerm);
    }
  };

  return (
    <HeaderContainer>
      <WelcomeMessage>
        <h1>Olá, Igor!</h1>
        <p>
          <Calendar size={16} style={{ marginRight: '0.5rem' }} /> {today}
        </p>
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
        <UserMenu>
          <UserButton onClick={() => setUserMenuOpen(!userMenuOpen)}>
            <UserAvatar src="https://i.pravatar.cc/40" alt="Avatar do Usuário" />
            <UserName>Igor Medeiros</UserName>
            <ChevronDown size={20} style={{ color: '#6b7280' }} />
          </UserButton>
          {userMenuOpen && (
            <UserDropdown onMouseLeave={() => setUserMenuOpen(false)}>
              <DropdownLink href="#" onClick={() => navigate("/profile")}>Meu Perfil</DropdownLink>
              <DropdownLink href="#" onClick={() =>  navigate("/Help")}>Ajuda</DropdownLink>
              <div style={{ borderTop: '1px solid #e5e7eb', margin: '0.5rem 0' }}></div>
              <DropdownLink href="#" className="logout">
                <LogOut size={20} /> Sair
              </DropdownLink>
            </UserDropdown>
          )}
        </UserMenu>
      </HeaderActions>
    </HeaderContainer>
  );
};

export default Header;
