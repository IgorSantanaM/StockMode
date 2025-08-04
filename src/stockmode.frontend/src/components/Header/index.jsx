import React, {useState} from "react";    
import { HeaderContainer, SearchInputContainer, SearchInput, UserMenu,  UserDropdown  } from "./styles";
import { Calendar, Search, ChevronDown, LogOut } from "lucide-react";

const Header = () =>{
    const [userMenuOpen, setUserMenuOpen] = useState(false);
    const today = new Date().toLocaleDateString('pt-BR', { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' });
    
    return(
        <HeaderContainer>
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
        </HeaderContainer>
    );
}

export default Header;
