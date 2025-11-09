import styled from 'styled-components';

export const PageContainer = styled.div`
  padding: 2rem;
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
  color: ${props => props.theme.colors.text};
  background-color: ${props => props.theme.colors.backgroundTertiary};
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

export const Textarea = styled.textarea`
  padding: 0.75rem 1rem;
  border: 1px solid ${props => props.theme.colors.border};
  border-radius: 0.5rem;
  font-size: 1rem;
  color: ${props => props.theme.colors.text};
  background-color: ${props => props.theme.colors.backgroundTertiary};
  transition: all 0.2s ease-in-out;
  min-height: 120px;
  resize: vertical;

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
  border: 1px solid ${props => props.theme.colors.border};
  border-radius: 0.5rem;
  min-height: 100px;
  background-color: ${props => props.theme.colors.backgroundTertiary};
`;

export const TagBadge = styled.span`
  display: inline-flex;
  align-items: center;
  gap: 0.5rem;
  padding: 0.5rem 0.75rem;
  background-color: ${props => props.color || props.theme.colors.primary};
  color: white;
  border-radius: 0.375rem;
  font-size: 0.875rem;
  font-weight: 500;
  cursor: ${props => props.onClick ? 'pointer' : 'default'};
  transition: all 0.2s ease;

  &:hover {
    opacity: ${props => props.onClick ? '0.8' : '1'};
  }

  svg {
    cursor: pointer;
  }
`;

export const TagInput = styled(Input)`
  flex: 1;
  min-width: 200px;
`;

export const ColorInput = styled.input`
  width: 60px;
  height: 40px;
  border: 1px solid ${props => props.theme.colors.border};
  border-radius: 0.5rem;
  cursor: pointer;
  background-color: ${props => props.theme.colors.backgroundTertiary};
  
  &::-webkit-color-swatch-wrapper {
    padding: 2px;
  }
  
  &::-webkit-color-swatch {
    border: none;
    border-radius: 0.25rem;
  }
`;

export const AddTagButton = styled.button`
  padding: 0.5rem 1rem;
  background-color: ${props => props.theme.colors.primary};
  color: white;
  border: none;
  border-radius: 0.5rem;
  font-size: 0.875rem;
  font-weight: 500;
  cursor: pointer;
  transition: all 0.2s ease;
  white-space: nowrap;

  &:hover {
    background-color: ${props => props.theme.colors.primaryHover};
  }

  &:disabled {
    opacity: 0.5;
    cursor: not-allowed;
  }
`;

export const TagCreationRow = styled.div`
  display: flex;
  gap: 0.75rem;
  align-items: flex-end;
  margin-bottom: 1rem;
`;

export const EmptyTagMessage = styled.p`
  color: ${props => props.theme.colors.textTertiary};
  font-size: 0.875rem;
  text-align: center;
  margin: auto;
`;

export const SelectedTagsContainer = styled.div`
  display: flex;
  flex-wrap: wrap;
  gap: 0.5rem;
  padding: 0.75rem;
  border: 1px solid ${props => props.theme.colors.border};
  border-radius: 0.5rem;
  min-height: 80px;
  background-color: ${props => props.theme.colors.backgroundTertiary};
`;
