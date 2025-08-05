import React, { useState } from 'react';
import { Settings, Building, Store, Users, Link } from 'lucide-react';
import {
  PageContainer,
  Container,
  TitleContainer,
  Title,
  SettingsLayout,
  NavMenu,
  NavButton,
  ContentCard,
  SectionTitle,
  Form,
  FormRow,
  FormGroup,
  Label,
  Input,
  ButtonContainer,
  PrimaryButton,
  Paragraph,
} from './styles';

// Componentes para cada aba de conteúdo
const GeneralSettings = () => (
  <div>
    <SectionTitle>Geral</SectionTitle>
    <Form>
      <FormGroup>
        <Label htmlFor="storeName">Nome da Loja</Label>
        <Input id="storeName" type="text" defaultValue="Minha Loja de Moda" />
      </FormGroup>
      <FormGroup>
        <Label htmlFor="storeContact">Email de Contato</Label>
        <Input id="storeContact" type="email" defaultValue="contato@minhaloja.com" />
      </FormGroup>
      <ButtonContainer>
        <PrimaryButton type="submit">Guardar Alterações</PrimaryButton>
      </ButtonContainer>
    </Form>
  </div>
);

const StoreSettings = () => (
  <div>
    <SectionTitle>Loja</SectionTitle>
    <Form>
       <FormRow>
            <FormGroup>
                <Label htmlFor="currency">Moeda</Label>
                <Input id="currency" type="text" defaultValue="Real Brasileiro (BRL)" disabled />
            </FormGroup>
             <FormGroup>
                <Label htmlFor="timezone">Fuso Horário</Label>
                <Input id="timezone" type="text" defaultValue="(GMT-04:00) Manaus" disabled />
            </FormGroup>
       </FormRow>
        <FormGroup>
            <Label htmlFor="receiptFooter">Texto do Rodapé do Recibo</Label>
            <Input id="receiptFooter" type="text" placeholder="Ex: Obrigado pela sua preferência!" />
        </FormGroup>
      <ButtonContainer>
        <PrimaryButton type="submit">Guardar Alterações</PrimaryButton>
      </ButtonContainer>
    </Form>
  </div>
);

const UserSettings = () => (
    <div>
        <SectionTitle>Utilizadores</SectionTitle>
        <Paragraph>Funcionalidade de gestão de utilizadores em breve.</Paragraph>
        <ButtonContainer>
            <PrimaryButton>Convidar Utilizador</PrimaryButton>
        </ButtonContainer>
    </div>
);

const IntegrationsSettings = () => (
    <div >
        <SectionTitle>Integrações</SectionTitle>
        <Paragraph> Funcionalidade de integrações com outras plataformas em breve.</Paragraph>
    </div>
);


const SettingsPage = () => {
  const [activeTab, setActiveTab] = useState('general');

  const renderContent = () => {
    switch (activeTab) {
      case 'general':
        return <GeneralSettings />;
      case 'store':
        return <StoreSettings />;
      case 'users':
        return <UserSettings />;
      // --- CORREÇÃO: Adicionada a lógica para a nova aba ---
      case 'integrations':
        return <IntegrationsSettings />;
      default:
        return <GeneralSettings />;
    }
  };

  return (
    <PageContainer>
      <Container>
        <TitleContainer>
          <Settings size={32} color="#4f46e5" />
          <Title>Configurações</Title>
        </TitleContainer>
        <SettingsLayout>
          <NavMenu>
            <NavButton active={activeTab === 'general'} onClick={() => setActiveTab('general')}>
              <Building size={20} />
              Geral
            </NavButton>
            <NavButton active={activeTab === 'store'} onClick={() => setActiveTab('store')}>
              <Store size={20} />
              Loja
            </NavButton>
            <NavButton active={activeTab === 'users'} onClick={() => setActiveTab('users')}>
              <Users size={20} />
              Utilizadores
            </NavButton>
            <NavButton active={activeTab === 'integrations'} onClick={() => setActiveTab('integrations')}>
              <Link size={20} />
              Integrações
            </NavButton>
          </NavMenu>
          <ContentCard>
            {renderContent()}
          </ContentCard>
        </SettingsLayout>
      </Container>
    </PageContainer>
  );
};

export default SettingsPage;
