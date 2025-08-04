import React from 'react';
import styled from 'styled-components';

export const AppContainer = styled.div`
  display: flex;
`;

export const MainContent = styled.main`
  flex: 1;
  display: flex;
  flex-direction: column;
  overflow-y: auto;
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