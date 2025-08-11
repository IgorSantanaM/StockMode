import styled from 'styled-components';
import { NavLink, Link } from 'react-router-dom';

export const Backdrop = styled.div`
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.5);
  z-index: 100;
  opacity: ${({ isOpen }) => (isOpen ? 1 : 0)};
  visibility: ${({ isOpen }) => (isOpen ? 'visible' : 'hidden')};
  transition: opacity 0.3s ease-in-out;

  @media (min-width: 769px) {
    display: none; // O backdrop só existe em ecrãs móveis
  }
`;

export const SidebarContainer = styled.aside`
  background-color: #ffffff;
  box-shadow: 0 2px 4px rgba(0,0,0,0.05);
  flex-shrink: 0;
  display: flex;
  flex-direction: column;
  width: 256px;
  height: 100vh;
  border-right: 1px solid #e5e7eb;
  transition: transform 0.3s ease-in-out;
  z-index: 101;

  @media (max-width: 768px) {
    position: fixed;
    left: 0;
    top: 0;
    transform: ${({ isOpen }) => (isOpen ? 'translateX(0)' : 'translateX(-100%)')};
  }
`;

// ... (resto dos seus estilos da Sidebar: SidebarHeader, LogoText, Nav, etc.)
export const SidebarHeader = styled(Link)`
  height: 80px;
  display: flex;
  align-items: center;
  justify-content: center;
  border-bottom: 1px solid #e5e7eb;
  gap: 0.5rem;
  text-decoration: none;
`;

export const LogoText = styled.span`
  font-size: 1.5rem;
  font-weight: bold;
  color: #1f2937;
`;

export const Nav = styled.nav`
  flex-grow: 1;
  padding: 1rem;
  display: flex;
  flex-direction: column;
  gap: 0.5rem;
`;
