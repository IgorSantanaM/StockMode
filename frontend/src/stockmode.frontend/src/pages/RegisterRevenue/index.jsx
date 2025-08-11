import React, { useState } from 'react';
import { PlusCircle, X, CheckCircle, AlertCircle } from 'lucide-react';
import {
  PageContainer, Container, TitleContainer, Title, Form, FormGroup, Label, Input,
  ErrorMessage, ButtonContainer, Button, CancelButton, FormRow, Select, Alert
} from './styles';

const RegisterRevenue = () => {
  const [formData, setFormData] = useState({
    description: '',
    amount: '',
    date: new Date().toISOString().split('T')[0], // Data de hoje por defeito
    category: 'Recebimento de Crediário',
    customerId: '',
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
    if (!formData.description.trim()) newErrors.description = 'A descrição da receita é obrigatória.';
    if (!formData.amount.trim()) {
      newErrors.amount = 'O valor é obrigatório.';
    } else if (isNaN(Number(formData.amount)) || Number(formData.amount) <= 0) {
      newErrors.amount = 'O valor deve ser um número positivo.';
    }
    if (!formData.date) newErrors.date = 'A data é obrigatória.';
    
    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (!validate()) return;

    setIsSubmitting(true);
    setApiStatus({ error: null, success: false });

    const payload = {
        ...formData,
        amount: parseFloat(formData.amount)
    };

    console.log("Enviando para a API:", payload);

    // Simulação de chamada à API
    try {
      await new Promise(resolve => setTimeout(resolve, 1500));
      
      setApiStatus({ error: null, success: true });
      // Limpar formulário
      setFormData({
        description: '',
        amount: '',
        date: new Date().toISOString().split('T')[0],
        category: 'Recebimento de Crediário',
        customerId: '',
      });
      setErrors({});

    } catch (error) {
      console.error("Erro ao registar receita:", error);
      setApiStatus({ error: error.message || 'Falha ao registar receita.', success: false });
    } finally {
      setIsSubmitting(false);
    }
  };

  return (
    <PageContainer>
      <Container>
        <TitleContainer>
          <PlusCircle size={32} color="#10b981" />
          <Title>Registar Nova Receita</Title>
        </TitleContainer>

        {apiStatus.success && <Alert type="success"><CheckCircle size={20} style={{ marginRight: '0.5rem' }}/>Receita registada com sucesso!</Alert>}
        {apiStatus.error && <Alert type="error"><AlertCircle size={20} style={{ marginRight: '0.5rem' }}/>{apiStatus.error}</Alert>}

        <Form onSubmit={handleSubmit} noValidate>
          <FormRow>
            <FormGroup style={{ gridColumn: '1 / -1' }}>
              <Label htmlFor="description">Descrição / Origem da Receita</Label>
              <Input type="text" id="description" placeholder="Ex: Pagamento da parcela 2/3 do cliente João Silva" value={formData.description} onChange={handleChange} className={errors.description ? 'error' : ''} />
              {errors.description && <ErrorMessage>{errors.description}</ErrorMessage>}
            </FormGroup>
          </FormRow>

          <FormRow>
            <FormGroup>
              <Label htmlFor="amount">Valor (R$)</Label>
              <Input type="number" step="0.01" id="amount" value={formData.amount} onChange={handleChange} className={errors.amount ? 'error' : ''} />
              {errors.amount && <ErrorMessage>{errors.amount}</ErrorMessage>}
            </FormGroup>
            <FormGroup>
              <Label htmlFor="date">Data da Receita</Label>
              <Input type="date" id="date" value={formData.date} onChange={handleChange} className={errors.date ? 'error' : ''} />
              {errors.date && <ErrorMessage>{errors.date}</ErrorMessage>}
            </FormGroup>
          </FormRow>
          
          <FormRow>
            <FormGroup>
              <Label htmlFor="category">Categoria</Label>
              <Select id="category" value={formData.category} onChange={handleChange}>
                <option>Recebimento de Crediário</option>
                <option>Aporte de Sócio</option>
                <option>Empréstimo</option>
                <option>Outras Receitas</option>
              </Select>
            </FormGroup>
            <FormGroup>
              <Label htmlFor="customerId">Vincular a um Cliente (Opcional)</Label>
              <Input type="text" id="customerId" placeholder="Buscar cliente por nome ou CPF..." value={formData.customerId} onChange={handleChange} />
            </FormGroup>
          </FormRow>

          <ButtonContainer>
            <CancelButton type="button">
              <X size={20} style={{ marginRight: '0.5rem' }} />
              Cancelar
            </CancelButton>
            <Button type="submit" disabled={isSubmitting}>
              {isSubmitting ? 'Registando...' : 'Registar Receita'}
            </Button>
          </ButtonContainer>
        </Form>
      </Container>
    </PageContainer>
  );
};

export default RegisterRevenue;
