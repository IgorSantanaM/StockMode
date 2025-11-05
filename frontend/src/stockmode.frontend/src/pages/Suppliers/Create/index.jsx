import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { toast } from 'react-toastify';
import { Truck, X } from 'lucide-react';
import { useTheme } from '../../../contexts/ThemeContext';
import api from '../../../services/api';
import {
  PageContainer, Container, TitleContainer, Title, Form, FormGroup, Label, Input,
  ErrorMessage, ButtonContainer, Button, CancelButton, SectionTitle, FormRow, Textarea,
} from './styles';

// --- Funções de formatação (sem alterações) ---
const formatCnpj = (value) => {
  if (!value) return value;
  const cnpj = value.replace(/[^\d]/g, '');
  return cnpj
    .replace(/(\d{2})(\d)/, '$1.$2')
    .replace(/(\d{3})(\d)/, '$1.$2')
    .replace(/(\d{3})(\d)/, '$1/$2')
    .replace(/(\d{4})(\d)/, '$1-$2')
    .slice(0, 18);
};

const formatPhoneNumber = (value) => {
  if (!value) return value;
  const phoneNumber = value.replace(/[^\d]/g, '');
  const phoneNumberLength = phoneNumber.length;
  if (phoneNumberLength < 3) return `(${phoneNumber}`;
  if (phoneNumberLength < 8) return `(${phoneNumber.slice(0, 2)}) ${phoneNumber.slice(2)}`;
  if (phoneNumberLength < 12) return `(${phoneNumber.slice(0, 2)}) ${phoneNumber.slice(2, 7)}-${phoneNumber.slice(7, 11)}`;
  return `(${phoneNumber.slice(0, 2)}) ${phoneNumber.slice(2, 7)}-${phoneNumber.slice(7, 11)}`;
};

const formatZipCode = (value) => {
    if (!value) return value;
    const zipCode = value.replace(/[^\d]/g, '');
    return zipCode
        .replace(/(\d{5})(\d)/, '$1-$2')
        .slice(0, 9);
};


