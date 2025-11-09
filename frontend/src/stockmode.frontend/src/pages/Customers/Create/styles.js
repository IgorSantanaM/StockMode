import styled from 'styled-components';

export const PageContainer = styled.div`
  background-color: ${props => props.theme.colors.background};
  min-height: 100vh;
`;

export const Container = styled.div`
  max-width: 900px;
  margin: 0 auto;
  padding: 2rem;
  background-color: ${props => props.theme.colors.backgroundSecondary};
  border-radius: 1rem;
  box-shadow: 0 4px 6px -1px ${props => props.theme.colors.shadow}, 
              0 2px 4px -2px ${props => props.theme.colors.shadow};
`;

export const TitleContainer = styled.div`
  display: flex;
  align-items: center;
  gap: 0.75rem;
  margin-bottom: 1.5rem;
  padding-bottom: 1.5rem;
  border-bottom: 1px solid ${props => props.theme.colors.border};
`;

export const Title = styled.h2`
  font-size: 1.75rem;
  font-weight: bold;
  color: ${props => props.theme.colors.text};
  margin: 0;
`;

export const Form = styled.form`
  display: flex;
  flex-direction: column;
  gap: 2rem;
`;

export const SectionTitle = styled.h3`
  font-size: 1.25rem;
  font-weight: 600;
  color: ${props => props.theme.colors.text};
  margin: 0;
  padding-bottom: 1rem;
  border-bottom: 1px solid ${props => props.theme.colors.border};
`;

export const FormRow = styled.div`
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
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
`;

export const Input = styled.input`
  padding: 0.75rem 1rem;
  border: 1px solid ${props => props.theme.colors.border};
  border-radius: 0.5rem;
  font-size: 1rem;
  background-color: ${props => props.theme.colors.background};
  color: ${props => props.theme.colors.text};
  transition: all 0.2s ease-in-out;
  
  &:focus {
    outline: none;
    border-color: ${props => props.theme.colors.primary};
    box-shadow: 0 0 0 3px ${props => props.theme.colors.primaryLight};
  }

  &.error {
    border-color: ${props => props.theme.colors.danger};
    &:focus {
      box-shadow: 0 0 0 3px ${props => props.theme.colors.dangerLight};
    }
  }
`;

export const ErrorMessage = styled.span`
  color: ${props => props.theme.colors.danger};
  font-size: 0.875rem;
  margin-top: 0.5rem;
`;

export const ButtonContainer = styled.div`
  display: flex;
  justify-content: flex-end;
  gap: 1rem;
  margin-top: 2rem;
  padding-top: 1.5rem;
  border-top: 1px solid ${props => props.theme.colors.border};
`;

export const Button = styled.button`
  display: flex;
  align-items: center;
  justify-content: center;
  padding: 0.75rem 1.5rem;
  background-color: ${props => props.theme.colors.primary};
  color: white;
  border: none;
  border-radius: 0.5rem;
  font-size: 1rem;
  font-weight: 600;
  cursor: pointer;
  transition: all 0.2s ease;

  &:hover {
    background-color: ${props => props.theme.colors.primaryHover};
  }

  &:disabled {
    opacity: 0.5;
    cursor: not-allowed;
  }
`;

export const CancelButton = styled(Button)`
  background-color: ${props => props.theme.colors.backgroundTertiary};
  color: ${props => props.theme.colors.text};

  &:hover {
    background-color: ${props => props.theme.colors.border};
  }
`;

export const Alert = styled.div`
  padding: 1rem;
  margin-bottom: 1.5rem;
  border-radius: 0.5rem;
  font-weight: 500;
  display: flex;
  align-items: center;
  color: ${props => props.type === 'success' 
    ? props.theme.name === 'dark' ? '#86efac' : '#065f46'
    : props.theme.name === 'dark' ? '#fca5a5' : '#991b1b'};
  background-color: ${props => props.type === 'success' 
    ? props.theme.colors.successLight 
    : props.theme.colors.dangerLight};
`;

export const TagsContainer = styled.div`
  display: flex;
  flex-wrap: wrap;
  gap: 0.5rem;
  padding: 0.75rem;
  background-color: ${props => props.theme.colors.background};
  border: 1px solid ${props => props.theme.colors.border};
  border-radius: 0.5rem;
  min-height: 50px;
`;

export const SelectedTagsContainer = styled.div`
  display: flex;
  flex-wrap: wrap;
  gap: 0.5rem;
  margin-bottom: 1rem;
  padding: 0.75rem;
  background-color: ${props => props.theme.colors.background};
  border-radius: 0.5rem;
  border: 2px dashed ${props => props.theme.colors.border};
`;

export const TagBadge = styled.span`
  display: inline-flex;
  align-items: center;
  gap: 0.375rem;
  padding: 0.375rem 0.75rem;
  background-color: ${props => props.color || props.theme.colors.primary};
  color: white;
  border-radius: 9999px;
  font-size: 0.875rem;
  font-weight: 500;
  white-space: nowrap;
  transition: all 0.2s ease;

  &:hover {
    opacity: 0.9;
    transform: translateY(-1px);
  }
`;

export const RemoveTagButton = styled.button`
  display: inline-flex;
  align-items: center;
  justify-content: center;
  background: rgba(255, 255, 255, 0.2);
  border: none;
  border-radius: 50%;
  width: 18px;
  height: 18px;
  cursor: pointer;
  padding: 0;
  color: white;
  transition: background-color 0.2s ease;

  &:hover {
    background: rgba(255, 255, 255, 0.3);
  }
`;

export const TagInput = styled(Input)`
  font-size: 0.875rem;
`;

export const CreateTagButton = styled.button`
  display: inline-flex;
  align-items: center;
  gap: 0.375rem;
  padding: 0.375rem 0.75rem;
  background-color: ${props => props.theme.colors.backgroundTertiary};
  color: ${props => props.theme.colors.text};
  border: 1px dashed ${props => props.theme.colors.border};
  border-radius: 9999px;
  font-size: 0.875rem;
  font-weight: 500;
  cursor: pointer;
  transition: all 0.2s ease;

  &:hover {
    background-color: ${props => props.theme.colors.border};
    border-color: ${props => props.theme.colors.primary};
  }
`;
