import styled from 'styled-components';

export const PageContainer = styled.div`
  display: flex;
  height: calc(100vh - 80px); // Altura total menos o header
  background-color: #f3f5f7;
`;

export const LeftPanel = styled.div`
  width: 60%;
  padding: 2rem;
  display: flex;
  flex-direction: column;
  border-right: 1px solid #e5e7eb;
`;

export const RightPanel = styled.div`
  width: 40%;
  padding: 2rem;
  display: flex;
  flex-direction: column;
  background-color: #ffffff;
`;

export const SearchContainer = styled.div`
  position: relative;
  margin-bottom: 1.5rem;
`;

export const Input = styled.input`
  width: 100%;
  padding: 1rem 1rem 1rem 3rem;
  border: 1px solid #d1d5db;
  border-radius: 0.5rem;
  font-size: 1rem;
  background-color: #ffffff;
  transition: all 0.2s ease-in-out;

  &:focus {
    outline: none;
    border-color: #4f46e5;
    box-shadow: 0 0 0 3px rgba(79, 70, 229, 0.1);
  }
`;

export const SearchResultsContainer = styled.div`
  flex-grow: 1;
  overflow-y: auto;
  margin: 1rem -0.5rem 0;
  padding-right: 0.5rem;
`;

export const ProductCard = styled.div`
  display: flex;
  align-items: center;
  padding: 1rem;
  background-color: #ffffff;
  border: 1px solid #e5e7eb;
  border-radius: 0.75rem;
  margin-bottom: 1rem;
  cursor: pointer;
  transition: all 0.2s ease-in-out;

  &:hover {
    border-color: #4f46e5;
    box-shadow: 0 4px 6px -1px rgb(0 0 0 / 0.1), 0 2px 4px -2px rgb(0 0 0 / 0.1);
    transform: translateY(-2px);
  }
`;

export const ProductImage = styled.img`
  width: 60px;
  height: 60px;
  border-radius: 0.5rem;
  object-fit: cover;
  margin-right: 1rem;
`;

export const ProductInfo = styled.div`
  flex-grow: 1;
`;

export const ProductName = styled.p`
  font-weight: 600;
  color: #1f2937;
  margin: 0;
`;

export const ProductSku = styled.p`
  font-size: 0.875rem;
  color: #6b7280;
  margin: 0.25rem 0;
`;

export const ProductPrice = styled.p`
  font-weight: bold;
  color: #10b981;
  margin: 0;
`;

export const SectionTitle = styled.h3`
  font-size: 1.5rem;
  font-weight: bold;
  color: #1f2937;
  margin: 0 0 1.5rem 0;
  padding-bottom: 1rem;
  border-bottom: 1px solid #e5e7eb;
`;

export const SaleItemsList = styled.div`
  flex-grow: 1;
  overflow-y: auto;
  margin-right: -1rem;
  padding-right: 1rem;
`;

export const SaleItemCard = styled.div`
  display: flex;
  align-items: center;
  padding: 1rem 0;
  border-bottom: 1px solid #e5e7eb;
`;

export const QuantityControl = styled.div`
  display: flex;
  align-items: center;
  gap: 0.5rem;
  margin-right: 1rem;
`;

export const QuantityButton = styled.button`
  width: 28px;
  height: 28px;
  border-radius: 50%;
  border: 1px solid #d1d5db;
  background-color: #ffffff;
  color: #4b5563;
  font-size: 1.25rem;
  display: flex;
  align-items: center;
  justify-content: center;
  cursor: pointer;
  &:hover {
    background-color: #f3f4f6;
  }
`;

export const SummaryContainer = styled.div`
  padding-top: 1.5rem;
  border-top: 1px solid #e5e7eb;
`;

export const SummaryRow = styled.div`
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 0.75rem;
  font-size: 1rem;
  color: #4b5563;
`;

export const TotalRow = styled(SummaryRow)`
  font-size: 1.25rem;
  font-weight: bold;
  color: #1f2937;
  margin-top: 1rem;
`;

export const FinalizeButton = styled.button`
  width: 100%;
  padding: 1rem;
  background-color: #10b981;
  color: white;
  border: none;
  border-radius: 0.5rem;
  font-size: 1.125rem;
  font-weight: 600;
  cursor: pointer;
  margin-top: 1.5rem;
  transition: background-color 0.2s ease;

  &:hover {
    background-color: #059669;
  }
  &:disabled {
    background-color: #6ee7b7;
    cursor: not-allowed;
  }
`;

export const Select = styled.select`
  padding: 0.5rem;
  border: 1px solid #d1d5db;
  border-radius: 0.5rem;
  font-size: 0.875rem;
  background-color: #ffffff;
`;
