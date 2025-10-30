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
  color: ${props => props.theme.colors.textSecondary};

  &:hover:not(.active) {
    background-color: ${props => props.theme.colors.backgroundTertiary};
    color: ${props => props.theme.colors.text};
  }

  &.active {
    color: #ffffff;
    background-color: ${props => props.theme.colors.primary};
    box-shadow: 0 4px 6px -1px ${props => props.theme.colors.shadowMedium};
  }

  > svg {
    margin-right: 0.75rem;
  }
`;