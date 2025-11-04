import styled from 'styled-components';

export const PageContainer = styled.div`
  padding: 2rem;
  background-color: ${props => props.theme.colors.background};
  transition: background-color 0.3s ease;
`;

export const Container = styled.div`
  max-width: 900px;
  margin: 0 auto;
`;

export const Header = styled.div`
  text-align: center;
  margin-bottom: 3rem;
`;

export const Title = styled.h2`
  font-size: 2rem;
  font-weight: bold;
  color: ${props => props.theme.colors.text};
  margin: 0;
  transition: color 0.3s ease;
`;

export const Subtitle = styled.p`
  font-size: 1.125rem;
  color: ${props => props.theme.colors.textSecondary};
  margin-top: 0.5rem;
  transition: color 0.3s ease;
`;

export const SearchContainer = styled.div`
  position: relative;
  max-width: 600px;
  margin: 0 auto 3rem auto;
`;

export const Input = styled.input`
  width: 100%;
  padding: 1rem 1rem 1rem 3.5rem;
  border: 1px solid ${props => props.theme.colors.border};
  border-radius: 9999px;
  font-size: 1rem;
  background-color: ${props => props.theme.colors.backgroundSecondary};
  color: ${props => props.theme.colors.text};
  box-shadow: 0 4px 6px -1px ${props => props.theme.colors.shadow};
  transition: all 0.3s ease;

  &:focus {
    outline: none;
    border-color: ${props => props.theme.colors.primary};
    box-shadow: 0 0 0 3px ${props => props.theme.colors.primaryLight}, 0 4px 6px -1px ${props => props.theme.colors.shadow};
  }

  &::placeholder {
    color: ${props => props.theme.colors.textTertiary};
  }
`;

export const FaqSection = styled.section`
  margin-bottom: 3rem;
`;

export const SectionTitle = styled.h3`
  font-size: 1.5rem;
  font-weight: 600;
  color: ${props => props.theme.colors.text};
  margin-bottom: 1.5rem;
  transition: color 0.3s ease;
`;

export const AccordionItem = styled.div`
  background-color: ${props => props.theme.colors.backgroundSecondary};
  border: 1px solid ${props => props.theme.colors.border};
  border-radius: 0.75rem;
  margin-bottom: 1rem;
  overflow: hidden;
  transition: all 0.3s ease;
`;

export const AccordionButton = styled.button`
  width: 100%;
  padding: 1.25rem 1.5rem;
  display: flex;
  justify-content: space-between;
  align-items: center;
  background: none;
  border: none;
  font-size: 1rem;
  font-weight: 500;
  color: ${props => props.theme.colors.text};
  text-align: left;
  cursor: pointer;
  transition: color 0.3s ease;

  svg {
    transition: transform 0.2s ease-in-out;
    transform: ${({ isOpen }) => (isOpen ? 'rotate(180deg)' : 'rotate(0deg)')};
  }
`;

export const AccordionContent = styled.div`
  padding: 0 1.5rem 1.25rem 1.5rem;
  color: ${props => props.theme.colors.textSecondary};
  line-height: 1.6;
  transition: color 0.3s ease;
`;

export const ContactSection = styled.section`
  text-align: center;
`;

export const ContactCard = styled.div`
  background-color: ${props => props.theme.colors.backgroundSecondary};
  padding: 2rem;
  border-radius: 1rem;
  box-shadow: 0 4px 6px -1px ${props => props.theme.colors.shadow};
  max-width: 600px;
  margin: 0 auto;
  transition: all 0.3s ease;
`;

export const PrimaryButton = styled.button`
  display: inline-flex;
  align-items: center;
  justify-content: center;
  gap: 0.5rem;
  padding: 0.75rem 1.5rem;
  background-color: ${props => props.theme.colors.primary};
  color: white;
  border: none;
  border-radius: 0.5rem;
  font-size: 1rem;
  font-weight: 600;
  cursor: pointer;
  transition: all 0.3s ease;
  margin-top: 1rem;

  &:hover {
    background-color: ${props => props.theme.colors.primaryHover};
  }
`;

export const ModalBackdrop = styled.div`
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: ${props => props.theme.colors.overlay};
  display: flex;
  align-items: center;
  justify-content: center;
  z-index: 1000;
  transition: background-color 0.3s ease;
`;

export const ModalContent = styled.div`
  background-color: ${props => props.theme.colors.backgroundSecondary};
  padding: 2rem;
  border-radius: 1rem;
  box-shadow: 0 10px 15px -3px ${props => props.theme.colors.shadow};
  width: 90%;
  max-width: 500px;
  transition: all 0.3s ease;
`;

export const ModalHeader = styled.div`
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 1.5rem;
`;

export const ModalTitle = styled.h3`
  font-size: 1.25rem;
  font-weight: 600;
  color: ${props => props.theme.colors.text};
  margin: 0;
  transition: color 0.3s ease;
`;

export const CloseButton = styled.button`
  background: none;
  border: none;
  color: ${props => props.theme.colors.textTertiary};
  cursor: pointer;
  padding: 0.25rem;
  border-radius: 999px;
  transition: all 0.3s ease;
  
  &:hover {
    background-color: ${props => props.theme.colors.borderLight};
    color: ${props => props.theme.colors.text};
  }
`;
