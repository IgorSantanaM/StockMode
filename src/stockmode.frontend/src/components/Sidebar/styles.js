import styled from "styled-components";

export const SidebarContainer = styled.aside`
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