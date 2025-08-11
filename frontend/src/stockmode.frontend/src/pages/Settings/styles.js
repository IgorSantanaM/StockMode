import styled from 'styled-components';

export const PageContainer = styled.div`
  padding: 2rem;
  background-color: #f3f5f7;
`;

export const Container = styled.div`
  max-width: 1000px;
  margin: 0 auto;
`;

export const TitleContainer = styled.div`
  display: flex;
  align-items: center;
  gap: 0.75rem;
  margin-bottom: 2rem;
`;

export const Title = styled.h2`
  font-size: 1.75rem;
  font-weight: bold;
  color: #1f2937;
  margin: 0;
`;

export const SettingsLayout = styled.div`
  display: flex;
  gap: 2rem;
  align-items: flex-start;

  @media (max-width: 768px) {
    flex-direction: column;
  }
`;

export const NavMenu = styled.div`
  display: flex;
  flex-direction: column;
  gap: 0.5rem;
  width: 200px;
  flex-shrink: 0;
`;

export const NavButton = styled.button`
  display: flex;
  align-items: center;
  gap: 0.75rem;
  padding: 0.75rem 1rem;
  border-radius: 0.5rem;
  border: none;
  background-color: ${({ active }) => (active ? '#eef2ff' : 'transparent')};
  color: ${({ active }) => (active ? '#4f46e5' : '#374151')};
  font-weight: 500;
  font-size: 1rem;
  text-align: left;
  cursor: pointer;
  transition: all 0.2s ease;

  &:hover {
    background-color: #eef2ff;
    color: #4f46e5;
  }
`;

export const Paragraph = styled.p`
    color: black;

`;

export const ContentCard = styled.div`
  flex-grow: 1;
  background-color: white;
  border-radius: 1rem;
  box-shadow: 0 4px 6px -1px rgb(0 0 0 / 0.1), 0 2px 4px -2px rgb(0 0 0 / 0.1);
  padding: 2rem;
`;

export const SectionTitle = styled.h3`
  font-size: 1.25rem;
  font-weight: 600;
  color: #374151;
  margin: 0 0 1.5rem 0;
  padding-bottom: 1rem;
  border-bottom: 1px solid #e5e7eb;
`;

export const Form = styled.form`
  display: flex;
  flex-direction: column;
  gap: 1.5rem;
`;

export const FormRow = styled.div`
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
  gap: 1.5rem;
`;

export const FormGroup = styled.div`
  display: flex;
  flex-direction: column;
`;

export const Label = styled.label`
  font-size: 0.875rem;
  font-weight: 500;
  color: #4b5563;
  margin-bottom: 0.5rem;
`;

export const Input = styled.input`
  padding: 0.75rem 1rem;
  border: 1px solid #d1d5db;
  border-radius: 0.5rem;
  font-size: 1rem;
  background-color: #f9fafb;
  color: black;
`;

export const ButtonContainer = styled.div`
  display: flex;
  justify-content: flex-end;
  margin-top: 1rem;
  padding-top: 1.5rem;
  border-top: 1px solid #e5e7eb;
`;

export const PrimaryButton = styled.button`
  padding: 0.625rem 1.25rem;
  background-color: #4f46e5;
  color: white;
  border: none;
  border-radius: 0.5rem;
  font-size: 0.875rem;
  font-weight: 600;
  cursor: pointer;
`;
