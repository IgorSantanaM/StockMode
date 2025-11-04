import styled from 'styled-components';

export const PageContainer = styled.div`
  padding: 2rem;
  background-color: ${props => props.theme.colors.background};
  transition: background-color 0.3s ease;
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
  color: ${props => props.theme.colors.text};
  margin: 0;
  transition: color 0.3s ease;
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
  background-color: ${({ active, theme }) => (active ? theme.colors.primaryLight : 'transparent')};
  color: ${({ active, theme }) => (active ? theme.colors.primary : theme.colors.text)};
  font-weight: 500;
  font-size: 1rem;
  text-align: left;
  cursor: pointer;
  transition: all 0.3s ease;

  &:hover {
    background-color: ${props => props.theme.colors.primaryLight};
    color: ${props => props.theme.colors.primary};
  }
`;

export const Paragraph = styled.p`
  color: ${props => props.theme.colors.text};
  transition: color 0.3s ease;
`;

export const ContentCard = styled.div`
  flex-grow: 1;
  background-color: ${props => props.theme.colors.backgroundSecondary};
  border-radius: 1rem;
  box-shadow: 0 4px 6px -1px ${props => props.theme.colors.shadow};
  padding: 2rem;
  transition: all 0.3s ease;
`;

export const SectionTitle = styled.h3`
  font-size: 1.25rem;
  font-weight: 600;
  color: ${props => props.theme.colors.text};
  margin: 0 0 1.5rem 0;
  padding-bottom: 1rem;
  border-bottom: 1px solid ${props => props.theme.colors.border};
  transition: color 0.3s ease, border-color 0.3s ease;
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
  color: ${props => props.theme.colors.textSecondary};
  margin-bottom: 0.5rem;
  transition: color 0.3s ease;
`;

export const Input = styled.input`
  padding: 0.75rem 1rem;
  border: 1px solid ${props => props.theme.colors.border};
  border-radius: 0.5rem;
  font-size: 1rem;
  background-color: ${props => props.theme.colors.backgroundTertiary};
  color: ${props => props.theme.colors.text};
  transition: all 0.3s ease;

  &:focus {
    outline: none;
    border-color: ${props => props.theme.colors.primary};
    box-shadow: 0 0 0 3px ${props => props.theme.colors.primaryLight};
  }

  &::placeholder {
    color: ${props => props.theme.colors.textTertiary};
  }
`;

export const ButtonContainer = styled.div`
  display: flex;
  justify-content: flex-end;
  margin-top: 1rem;
  padding-top: 1.5rem;
  border-top: 1px solid ${props => props.theme.colors.border};
  transition: border-color 0.3s ease;
`;

export const PrimaryButton = styled.button`
  padding: 0.625rem 1.25rem;
  background-color: ${props => props.theme.colors.primary};
  color: white;
  border: none;
  border-radius: 0.5rem;
  font-size: 0.875rem;
  font-weight: 600;
  cursor: pointer;
  transition: all 0.3s ease;

  &:hover {
    background-color: ${props => props.theme.colors.primaryHover};
  }
`;
