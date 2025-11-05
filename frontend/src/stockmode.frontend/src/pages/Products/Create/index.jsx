import React, { useState } from 'react';
import { PackagePlus, X, Plus, Trash2 } from 'lucide-react';
import { useTheme } from '../../../contexts/ThemeContext';
import {
  PageContainer, Container, TitleContainer, Title, Form, FormGroup, Label, Input,
  Textarea, ErrorMessage, ButtonContainer, Button, CancelButton, SectionTitle,
  VariationsContainer, VariationCard, VariationHeader, VariationTitle, RemoveButton,
  VariationFormRow, AddVariationButton
} from './styles';
import api from '../../../services/api';
import { useNavigate } from 'react-router-dom';

const ProductCreation = () => {
  const navigate = useNavigate();
  const { theme } = useTheme();
  const [name, setName] = useState('');
  const [description, setDescription] = useState('');
  const [variations, setVariations] = useState([
    { name: '', sku: '', costPrice: '', salePrice: '', stockQuantity: '' }
  ]);
  const [errors, setErrors] = useState({});
  const [isSubmitting, setIsSubmitting] = useState(false);

  const handleVariationChange = (index, field, value) => {
    const newVariations = [...variations];
    newVariations[index][field] = value;
    setVariations(newVariations);
  };

  const handleAddVariation = () => {
    setVariations([...variations, { name: '', sku: '', costPrice: '', salePrice: '', stockQuantity: '' }]);
  };

  const handleRemoveVariation = (index) => {
    const newVariations = variations.filter((_, i) => i !== index);
    setVariations(newVariations);
  };

  const validate = () => {
    const newErrors = {};
    if (!name.trim()) newErrors.name = 'O nome do produto é obrigatório.';
    
    const variationErrors = [];
    variations.forEach((v, index) => {
      const err = {};
      if (!v.name.trim()) err.name = 'Nome da variação é obrigatório.';
      if (!v.sku.trim()) err.sku = 'SKU é obrigatório.';
      if (v.salePrice === '' || isNaN(Number(v.salePrice))) err.salePrice = 'Preço de venda inválido.';
      if (v.stockQuantity === '' || !Number.isInteger(Number(v.stockQuantity)) || Number(v.stockQuantity) < 0) err.stockQuantity = 'Estoque inválido.';
      if (Object.keys(err).length > 0) variationErrors[index] = err;
    });

    if (variationErrors.length > 0) newErrors.variations = variationErrors;
    
    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (!validate()) return;

    setIsSubmitting(true);
    
    const commandPayload = {
      name,
      description,
      variations: variations.map(v => ({
        name: v.name,
        sku: v.sku,
        costPrice: parseFloat(v.costPrice) || 0,
        salePrice: parseFloat(v.salePrice),
        stockQuantity: parseInt(v.stockQuantity, 10)
      }))
    };

    console.log("Enviando para a API:", commandPayload);

    try {
      const response = await api.post("products", commandPayload);
      console.log("Data send successfully! ", response.data);

      navigate("/products");
    } catch (error) {
      console.error("Erro ao cadastrar produto:", error);
      alert('Erro ao cadastrar produto. Verifique o console.');
    } finally {
      setIsSubmitting(false);
    }
  };

  return (
    <PageContainer>
      <Container>
        <TitleContainer>
          <PackagePlus size={32} color={theme.colors.primary} />
          <Title>Cadastrar Novo Produto</Title>
        </TitleContainer>

        <Form onSubmit={handleSubmit} noValidate>
          <div>
            <SectionTitle>Informações Principais</SectionTitle>
            <FormGroup style={{marginTop: '1.5rem'}}>
              <Label htmlFor="name">Nome do Produto</Label>
              <Input type="text" id="name" value={name} onChange={(e) => setName(e.target.value)} className={errors.name ? 'error' : ''} />
              {errors.name && <ErrorMessage>{errors.name}</ErrorMessage>}
            </FormGroup>
            <FormGroup style={{marginTop: '1.5rem'}}>
              <Label htmlFor="description">Descrição</Label>
              <Textarea id="description" value={description} onChange={(e) => setDescription(e.target.value)} />
            </FormGroup>
          </div>

          <div>
            <SectionTitle>Variações e Estoque</SectionTitle>
            <VariationsContainer style={{marginTop: '1.5rem'}}>
              {variations.map((v, index) => (
                <VariationCard key={index}>
                  <VariationHeader>
                    <VariationTitle>Variação #{index + 1}</VariationTitle>
                    {variations.length > 1 && (
                      <RemoveButton type="button" onClick={() => handleRemoveVariation(index)}>
                        <Trash2 size={20} />
                      </RemoveButton>
                    )}
                  </VariationHeader>
                  
                  <VariationFormRow>
                    <FormGroup>
                      <Label htmlFor={`var-name-${index}`}>Nome da Variação (Ex: Branca - M)</Label>
                      <Input type="text" id={`var-name-${index}`} value={v.name} onChange={(e) => handleVariationChange(index, 'name', e.target.value)} className={errors.variations?.[index]?.name ? 'error' : ''} />
                      {errors.variations?.[index]?.name && <ErrorMessage>{errors.variations[index].name}</ErrorMessage>}
                    </FormGroup>
                    <FormGroup>
                      <Label htmlFor={`var-sku-${index}`}>SKU (Código)</Label>
                      <Input type="text" id={`var-sku-${index}`} value={v.sku} onChange={(e) => handleVariationChange(index, 'sku', e.target.value)} className={errors.variations?.[index]?.sku ? 'error' : ''} />
                      {errors.variations?.[index]?.sku && <ErrorMessage>{errors.variations[index].sku}</ErrorMessage>}
                    </FormGroup>
                  </VariationFormRow>
                  
                  <VariationFormRow>
                     <FormGroup>
                      <Label htmlFor={`var-cost-${index}`}>Preço de Custo (R$)</Label>
                      <Input type="number" step="0.01" id={`var-cost-${index}`} value={v.costPrice} onChange={(e) => handleVariationChange(index, 'costPrice', e.target.value)} />
                    </FormGroup>
                    <FormGroup>
                      <Label htmlFor={`var-sale-${index}`}>Preço de Venda (R$)</Label>
                      <Input type="number" step="0.01" id={`var-sale-${index}`} value={v.salePrice} onChange={(e) => handleVariationChange(index, 'salePrice', e.target.value)} className={errors.variations?.[index]?.salePrice ? 'error' : ''} />
                      {errors.variations?.[index]?.salePrice && <ErrorMessage>{errors.variations[index].salePrice}</ErrorMessage>}
                    </FormGroup>
                  </VariationFormRow>

                   <FormGroup>
                      <Label htmlFor={`var-stock-${index}`}>Quantidade em Estoque</Label>
                      <Input type="number" id={`var-stock-${index}`} value={v.stockQuantity} onChange={(e) => handleVariationChange(index, 'stockQuantity', e.target.value)} className={errors.variations?.[index]?.stockQuantity ? 'error' : ''} />
                      {errors.variations?.[index]?.stockQuantity && <ErrorMessage>{errors.variations[index].stockQuantity}</ErrorMessage>}
                    </FormGroup>
                </VariationCard>
              ))}
              <AddVariationButton type="button" onClick={handleAddVariation}>
                <Plus size={20} /> Adicionar outra variação
              </AddVariationButton>
            </VariationsContainer>
          </div>

          <ButtonContainer>
            <CancelButton type="button" onClick={() => navigate('/Products')}>
              <X size={20} style={{ marginRight: '0.5rem' }} />
              Cancelar
            </CancelButton>
            <Button type="submit" disabled={isSubmitting}>
              {isSubmitting ? 'Salvando...' : 'Salvar Produto'}
            </Button>
          </ButtonContainer>
        </Form>
      </Container>
    </PageContainer>
  );
};

export default ProductCreation;
