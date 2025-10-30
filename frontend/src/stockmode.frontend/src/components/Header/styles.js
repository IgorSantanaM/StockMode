import styled from 'styled-components';

export const HeaderContainer = styled.header`
  background-color: ${props => props.theme.colors.backgroundSecondary};
  height: 80px;
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 0 2rem;
  border-bottom: 1px solid ${props => props.theme.colors.border};
  position: sticky;
  top: 0;
  z-index: 10;
  transition: background-color 0.3s ease, border-color 0.3s ease;
`;

export const WelcomeMessage = styled.div`
  h1 {
    font-size: 1.5rem;
    font-weight: bold;
    color: ${props => props.theme.colors.text};
    margin: 0;
  }
  p {
    font-size: 0.875rem;
    color: ${props => props.theme.colors.textSecondary};
    display: flex;
    align-items: center;
    margin-top: 0.25rem;
  }
`;

export const HeaderActions = styled.div`
  display: flex;
  align-items: center;
  gap: 1.5rem;
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
  border: 1px solid ${props => props.theme.colors.border};
  background-color: ${props => props.theme.colors.backgroundTertiary};
  color: ${props => props.theme.colors.text};
  transition: all 0.2s ease;
  
  &:focus {
    outline: none;
    border-color: ${props => props.theme.colors.primary};
    box-shadow: 0 0 0 2px ${props => props.theme.colors.primaryLight};
  }

  &::placeholder {
    color: ${props => props.theme.colors.textTertiary};
  }
`;

export const UserMenu = styled.div`
  position: relative;
`;

export const UserButton = styled.button`
  display: flex;
  align-items: center;
  gap: 0.5rem;
  background: none;
  border: none;
  cursor: pointer;
  color: ${props => props.theme.colors.text};
`;

export const UserAvatar = styled.img`
  height: 2.5rem;
  width: 2.5rem;
  border-radius: 9999px;
`;

export const UserName = styled.span`
  font-weight: 600;
  color: ${props => props.theme.colors.text};
`;

export const UserDropdown = styled.div`
  position: absolute;
  right: 0;
  margin-top: 0.5rem;
  width: 12rem;
  background-color: ${props => props.theme.colors.backgroundSecondary};
  border-radius: 0.5rem;
  box-shadow: 0 10px 15px -3px ${props => props.theme.colors.shadow};
  padding: 0.5rem 0;
  z-index: 20;
  border: 1px solid ${props => props.theme.colors.border};
`;

export const DropdownLink = styled.a`
  display: flex;
  align-items: center;
  gap: 0.5rem;
  padding: 0.5rem 1rem;
  color: ${props => props.theme.colors.text};
  text-decoration: none;
  transition: background-color 0.2s ease;
  
  &:hover {
    background-color: ${props => props.theme.colors.backgroundTertiary};
  }
  
  &.logout {
    color: ${props => props.theme.colors.danger};
    
    &:hover {
      background-color: ${props => props.theme.colors.dangerLight};
    }
  }
`;
