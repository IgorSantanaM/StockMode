import styled from "styled-components";

export const SidebarContainer = styled.aside`
  background-color: #ffffff;
  box-shadow: 0 2px 4px rgba(0,0,0,0.05);
  flex-shrink: 0;
  display: flex;
  flex-direction: column;
  width: 16rem;
  height: 49.5rem;
  border-right: 1px solid #e5e7eb;

  @media (max-width: 768px) {
    display: none;
  }
    
`;

export const SidebarHeader = styled.div`
  display: flex;
  align-items: center;
  justify-content: center;
  border-bottom: 3px solid #e5e7eb;
  gap: 0.5rem;
  padding-bottom: 8px;
  padding-top: 8px;
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

export const HomeButton = styled.button`
  width: 100%;
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 1.125rem;
  font-weight: 600;
  padding: 1rem 0;
  background-color: #ffffff;
  color: white;
  border-radius: 0.75rem;
  border: none;
  cursor: pointer;
  transition: all 0.2s ease-in-out;

  &:hover {
    background-color: #554fc9ff;
    transform: scale(1.02);
  }
`;