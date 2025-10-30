import styled from 'styled-components';
import { NavLink, Link } from 'react-router-dom';

export const Backdrop = styled.div`
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: ${props => props.theme.colors.overlay};
  z-index: 100;
  opacity: ${({ isOpen }) => (isOpen ? 1 : 0)};
  visibility: ${({ isOpen }) => (isOpen ? 'visible' : 'hidden')};
  transition: opacity 0.3s ease-in-out;

  @media (min-width: 769px) {
    display: none;
  }
`;

export const SidebarContainer = styled.aside`
  background-color: ${props => props.theme.colors.backgroundSecondary};
  box-shadow: 0 2px 4px ${props => props.theme.colors.shadow};
  flex-shrink: 0;
  display: flex;
  flex-direction: column;
  width: 256px;
  height: 100vh;
  border-right: 1px solid ${props => props.theme.colors.border};
  transition: transform 0.3s ease-in-out, background-color 0.3s ease, border-color 0.3s ease;
  z-index: 101;

  @media (max-width: 768px) {
    position: fixed;
    left: 0;
    top: 0;
    transform: ${({ isOpen }) => (isOpen ? 'translateX(0)' : 'translateX(-100%)')};
  }
`;

export const SidebarHeader = styled(Link)`
  height: 80px;
  display: flex;
  align-items: center;
  justify-content: center;
  border-bottom: 1px solid ${props => props.theme.colors.border};
  gap: 0.5rem;
  text-decoration: none;
  transition: border-color 0.3s ease;
`;

export const LogoText = styled.span`
  font-size: 1.5rem;
  font-weight: bold;
  color: ${props => props.theme.colors.text};
  transition: color 0.3s ease;
`;

export const Nav = styled.nav`
  flex-grow: 1;
  padding: 1rem;
  display: flex;
  flex-direction: column;
  gap: 0.5rem;
`;
