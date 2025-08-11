import React, { useState } from 'react';
import { User, Lock, Bell } from 'lucide-react';
import {
  PageContainer,
  Container,
  ProfileHeader,
  Avatar,
  UserInfo,
  Card,
  SectionTitle,
  Form,
  FormRow,
  FormGroup,
  Label,
  Input,
  ButtonContainer,
  PrimaryButton,
  PreferenceRow,
} from './styles';

// --- DADOS MOCK (Numa aplicação real, viriam do contexto de autenticação ou da API) ---
const userData = {
  name: 'Igor Medeiros',
  email: 'igor.medeiros@stockmode.com',
  avatar: 'https://i.pravatar.cc/80',
};

const Profile = () => {
  const [name, setName] = useState(userData.name);
  const [email, setEmail] = useState(userData.email);

  const handleInfoSubmit = (e) => {
    e.preventDefault();
    // Lógica para enviar a atualização de nome/email para a API
    console.log('Atualizando informações:', { name, email });
    alert('Informações atualizadas com sucesso!');
  };

  const handlePasswordSubmit = (e) => {
    e.preventDefault();
    // Lógica para enviar a atualização de senha para a API
    console.log('Atualizando senha...');
    alert('Senha atualizada com sucesso!');
  };

  return (
    <PageContainer>
      <Container>
        <ProfileHeader>
          <Avatar src={userData.avatar} alt="Avatar do Utilizador" />
          <UserInfo>
            <h2>{userData.name}</h2>
            <p>{userData.email}</p>
          </UserInfo>
        </ProfileHeader>

        <Card>
          <SectionTitle>
            <User size={20} style={{ marginRight: '0.5rem', verticalAlign: 'bottom' }} />
            Informações Pessoais
          </SectionTitle>
          <Form onSubmit={handleInfoSubmit}>
            <FormRow>
              <FormGroup>
                <Label htmlFor="name">Nome Completo</Label>
                <Input id="name" type="text" value={name} onChange={(e) => setName(e.target.value)} />
              </FormGroup>
              <FormGroup>
                <Label htmlFor="email">Endereço de Email</Label>
                <Input id="email" type="email" value={email} onChange={(e) => setEmail(e.target.value)} />
              </FormGroup>
            </FormRow>
            <ButtonContainer>
              <PrimaryButton type="submit">Guardar Alterações</PrimaryButton>
            </ButtonContainer>
          </Form>
        </Card>

        <Card>
          <SectionTitle>
            <Lock size={20} style={{ marginRight: '0.5rem', verticalAlign: 'bottom' }} />
            Segurança
          </SectionTitle>
          <Form onSubmit={handlePasswordSubmit}>
            <FormRow>
              <FormGroup>
                <Label htmlFor="currentPassword">Palavra-passe Atual</Label>
                <Input id="currentPassword" type="password" />
              </FormGroup>
            </FormRow>
            <FormRow>
              <FormGroup>
                <Label htmlFor="newPassword">Nova Palavra-passe</Label>
                <Input id="newPassword" type="password" />
              </FormGroup>
              <FormGroup>
                <Label htmlFor="confirmPassword">Confirmar Nova Palavra-passe</Label>
                <Input id="confirmPassword" type="password" />
              </FormGroup>
            </FormRow>
            <ButtonContainer>
              <PrimaryButton type="submit">Alterar Palavra-passe</PrimaryButton>
            </ButtonContainer>
          </Form>
        </Card>
        
        <Card>
          <SectionTitle>
            <Bell size={20} style={{ marginRight: '0.5rem', verticalAlign: 'bottom' }} />
            Preferências de Notificação
          </SectionTitle>
          <div>
            <PreferenceRow>
                <div>
                    <p style={{fontWeight: 500, color: '#1f2937'}}>Resumo Diário de Vendas</p>
                    <p style={{fontSize: '0.875rem', color: '#6b7280'}}>Receber um email diário com o resumo das suas vendas.</p>
                </div>
                {/* Em um app real, este seria um componente de switch customizado */}
                <input type="checkbox" style={{transform: 'scale(1.5)'}} defaultChecked />
            </PreferenceRow>
            <PreferenceRow>
                <div>
                    <p style={{fontWeight: 500, color: '#1f2937'}}>Alertas de Baixo Estoque</p>
                    <p style={{fontSize: '0.875rem', color: '#6b7280'}}>Ser notificado quando um item atingir o nível mínimo de estoque.</p>
                </div>
                <input type="checkbox" style={{transform: 'scale(1.5)'}} defaultChecked />
            </PreferenceRow>
          </div>
        </Card>
      </Container>
    </PageContainer>
  );
};

export default Profile;