const SupplierCreation = () => {
  const navigate = useNavigate();
  const { theme } = useTheme();
  const [formData, setFormData] = useState({
    name: '',
    corporateName: '',
    cnpj: '',
    contactPerson: '',
    email: '',
    phoneNumber: '',
    notes: '', 
    addressDto: {
      street: '',
      number: '',
      city: '',
      state: '',
      zipCode: ''
    }
  });

  const [errors, setErrors] = useState({});
  const [isSubmitting, setIsSubmitting] = useState(false);
  
  const handleChange = (e) => {
    const { id, value } = e.target;

    let formattedValue = value;
    if (id === 'cnpj') {
        formattedValue = formatCnpj(value);
    } else if (id === 'phoneNumber') {
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
        addressDto: {
            ...prev.addressDto,
            [id]: formattedValue
        }
    }));
  }

  // --- Função de Validação Atualizada ---
  const validate = () => {
    const newErrors = {};
    const addressErrors = {};
    
    // Validação das Informações da Empresa
    if (!formData.name.trim()) newErrors.name = 'O nome fantasia é obrigatório.';
    if (!formData.corporateName.trim()) newErrors.corporateName = 'A razão social é obrigatória.';
    const cnpjDigits = formData.cnpj.replace(/[^\d]/g, '');
    if (!cnpjDigits) {
        newErrors.cnpj = 'O CNPJ é obrigatório.';
    } else if (cnpjDigits.length !== 14) {
        newErrors.cnpj = 'O CNPJ deve conter 14 dígitos.';
    }

    // Validação das Informações de Contato
    if (!formData.email.trim()) {
      newErrors.email = 'O email é obrigatório.';
    } else if (!/\S+@\S+\.\S+/.test(formData.email)) {
      newErrors.email = 'O formato do email é inválido.';
    }
    const phoneDigits = formData.phoneNumber.replace(/[^\d]/g, '');
    if (!phoneDigits) {
        newErrors.phoneNumber = 'O telefone é obrigatório.';
    } else if (phoneDigits.length < 10 || phoneDigits.length > 11) {
        newErrors.phoneNumber = 'O telefone deve ter 10 ou 11 dígitos.';
    }

    // Validação do Endereço
    if (!formData.addressDto.street.trim()) addressErrors.street = 'A rua é obrigatória.';
    if (!formData.addressDto.number.trim()) addressErrors.number = 'O número é obrigatório.';
    if (!formData.addressDto.city.trim()) addressErrors.city = 'A cidade é obrigatória.';
    if (!formData.addressDto.state.trim()) addressErrors.state = 'O estado é obrigatório.';
    const zipCodeDigits = formData.addressDto.zipCode.replace(/[^\d]/g, '');
    if(!zipCodeDigits){
        addressErrors.zipCode = 'O CEP é obrigatório.';
    } else if (zipCodeDigits.length !== 8){
        addressErrors.zipCode = 'O CEP deve conter 8 dígitos.'
    }
    
    if (Object.keys(addressErrors).length > 0) {
        newErrors.addressDto = addressErrors;
    }

    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (!validate()) {
        toast.warn('Por favor, corrija os erros no formulário.');
        return;
    }

    setIsSubmitting(true);
    
    try {
      const payload = {
        ...formData,
        addressDto: {
            ...formData.addressDto,
        }
      };
      console.log("Dados: ", payload);

      await api.post('/suppliers', payload);
      
      toast.success('Fornecedor cadastrado com sucesso!');
      navigate('/suppliers');

    } catch (error) {
      console.error("Erro ao cadastrar fornecedor:", error);
      const errorMessage = error.response?.data?.message || 'Falha ao criar fornecedor. Tente novamente.';
      toast.error(errorMessage);
    } finally {
      setIsSubmitting(false);
    }
  };

  return (
    <PageContainer>
      <Container>
        <TitleContainer>
          <Truck size={32} color={theme.colors.primary} />
          <Title>Adicionar Novo Fornecedor</Title>
        </TitleContainer>

        <Form onSubmit={handleSubmit} noValidate>
          <div>
            <SectionTitle>Informações da Empresa</SectionTitle>
            <FormRow>
              <FormGroup style={{flex: 2}}>
                <Label htmlFor="name">Nome Fantasia</Label>
                <Input type="text" id="name" value={formData.name} onChange={handleChange} className={errors.name ? 'error' : ''} />
                {errors.name && <ErrorMessage>{errors.name}</ErrorMessage>}
              </FormGroup>
              <FormGroup style={{flex: 3}}>
                <Label htmlFor="corporateName">Razão Social</Label>
                <Input type="text" id="corporateName" value={formData.corporateName} onChange={handleChange} className={errors.corporateName ? 'error' : ''}/>
                {errors.corporateName && <ErrorMessage>{errors.corporateName}</ErrorMessage>}
              </FormGroup>
              <FormGroup style={{flex: 2}}>
                <Label htmlFor="cnpj">CNPJ</Label>
                <Input type="text" id="cnpj" value={formData.cnpj} onChange={handleChange} className={errors.cnpj ? 'error' : ''} />
                {errors.cnpj && <ErrorMessage>{errors.cnpj}</ErrorMessage>}
              </FormGroup>
            </FormRow>
          </div>

          <div>
            <SectionTitle>Informações de Contato</SectionTitle>
            <FormRow>
              <FormGroup>
                <Label htmlFor="contactPerson">Pessoa de Contato</Label>
                <Input type="text" id="contactPerson" value={formData.contactPerson} onChange={handleChange} />
              </FormGroup>
              <FormGroup>
                <Label htmlFor="email">Email</Label>
                <Input type="email" id="email" value={formData.email} onChange={handleChange} className={errors.email ? 'error' : ''} />
                {errors.email && <ErrorMessage>{errors.email}</ErrorMessage>}
              </FormGroup>
              <FormGroup>
                <Label htmlFor="phoneNumber">Telefone</Label>
                <Input type="tel" id="phoneNumber" value={formData.phoneNumber} onChange={handleChange} className={errors.phoneNumber ? 'error' : ''} />
                {errors.phoneNumber && <ErrorMessage>{errors.phoneNumber}</ErrorMessage>}
              </FormGroup>
            </FormRow>
          </div>

          <div>
            <SectionTitle>Endereço</SectionTitle>
            <FormRow>
              <FormGroup style={{ flex: 3 }}>
                <Label htmlFor="street">Rua / Logradouro</Label>
                <Input type="text" id="street" value={formData.addressDto.street} onChange={handleAddressChange} className={errors.addressDto?.street ? 'error' : ''} />
                {errors.addressDto?.street && <ErrorMessage>{errors.addressDto.street}</ErrorMessage>}
              </FormGroup>
              <FormGroup style={{ flex: 1 }}>
                <Label htmlFor="number">Número</Label>
                <Input type="text" id="number" value={formData.addressDto.number} onChange={handleAddressChange} className={errors.addressDto?.number ? 'error' : ''} />
                {errors.addressDto?.number && <ErrorMessage>{errors.addressDto.number}</ErrorMessage>}
              </FormGroup>
            </FormRow>
            <FormRow>
              <FormGroup>
                <Label htmlFor="city">Cidade</Label>
                <Input type="text" id="city" value={formData.addressDto.city} onChange={handleAddressChange} className={errors.addressDto?.city ? 'error' : ''} />
                {errors.addressDto?.city && <ErrorMessage>{errors.addressDto.city}</ErrorMessage>}
              </FormGroup>
              <FormGroup>
                <Label htmlFor="state">Estado</Label>
                <Input type="text" id="state" value={formData.addressDto.state} onChange={handleAddressChange} className={errors.addressDto?.state ? 'error' : ''} />
                {errors.addressDto?.state && <ErrorMessage>{errors.addressDto.state}</ErrorMessage>}
              </FormGroup>
              <FormGroup>
                <Label htmlFor="zipCode">CEP</Label>
                <Input type="text" id="zipCode" value={formData.addressDto.zipCode} onChange={handleAddressChange} className={errors.addressDto?.zipCode ? 'error' : ''} />
                {errors.addressDto?.zipCode && <ErrorMessage>{errors.addressDto.zipCode}</ErrorMessage>}
              </FormGroup>
            </FormRow>
          </div>

          <div>
            <SectionTitle>Informações Adicionais</SectionTitle>
            <FormRow>
              <FormGroup>
                <Label htmlFor="notes">Observações</Label>
                <Textarea id="notes" value={formData.notes} onChange={handleChange} rows="4" />
              </FormGroup>
            </FormRow>
          </div>

          <ButtonContainer>
            <CancelButton type="button" onClick={() => navigate('/suppliers')}>
              <X size={20} style={{ marginRight: '0.5rem' }} />
              Cancelar
            </CancelButton>
            <Button type="submit" disabled={isSubmitting}>
              {isSubmitting ? 'Salvando...' : 'Salvar Fornecedor'}
            </Button>
          </ButtonContainer>
        </Form>
      </Container>
    </PageContainer>
  );
};

export default SupplierCreation;