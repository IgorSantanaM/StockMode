import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { UserPlus, X, CheckCircle, AlertCircle, Tag, Plus } from 'lucide-react';
import { useTheme } from '../../../contexts/ThemeContext';
import api from '../../../services/api'; 
import {
  PageContainer, Container, TitleContainer, Title, Form, FormGroup, Label, Input,
  ErrorMessage, ButtonContainer, Button, CancelButton, SectionTitle, FormRow, Alert,
  TagsContainer, TagBadge, TagInput, CreateTagButton, SelectedTagsContainer, RemoveTagButton
} from './styles';

const CustomerCreation = () => {
  const navigate = useNavigate();
  const { theme } = useTheme();
  
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
  
  // Tags state
  const [availableTags, setAvailableTags] = useState([]);
  const [selectedTags, setSelectedTags] = useState([]);
  const [newTagName, setNewTagName] = useState('');
  const [newTagColor, setNewTagColor] = useState('#4f46e5');
  const [showCreateTag, setShowCreateTag] = useState(false);
  const [loadingTags, setLoadingTags] = useState(false);

  useEffect(() => {
    fetchTags();
  }, []);

  const fetchTags = async () => {
    try {
      setLoadingTags(true);
      const response = await api.get('/tags');
      setAvailableTags(response.data?.items || response.data || []);
    } catch (error) {
      console.error('Error fetching tags:', error);
    } finally {
      setLoadingTags(false);
    }
  };

  const handleCreateTag = async () => {
    if (!newTagName.trim()) {
      alert('Por favor, insira um nome para a tag.');
      return;
    }

    try {
      await api.post('/tags', {
        name: newTagName.trim(),
        color: newTagColor
      });
      
      // Refetch all tags to get the updated list
      await fetchTags();
      
      setNewTagName('');
      setNewTagColor('#4f46e5');
      setShowCreateTag(false);
    } catch (error) {
      console.error('Error creating tag:', error);
      alert('Erro ao criar tag. Por favor, tente novamente.');
    }
  };

  const handleAddTag = (tag) => {
    if (!selectedTags.find(t => t.id === tag.id)) {
      setSelectedTags([...selectedTags, tag]);
    }
  };

  const handleRemoveTag = (tagId) => {
    setSelectedTags(selectedTags.filter(t => t.id !== tagId));
  };

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
      const customerId = response.data.id || response.data;
      
      // Add tags to customer if any selected
      if (selectedTags.length > 0 && customerId) {
        for (const tag of selectedTags) {
          try {
            await api.patch('/customers/add-tag', {
              customerId: customerId,
              tagId: tag.id
            });
          } catch (tagError) {
            console.error('Error adding tag to customer:', tagError);
          }
        }
      }
      
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
          <UserPlus size={32} color={theme.colors.primary} />
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

          <div>
            <SectionTitle>
              <Tag size={20} style={{ marginRight: '0.5rem' }} />
              Tags (Opcional)
            </SectionTitle>
            
            {selectedTags.length > 0 && (
              <SelectedTagsContainer>
                {selectedTags.map(tag => (
                  <TagBadge key={tag.id} color={tag.color}>
                    {tag.name}
                    <RemoveTagButton onClick={() => handleRemoveTag(tag.id)}>
                      <X size={14} />
                    </RemoveTagButton>
                  </TagBadge>
                ))}
              </SelectedTagsContainer>
            )}

            <FormGroup>
              <Label>Tags Disponíveis</Label>
              <TagsContainer>
                {loadingTags && (
                  <p key="loading" style={{ color: '#6b7280', fontSize: '0.875rem' }}>Carregando tags...</p>
                )}
                
                {!loadingTags && availableTags.length === 0 && (
                  <p key="no-tags" style={{ color: '#6b7280', fontSize: '0.875rem' }}>
                    Nenhuma tag disponível. Crie uma nova tag.
                  </p>
                )}
                
                {!loadingTags && availableTags.length > 0 && (
                  <React.Fragment key="tags-list">
                    {availableTags
                      .filter(tag => !selectedTags.find(st => st.id === tag.id))
                      .map(tag => (
                        <TagBadge 
                          key={tag.id} 
                          color={tag.color} 
                          onClick={() => handleAddTag(tag)}
                          style={{ cursor: 'pointer' }}
                        >
                          + {tag.name}
                        </TagBadge>
                      ))}
                    
                    {availableTags.filter(tag => !selectedTags.find(st => st.id === tag.id)).length === 0 && (
                      <p key="all-added" style={{ color: '#6b7280', fontSize: '0.875rem' }}>
                        Todas as tags foram adicionadas.
                      </p>
                    )}
                  </React.Fragment>
                )}
                
                {!showCreateTag && (
                  <CreateTagButton key="create-button" onClick={() => setShowCreateTag(true)}>
                    <Plus size={16} /> Nova Tag
                  </CreateTagButton>
                )}
              </TagsContainer>

              {showCreateTag && (
                <div style={{ marginTop: '1rem', padding: '1rem', backgroundColor: theme.colors.backgroundSecondary, borderRadius: '0.5rem' }}>
                  <FormRow>
                    <FormGroup>
                      <Label htmlFor="tagName">Nome da Tag</Label>
                      <TagInput
                        type="text"
                        id="tagName"
                        value={newTagName}
                        onChange={(e) => setNewTagName(e.target.value)}
                        placeholder="Ex: VIP, Atacado, Frequente..."
                      />
                    </FormGroup>
                    <FormGroup>
                      <Label htmlFor="tagColor">Cor</Label>
                      <Input
                        type="color"
                        id="tagColor"
                        value={newTagColor}
                        onChange={(e) => setNewTagColor(e.target.value)}
                        style={{ height: '40px', cursor: 'pointer' }}
                      />
                    </FormGroup>
                  </FormRow>
                  <div style={{ display: 'flex', gap: '0.5rem', marginTop: '0.75rem' }}>
                    <Button type="button" onClick={handleCreateTag} style={{ flex: 1 }}>
                      Criar Tag
                    </Button>
                    <CancelButton type="button" onClick={() => {
                      setShowCreateTag(false);
                      setNewTagName('');
                      setNewTagColor('#4f46e5');
                    }}>
                      Cancelar
                    </CancelButton>
                  </div>
                </div>
              )}
            </FormGroup>
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

