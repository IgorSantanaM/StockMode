import styled from 'styled-components';

export const PageContainer = styled.div`
  padding: 2rem;
  background-color: #f3f5f7;
  width: 90rem;
  height: 40rem;
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
  color: #1f2937;
  margin: 0;
`;

export const Subtitle = styled.p`
  font-size: 1.125rem;
  color: #6b7280;
  margin-top: 0.5rem;
`;

export const SearchContainer = styled.div`
  position: relative;
  max-width: 600px;
  margin: 0 auto 3rem auto;
`;

export const Input = styled.input`
  width: 100%;
  padding: 1rem 1rem 1rem 3.5rem;
  border: 1px solid #d1d5db;
  border-radius: 9999px; /* Pill shape */
  font-size: 1rem;
  background-color: #ffffff;
  box-shadow: 0 4px 6px -1px rgb(0 0 0 / 0.1), 0 2px 4px -2px rgb(0 0 0 / 0.1);
  transition: all 0.2s ease-in-out;

  &:focus {
    outline: none;
    border-color: #4f46e5;
    box-shadow: 0 0 0 3px rgba(79, 70, 229, 0.1), 0 4px 6px -1px rgb(0 0 0 / 0.1);
  }
`;

export const FaqSection = styled.section`
  margin-bottom: 3rem;
`;

export const SectionTitle = styled.h3`
  font-size: 1.5rem;
  font-weight: 600;
  color: #374151;
  margin-bottom: 1.5rem;
`;

export const AccordionItem = styled.div`
  background-color: #ffffff;
  border: 1px solid #e5e7eb;
  border-radius: 0.75rem;
  margin-bottom: 1rem;
  overflow: hidden;
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
  color: #1f2937;
  text-align: left;
  cursor: pointer;

  svg {
    transition: transform 0.2s ease-in-out;
    transform: ${({ isOpen }) => (isOpen ? 'rotate(180deg)' : 'rotate(0deg)')};
  }
`;

export const AccordionContent = styled.div`
  padding: 0 1.5rem 1.25rem 1.5rem;
  color: #6b7280;
  line-height: 1.6;
`;

export const ContactSection = styled.section`
  text-align: center;
`;

export const ContactCard = styled.div`
  background-color: #ffffff;
  padding: 2rem;
  border-radius: 1rem;
  box-shadow: 0 4px 6px -1px rgb(0 0 0 / 0.1), 0 2px 4px -2px rgb(0 0 0 / 0.1);
  max-width: 600px;
  margin: 0 auto;
`;

export const PrimaryButton = styled.button`
  display: inline-flex;
  align-items: center;
  justify-content: center;
  gap: 0.5rem;
  padding: 0.75rem 1.5rem;
  background-color: #4f46e5;
  color: white;
  border: none;
  border-radius: 0.5rem;
  font-size: 1rem;
  font-weight: 600;
  cursor: pointer;
  transition: all 0.2s ease;
  margin-top: 1rem;

  &:hover {
    background-color: #4338ca;
  }
`;

// --- NOVOS ESTILOS PARA O MODAL ---

export const ModalBackdrop = styled.div`
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.5);
  display: flex;
  align-items: center;
  justify-content: center;
  z-index: 1000;
`;

export const ModalContent = styled.div`
  background-color: white;
  padding: 2rem;
  border-radius: 1rem;
  box-shadow: 0 10px 15px -3px rgb(0 0 0 / 0.1), 0 4px 6px -4px rgb(0 0 0 / 0.1);
  width: 90%;
  max-width: 500px;
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
  color: #1f2937;
  margin: 0;
`;

export const CloseButton = styled.button`
  background: none;
  border: none;
  color: #9ca3af;
  cursor: pointer;
  padding: 0.25rem;
  border-radius: 999px;
  
  &:hover {
    background-color: #f3f4f6;
    color: #1f2937;
  }
`;
