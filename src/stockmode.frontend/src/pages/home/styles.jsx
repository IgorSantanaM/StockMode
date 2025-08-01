import React from 'react';
import styled, { createGlobalStyle } from 'styled-components';

export const GlobalStyle = createGlobalStyle`
  body {
    margin: 0;
    font-family: 'Roboto', -apple-system, BlinkMacSystemFont, 'Segoe UI', 'Oxygen',
      'Ubuntu', 'Cantarell', 'Fira Sans', 'Droid Sans', 'Helvetica Neue',
      sans-serif;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    background-color: #f3f5f7;
  }
`;

export const AppContainer = styled.div`
  display: flex;
  height: 100vh;
`;

export const SidebarContainer = styled.aside`
  width: 256px;
  background-color: #ffffff;
  box-shadow: 0 2px 4px rgba(0,0,0,0.05);
  flex-shrink: 0;
  display: flex;
  flex-direction: column;
  border-right: 1px solid #e5e7eb;

  @media (max-width: 768px) {
    display: none;
  }
`;

export const SidebarHeader = styled.div`
  height: 80px;
  display: flex;
  align-items: center;
  justify-content: center;
  border-bottom: 1px solid #e5e7eb;
  gap: 0.5rem;
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

export const NavItemLink = styled.a`
  display: flex;
  align-items: center;
  padding: 0.75rem 1rem;
  font-size: 1rem;
  border-radius: 0.5rem;
  transition: all 0.2s ease-in-out;
  text-decoration: none;
  color: ${props => props.active ? '#ffffff' : '#4b5563'};
  background-color: ${props => props.active ? '#4f46e5' : 'transparent'};
  box-shadow: ${props => props.active ? '0 4px 6px -1px rgb(0 0 0 / 0.1), 0 2px 4px -2px rgb(0 0 0 / 0.1)' : 'none'};

  &:hover {
    background-color: ${props => props.active ? '#4338ca' : '#f3f4f6'};
  }

  > svg {
    margin-right: 0.75rem;
  }
`;

export const MainContent = styled.main`
  flex: 1;
  display: flex;
  flex-direction: column;
  overflow-y: auto;
`;

export const Header = styled.header`
  background-color: #ffffff;
  height: 80px;
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 0 2rem;
  border-bottom: 1px solid #e5e7eb;
  position: sticky;
  top: 0;
  z-index: 10;
`;

export const SearchInputContainer = styled.div`
  position: relative;
`;

export const SearchInput = styled.input`
  padding-left: 2.5rem;
  padding-right: 1rem;
  padding-top: 0.5rem;
  padding-bottom: 0.5rem;
  width: 256px;
  border-radius: 0.5rem;
  border: 1px solid #d1d5db;
  background-color: #f9fafb;
  &:focus {
    outline: none;
    ring: 2px;
    border-color: #4f46e5;
  }
`;

export const UserMenu = styled.div`
  position: relative;
`;

export const UserDropdown = styled.div`
  position: absolute;
  right: 0;
  margin-top: 0.5rem;
  width: 12rem;
  background-color: white;
  border-radius: 0.5rem;
  box-shadow: 0 10px 15px -3px rgb(0 0 0 / 0.1), 0 4px 6px -4px rgb(0 0 0 / 0.1);
  padding: 0.5rem 0;
  z-index: 20;
`;

export const DashboardGrid = styled.div`
  padding: 2rem;
  flex-grow: 1;
`;

export const StatsGrid = styled.div`
  display: grid;
  grid-template-columns: repeat(1, 1fr);
  gap: 1.5rem;
  margin-bottom: 2rem;
  @media (min-width: 768px) { grid-template-columns: repeat(2, 1fr); }
  @media (min-width: 1280px) { grid-template-columns: repeat(4, 1fr); }
`;

export const Card = styled.div`
  background-color: white;
  padding: 1.5rem;
  border-radius: 1rem;
  box-shadow: 0 1px 2px 0 rgb(0 0 0 / 0.05);
`;

export const ChartCard = styled(Card)`
  grid-column: span 1 / span 1;
  @media (min-width: 1280px) { grid-column: span 2 / span 2; }
`;

export const PrimaryButton = styled.button`
  width: 100%;
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 1.125rem;
  font-weight: 600;
  padding: 1rem 0;
  background-color: #4f46e5;
  color: white;
  border-radius: 0.75rem;
  border: none;
  cursor: pointer;
  box-shadow: 0 4px 6px -1px rgb(0 0 0 / 0.1), 0 2px 4px -2px rgb(0 0 0 / 0.1);
  transition: all 0.2s ease-in-out;

  &:hover {
    background-color: #4338ca;
    transform: scale(1.02);
  }
`;

export const SecondaryButton = styled(PrimaryButton)`
  font-size: 1rem;
  padding: 0.75rem 0;
  background-color: #e5e7eb;
  color: #1f2937;
  box-shadow: none;

  &:hover {
    background-color: #d1d5db;
    transform: none;
  }
`;

export const StatusBadge = styled.span`
  font-size: 0.75rem;
  font-weight: 600;
  padding: 0.25rem 0.5rem;
  border-radius: 9999px;
  background-color: ${props => props.status === 'Concluída' ? '#d1fae5' : '#fef3c7'};
  color: ${props => props.status === 'Concluída' ? '#065f46' : '#92400e'};
`;