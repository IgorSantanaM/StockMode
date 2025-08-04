import styled from "styled-components";

export const NavItemLink = styled.a`
  display: flex;
  align-items: center;
  padding: 0.75rem 1rem;
  font-size: 1rem;
  border-radius: 0.5rem;
  transition: all 0.2s ease-in-out;
  text-decoration: none;
  color: ${props => props.active ? '#ffffff' : '#4b5563'};
  background-color: ${props => props.active ? '#4840e6ff' : 'transparent'};
  box-shadow: ${props => props.active ? '0 4px 6px -1px rgb(0 0 0 / 0.1), 0 2px 4px -2px rgb(0 0 0 / 0.1)' : 'none'};

  &:hover {
    background-color: ${props => props.active ? '#4338ca' : '#f3f4f6'};
  }

  > svg {
    margin-right: 0.75rem;
  }
`;