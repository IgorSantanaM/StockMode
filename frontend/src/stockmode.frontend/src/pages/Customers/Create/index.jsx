import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { UserPlus, X, CheckCircle, AlertCircle } from 'lucide-react';
import api from '../../../services/api'; 
import {
  PageContainer, Container, TitleContainer, Title, Form, FormGroup, Label, Input,
  ErrorMessage, ButtonContainer, Button, CancelButton, SectionTitle, FormRow, Alert
} from './styles';

const CustomerCreation = () => {
  const navigate = useNavigate();
  
  const [formData, setFormData] = useState({
    name: '',
    email: '',
    phoneNumber: '',
    addressDto: {
      number: '',
      street: '',
      city: '',
      state: '',
      zipCode: ''
    }
  });

  const [errors, setErrors] = useState({});
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [apiStatus, setApiStatus] = useState({ error: null, success: false });

  const formatPhoneNumber = (value) => {
    if (!value) return value;
    const phoneNumber = value.replace(/[^\d]/g, '');
    const phoneNumberLength = phoneNumber.length;
    if (phoneNumberLength < 3) return `(${phoneNumber}`;
    if (phoneNumberLength < 8) return `(${phoneNumber.slice(0, 2)}) ${phoneNumber.slice(2)}`;
    return `(${phoneNumber.slice(0, 2)}) ${phoneNumber.slice(2, 7)}-${phoneNumber.slice(7, 11)}`;
  };

  const formatZipCode = (value) => {
    if (!value) return value;
    const zipCode = value.replace(/[^\d]/g, '');
    if (zipCode.length < 6) return zipCode;
    return `${zipCode.slice(0, 5)}-${zipCode.slice(5, 8)}`;
  };

  const handleChange = (e) => {
    const { id, value } = e.target;
    let formattedValue = value;
    if (id === 'phoneNumber') {
      formattedValue = formatPhoneNumber(value);
    }
    setFormData(prev => ({ ...prev, [id]: formattedValue }));
  };
  
  const handleAddressChange = (e) => {
    const { id, value } = e.target;
    let formattedValue = value;
    if (id === 'zipCode') {
      formattedValue = formatZipCode(value);
    }
    setFormData(prev => ({
      ...prev,
      addressDto: { ...prev.addressDto, [id]: formattedValue }
    }));
  };

  const validate = () => {
    const newErrors = {};
    if (!formData.name.trim()) newErrors.name = 'O Nome é obrigatório.';
    if (!formData.email.trim()) {
      newErrors.email = 'O Email é obrigatório.';
    } else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(formData.email)) {
      newErrors.email = 'O formato do email é inválido.';
    }
    if (!formData.phoneNumber.trim()) newErrors.phoneNumber = 'O Telefone é obrigatório.';
    
    // Valida o endereço apenas se algum campo for preenchido
    const addressFields = Object.values(formData.addressDto).some(field => (typeof field === 'string' && field.trim() !== '') || (typeof field === 'number' && field > 0));
    if (addressFields) {
      if (!formData.addressDto.street.trim()) newErrors.street = 'A rua é obrigatória.';
      if (!formData.addressDto.city.trim()) newErrors.city = 'A cidade é obrigatória.';
      if (!formData.addressDto.state.trim()) newErrors.state = 'O estado é obrigatório.';
      if (!formData.addressDto.zipCode.trim()) newErrors.zipCode = 'O CEP é obrigatório.';
      if (!formData.addressDto.number || formData.addressDto.number <= 0) newErrors.number = 'O número é obrigatório e deve ser positivo.';
    }

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
      addressDto: {
        ...formData.addressDto,
        number: parseInt(formData.addressDto.number, 10),
      }
    };
    
    // Se o endereço não foi preenchido, envia null
    if (!Object.values(payload.addressDto).some(v => v)) {
      payload.addressDto = null;
    }

    console.log("A enviar para a API:", payload);

    try {
      const response = await api.post("customers", payload);
      
      setApiStatus({ error: null, success: true });
      console.log("Data sent successfully! ", response.data);

      setTimeout(() => navigate("/customers"), 2000);

    } catch (error) {
      console.error("Erro ao cadastrar cliente:", error);
      const errorMessage = error.response?.data?.message || "Ocorreu um erro ao cadastrar o cliente.";
      setApiStatus({ error: errorMessage, success: false });
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

        {apiStatus.success && <Alert type="success"><CheckCircle size={20} style={{ marginRight: '0.5rem' }}/>Cliente cadastrado com sucesso! A redirecionar...</Alert>}
        {apiStatus.error && <Alert type="error"><AlertCircle size={20} style={{ marginRight: '0.5rem' }}/>{apiStatus.error}</Alert>}

        <Form onSubmit={handleSubmit} noValidate>
          <div>
            <SectionTitle>Informações Pessoais</SectionTitle>
            <FormGroup>
              <Label htmlFor="name">Nome Completo</Label>
              <Input type="text" id="name" value={formData.name} onChange={handleChange} className={errors.name ? 'error' : ''} />
              {errors.name && <ErrorMessage>{errors.name}</ErrorMessage>}
            </FormGroup>
          </div>

          <div>
            <SectionTitle>Informações de Contato</SectionTitle>
            <FormRow>
              <FormGroup>
                <Label htmlFor="email">Email</Label>
                <Input type="email" id="email" value={formData.email} onChange={handleChange} className={errors.email ? 'error' : ''} />
                {errors.email && <ErrorMessage>{errors.email}</ErrorMessage>}
              </FormGroup>
              <FormGroup>
                <Label htmlFor="phoneNumber">Telefone</Label>
                <Input type="tel" id="phoneNumber" value={formData.phoneNumber} onChange={handleChange} maxLength="15" className={errors.phoneNumber ? 'error' : ''} />
                {errors.phoneNumber && <ErrorMessage>{errors.phoneNumber}</ErrorMessage>}
              </FormGroup>
            </FormRow>
          </div>

          <div>
            <SectionTitle>Endereço (Opcional)</SectionTitle>
            <FormRow>
              <FormGroup style={{ gridColumn: 'span 2 / span 2' }}>
                <Label htmlFor="street">Rua</Label>
                <Input type="text" id="street" value={formData.addressDto.street} onChange={handleAddressChange} className={errors.street ? 'error' : ''} />
                {errors.street && <ErrorMessage>{errors.street}</ErrorMessage>}
              </FormGroup>
            </FormRow>
            <FormRow>
              <FormGroup>
                <Label htmlFor="number">Número</Label>
                <Input type="number" id="number" value={formData.addressDto.number} onChange={handleAddressChange} className={errors.number ? 'error' : ''} />
                {errors.number && <ErrorMessage>{errors.number}</ErrorMessage>}
              </FormGroup>
              <FormGroup>
                <Label htmlFor="city">Cidade</Label>
                <Input type="text" id="city" value={formData.addressDto.city} onChange={handleAddressChange} className={errors.city ? 'error' : ''} />
                {errors.city && <ErrorMessage>{errors.city}</ErrorMessage>}
              </FormGroup>
            </FormRow>
            <FormRow>
              <FormGroup>
                <Label htmlFor="state">Estado</Label>
                <Input type="text" id="state" value={formData.addressDto.state} onChange={handleAddressChange} className={errors.state ? 'error' : ''} />
                {errors.state && <ErrorMessage>{errors.state}</ErrorMessage>}
              </FormGroup>
              <FormGroup>
                <Label htmlFor="zipCode">CEP</Label>
                <Input type="text" id="zipCode" value={formData.addressDto.zipCode} onChange={handleAddressChange} maxLength="9" className={errors.zipCode ? 'error' : ''} />
                {errors.zipCode && <ErrorMessage>{errors.zipCode}</ErrorMessage>}
              </FormGroup>
            </FormRow>
          </div>

          <ButtonContainer>
            <CancelButton type="button" onClick={() => navigate('/customers')}>
              <X size={20} style={{ marginRight: '0.5rem' }} />
              Cancelar
            </CancelButton>
            <Button type="submit" disabled={isSubmitting}>
              {isSubmitting ? 'A Guardar...' : 'Guardar Cliente'}
            </Button>
          </ButtonContainer>
        </Form>
      </Container>
    </PageContainer>
  );
};

export default CustomerCreation;

