import styled from 'styled-components';

export const PageContainer = styled.div`
  display: flex;
  height: calc(100vh - 80px); // Altura total menos o header
  background-color: #f3f5f7;
  width: 94rem;
`;

export const LeftPanel = styled.div`
  width: 40%;
  padding: 2rem;
  display: flex;
  flex-direction: column;
  border-right: 1px solid #e5e7eb;
`;

export const RightPanel = styled.div`
  width: 43%;
  padding: 2rem;
  display: flex;
  flex-direction: column;
  background-color: #ffffff;
`;

export const SearchContainer = styled.div`
  position: relative;
  margin-bottom: 1.5rem;
  width: 27.8rem;
  color: black;
`;

export const Input = styled.input`
  width: 100%;
  padding: 1rem 1rem 1rem 3rem;
  border: 1px solid #d1d5db;
  border-radius: 0.5rem;
  font-size: 1rem;
  background-color: #ffffff;
  transition: all 0.2s ease-in-out;
  color: black;

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
  width: 50px;
  height: 50px;
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
  margin: 0.25rem 0 0 0;
`;

export const SectionTitle = styled.h3`
  font-size: 1.5rem;
  font-weight: bold;
  color: #1f2937;
  margin: 0 0 1.5rem 0;
  padding-bottom: 1rem;
  border-bottom: 1px solid #e5e7eb;
`;

export const ReceivedItemsList = styled.div`
  flex-grow: 1;
  overflow-y: auto;
`;

export const ReceivedItemCard = styled.div`
  display: flex;
  align-items: center;
  padding: 1rem 0;
  border-bottom: 1px solid #e5e7eb;
  gap: 1rem;
`;

export const QuantityInput = styled(Input)`
  width: 80px;
  padding: 0.5rem;
  text-align: center;
`;

export const FinalizeButton = styled.button`
  width: 100%;
  padding: 1rem;
  background-color: #4f46e5;
  color: white;
  border: none;
  border-radius: 0.5rem;
  font-size: 1.125rem;
  font-weight: 600;
  cursor: pointer;
  margin-top: 1.5rem;
  transition: background-color 0.2s ease;

  &:hover {
    background-color: #4338ca;
  }
  &:disabled {
    background-color: #a5b4fc;
    cursor: not-allowed;
  }
`;
