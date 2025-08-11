import styled from "styled-components";
import { NavLink } from 'react-router-dom';

export const NavItemLink = styled(NavLink)`
  display: flex;
  align-items: center;
  padding: 0.75rem 1rem;
  font-size: 1rem;
  border-radius: 0.5rem;
  transition: all 0.2s ease-in-out;
  text-decoration: none;
  color: #4b5563; // Cor padrão para o link inativo

  // Estilo para o estado de hover quando o link NÃO está ativo.
  &:hover:not(.active) {
    background-color: #f3f4f6;
  }

  // Estilo aplicado automaticamente pelo NavLink quando a rota está ativa.
  &.active {
    color: #ffffff;
    background-color: #4f46e5;
    box-shadow: 0 4px 6px -1px rgb(0 0 0 / 0.1), 0 2px 4px -2px rgb(0 0 0 / 0.1);
  }

  > svg {
    margin-right: 0.75rem;
  }
`;