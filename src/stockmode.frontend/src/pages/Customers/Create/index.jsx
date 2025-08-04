import React, { useState } from 'react';
import { UserPlus, X, CheckCircle, AlertCircle } from 'lucide-react';
import {
  PageContainer, Container, TitleContainer, Title, Form, FormGroup, Label, Input,
  ErrorMessage, ButtonContainer, Button, CancelButton, SectionTitle, FormRow, Alert
} from './styles';

const CustomerCreation = () => {
  const [formData, setFormData] = useState({
    name: '',
    email: '',
    phone: '',
    address: '',
  });
  const [errors, setErrors] = useState({});
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [apiStatus, setApiStatus] = useState({ error: null, success: false });

  const handleChange = (e) => {
    const { id, value } = e.target;
    setFormData(prev => ({ ...prev, [id]: value }));
  };

  const validate = () => {
    const newErrors = {};
    if (!formData.name.trim()) newErrors.name = 'O nome do cliente é obrigatório.';
    if (!formData.email.trim()) {
      newErrors.email = 'O email é obrigatório.';
    } else if (!/\S+@\S+\.\S+/.test(formData.email)) {
      newErrors.email = 'O formato do email é inválido.';
    }
    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (!validate()) return;

    setIsSubmitting(true);
    setApiStatus({ error: null, success: false });

    console.log("Enviando para a API:", formData);

    // Simulação de chamada à API
    try {
      // const response = await fetch('/api/customers', {
      //   method: 'POST',
      //   headers: { 'Content-Type': 'application/json' },
      //   body: JSON.stringify(formData)
      // });
      // if (!response.ok) {
      //   const errorData = await response.json();
      //   throw new Error(errorData.message || 'Falha ao criar cliente.');
      // }
      
      await new Promise(resolve => setTimeout(resolve, 1500));
      
      setApiStatus({ error: null, success: true });
      // Limpar formulário
      setFormData({ name: '', email: '', phone: '', address: '' });
      setErrors({});

    } catch (error) {
      console.error("Erro ao cadastrar cliente:", error);
      setApiStatus({ error: error.message, success: false });
    } finally {
      setIsSubmitting(false);
    }
  };

  return (
    <PageContainer>
      <Container>
        <TitleContainer>
          <UserPlus size={32} color="#4f46e5" />
          <Title>Adicionar Novo Cliente</Title>
        </TitleContainer>

        {apiStatus.success && <Alert type="success"><CheckCircle size={20} style={{ marginRight: '0.5rem' }}/>Cliente cadastrado com sucesso!</Alert>}
        {apiStatus.error && <Alert type="error"><AlertCircle size={20} style={{ marginRight: '0.5rem' }}/>{apiStatus.error}</Alert>}

        <Form onSubmit={handleSubmit} noValidate>
          <div>
            <SectionTitle>Informações Pessoais</SectionTitle>
            <FormRow style={{marginTop: '1.5rem'}}>
              <FormGroup>
                <Label htmlFor="name">Nome Completo</Label>
                <Input type="text" id="name" value={formData.name} onChange={handleChange} className={errors.name ? 'error' : ''} />
                {errors.name && <ErrorMessage>{errors.name}</ErrorMessage>}
              </FormGroup>
            </FormRow>
          </div>

          <div>
            <SectionTitle>Informações de Contato</SectionTitle>
            <FormRow style={{marginTop: '1.5rem'}}>
              <FormGroup>
                <Label htmlFor="email">Email</Label>
                <Input type="email" id="email" value={formData.email} onChange={handleChange} className={errors.email ? 'error' : ''} />
                {errors.email && <ErrorMessage>{errors.email}</ErrorMessage>}
              </FormGroup>
              <FormGroup>
                <Label htmlFor="phone">Telefone</Label>
                <Input type="tel" id="phone" value={formData.phone} onChange={handleChange} />
              </FormGroup>
            </FormRow>
          </div>

          <div>
            <SectionTitle>Endereço</SectionTitle>
            <FormRow style={{marginTop: '1.5rem'}}>
              <FormGroup>
                <Label htmlFor="address">Morada (Rua, Número, Bairro)</Label>
                <Input type="text" id="address" value={formData.address} onChange={handleChange} />
              </FormGroup>
            </FormRow>
          </div>

          <ButtonContainer>
            <CancelButton type="button">
              <X size={20} style={{ marginRight: '0.5rem' }} />
              Cancelar
            </CancelButton>
            <Button type="submit" disabled={isSubmitting}>
              {isSubmitting ? 'Salvando...' : 'Salvar Cliente'}
            </Button>
          </ButtonContainer>
        </Form>
      </Container>
    </PageContainer>
  );
};

export default CustomerCreation;
