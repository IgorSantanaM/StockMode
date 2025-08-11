import React, { useState } from 'react';
import { Search, ChevronDown, HelpCircle, MessageSquare, Mail, X } from 'lucide-react';
import {
  PageContainer, Container, Header, Title, Subtitle, SearchContainer, Input,
  FaqSection, SectionTitle, AccordionItem, AccordionButton, AccordionContent,
  ContactSection, ContactCard, PrimaryButton, ModalBackdrop, ModalContent,
  ModalHeader, ModalTitle, CloseButton
} from './styles';

// --- DADOS MOCK ---
const faqs = [
  { question: 'Como adiciono um novo produto com diferentes tamanhos e cores?', answer: 'Na página de "Adicionar Produto", preencha as informações principais. Na secção "Variações", clique em "Adicionar outra variação" para cada combinação de tamanho e cor, preenchendo o SKU, preço e estoque para cada uma.' },
  { question: 'Como registo uma venda "fiado" (crediário)?', answer: 'Na página de "Nova Venda", adicione os itens e selecione o cliente. No resumo do pagamento, escolha "Crediário" como método de pagamento. O sistema irá registar a dívida no perfil do cliente.' },
  { question: 'Onde vejo se tive lucro no mês?', answer: 'Acesse a página "Financeiro". O painel principal mostra o Faturamento e o Lucro Bruto para o mês atual. Para um relatório detalhado, você pode usar os filtros de período.' },
];

// Componente reutilizável para o Accordion
const FaqItem = ({ faq }) => {
  const [isOpen, setIsOpen] = useState(false);
  return (
    <AccordionItem>
      <AccordionButton onClick={() => setIsOpen(!isOpen)} isOpen={isOpen}>
        {faq.question}
        <ChevronDown size={20} />
      </AccordionButton>
      {isOpen && <AccordionContent>{faq.answer}</AccordionContent>}
    </AccordionItem>
  );
};

// Componente para o Modal de Email
const EmailModal = ({ onClose }) => {
    const handleFormSubmit = (e) => {
        e.preventDefault();
        console.log("Formulário de email enviado!");
        alert("Sua mensagem foi enviada com sucesso!");
        onClose(); 
    };

    return (
        <ModalBackdrop onClick={onClose}>
            <ModalContent onClick={e => e.stopPropagation()}>
                <ModalHeader>
                    <ModalTitle>Contactar Suporte</ModalTitle>
                    <CloseButton onClick={onClose}><X size={24} /></CloseButton>
                </ModalHeader>
                <form onSubmit={handleFormSubmit}>
                    <div style={{display: 'flex', flexDirection: 'column', gap: '1rem'}}>
                        <input type="email" placeholder="Seu email para contacto" required style={{padding: '0.75rem', borderRadius: '0.5rem', border: '1px solid #d1d5db'}} />
                        <input type="text" placeholder="Assunto" required style={{padding: '0.75rem', borderRadius: '0.5rem', border: '1px solid #d1d5db'}} />
                        <textarea placeholder="Descreva seu problema ou dúvida aqui..." rows="5" required style={{padding: '0.75rem', borderRadius: '0.5rem', border: '1px solid #d1d5db', resize: 'vertical'}}></textarea>
                    </div>
                    <div style={{marginTop: '1.5rem', textAlign: 'right'}}>
                        <PrimaryButton type="submit">
                            <Mail size={20} style={{marginRight: '0.5rem'}}/>
                            Enviar Mensagem
                        </PrimaryButton>
                    </div>
                </form>
            </ModalContent>
        </ModalBackdrop>
    );
};

const Help = () => {
  const [searchTerm, setSearchTerm] = useState('');
  const [isModalOpen, setIsModalOpen] = useState(false);

  return (
    <PageContainer>
      {isModalOpen && <EmailModal onClose={() => setIsModalOpen(false)} />}

      <Container>
        <Header>
          <HelpCircle size={48} color="#4f46e5" />
          <Title>Central de Ajuda</Title>
          <Subtitle>Como podemos ajudar? Encontre respostas rápidas aqui.</Subtitle>
        </Header>

        <SearchContainer>
          <Search style={{ position: 'absolute', left: '1.25rem', top: '50%', transform: 'translateY(-50%)', color: '#9ca3af' }} />
          <Input 
            type="text" 
            placeholder="Procure por tópicos (ex: 'adicionar item', 'relatório')..." 
            value={searchTerm}
            onChange={(e) => setSearchTerm(e.target.value)}
          />
        </SearchContainer>

        <FaqSection>
          <SectionTitle>Perguntas Frequentes</SectionTitle>
          {faqs.map((faq, index) => (
            <FaqItem key={index} faq={faq} />
          ))}
        </FaqSection>

        <ContactSection>
          <ContactCard>
            <SectionTitle>Não encontrou o que procurava?</SectionTitle>
            <p style={{ color: '#6b7280', marginBottom: '1.5rem' }}>A nossa equipa de suporte está pronta para ajudar.</p>
            <div style={{display: 'flex', justifyContent: 'center', gap: '1rem'}}>
                <PrimaryButton onClick={() => setIsModalOpen(true)}>
                    <Mail size={20} />
                    Enviar um Email
                </PrimaryButton>
            </div>
          </ContactCard>
        </ContactSection>
      </Container>
    </PageContainer>
  );
};

export default Help;
