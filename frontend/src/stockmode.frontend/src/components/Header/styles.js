import styled from 'styled-components';

export const HeaderContainer = styled.header`
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

export const WelcomeMessage = styled.div`
  h1 {
    font-size: 1.5rem;
    font-weight: bold;
    color: #1f2937;
    margin: 0;
  }
  p {
    font-size: 0.875rem;
    color: #6b7280;
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
  border: 1px solid #d1d5db;
  background-color: #f9fafb;
  color: black;
  &:focus {
    outline: none;
    border-color: #4f46e5;
    box-shadow: 0 0 0 2px rgba(79, 70, 229, 0.2);
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
`;

export const UserAvatar = styled.img`
  height: 2.5rem;
  width: 2.5rem;
  border-radius: 9999px;
`;

export const UserName = styled.span`
  font-weight: 600;
  color: #374151;
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
  border: 1px solid #e5e7eb;
`;

export const DropdownLink = styled.a`
  display: flex;
  align-items: center;
  gap: 0.5rem;
  padding: 0.5rem 1rem;
  color: #374151;
  text-decoration: none;
  font-size: 0.875rem;

  &:hover {
    background-color: #f3f4f6;
  }
  &.logout {
    color: #dc2626;
    &:hover {
      background-color: #fee2e2;
    }
  }
`;
