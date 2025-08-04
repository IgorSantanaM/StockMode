import styled from "styled-components";

export const HeaderContainer = styled.header`
  background-color: #ffffff;
  width: 180vh;
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
  color: black;
  &:focus {
    outline: none;
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