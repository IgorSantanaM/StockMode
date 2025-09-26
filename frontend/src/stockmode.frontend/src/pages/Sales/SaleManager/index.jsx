import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { 
  ShoppingCart, 
  Plus, 
  Minus, 
  Trash2, 
  Save, 
  Check, 
  X, 
  DollarSign, 
  CreditCard,
  User,
  AlertCircle,
  CheckCircle
} from 'lucide-react';
import api from '../../../services/api';
import {
  PageContainer,
  Container,
  Header,
  Title,
  StatusBadge,
  Section,
  SectionTitle,
  Card,
  Form,
  FormGroup,
  Label,
  Input,
  Select,
  Button,
  SecondaryButton,
  DangerButton,
  ItemsList,
  ItemCard,
  ItemInfo,
  ItemActions,
  PriceInfo,
  TotalSection,
  Alert,
  ActionButtons
} from './styles';

const PAYMENT_METHODS = [
  { value: 1, label: 'PIX' },
  { value: 2, label: 'Cartão de Crédito' },
  { value: 3, label: 'Cartão de Débito' },
  { value: 4, label: 'Dinheiro' },
  { value: 5, label: 'Crédito da Loja' }
];

const SALE_STATUS = {
  1: 'Pendente',
  2: 'Concluída',
  3: 'Cancelada'
};

const SaleManager = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const isNewSale = !id;
  
  const [sale, setSale] = useState(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);
  const [success, setSuccess] = useState(null);
  
  const [paymentMethod, setPaymentMethod] = useState(1); // Default to PIX
  const [discount, setDiscount] = useState(0);
  const [selectedCustomer, setSelectedCustomer] = useState('');
  const [customers, setCustomers] = useState([]);
  const [variations, setVariations] = useState([]);
  
  const [showAddItem, setShowAddItem] = useState(false);
  const [selectedVariation, setSelectedVariation] = useState('');
  const [quantity, setQuantity] = useState(1);

  useEffect(() => {
    loadInitialData();
    if (!isNewSale) {
      loadSale();
    }
  }, [id]);

  const loadInitialData = async () => {
    try {
      const [customersRes, variationsRes] = await Promise.all([
        api.get('customers?page=1&pageSize=100'),
        api.get('products/variations?page=1&pageSize=500')
      ]);
      
      console.log('Customers response:', customersRes.data);
      console.log('Variations response:', variationsRes.data);
      
      // Handle paginated API response format: { items: [], totalPages: 0, totalCount: 0 }
      const customersData = customersRes.data?.items || [];
      const variationsData = variationsRes.data?.items || [];
      
      console.log('Customers loaded:', customersData.length);
      console.log('Variations loaded:', variationsData.length);
      
      setCustomers(customersData);
      setVariations(variationsData);
    } catch (err) {
      setError('Erro ao carregar dados iniciais');
      console.error('Error loading initial data:', err);
    }
  };

  const loadSale = async () => {
    if (!id) return;
    
    setLoading(true);
    try {
      const response = await api.get(`sales/${id}`);
      setSale(response.data);
      setPaymentMethod(response.data.paymentMethod);
      setDiscount(response.data.discount || 0);
      setSelectedCustomer(response.data.customerId || '');
    } catch (err) {
      setError('Erro ao carregar venda');
      console.error('Error loading sale:', err);
    } finally {
      setLoading(false);
    }
  };

  const createSale = async () => {
    setLoading(true);
    setError(null);
    
    try {
      // Validate payment method
      if (!paymentMethod || paymentMethod < 1 || paymentMethod > 5) {
        setError('Selecione um método de pagamento válido');
        setLoading(false);
        return;
      }

      const payload = {
        paymentMethod: parseInt(paymentMethod)
      };
      
      console.log('Creating sale with payload:', payload);
      
      const response = await api.post('sales', payload);
      
      let saleId;
      
      if (response.data && typeof response.data === 'object') {
        saleId = response.data.id || response.data.Id;
      } else {
        saleId = response.data; 
      }
      
      if (!saleId && response.headers?.location) {
        const locationParts = response.headers.location.split('/');
        saleId = locationParts[locationParts.length - 1];
      }
      
      console.log('Sale created with ID:', saleId);
      
      if (saleId) {
        navigate(`/sales/manage/${saleId}`);
        setSuccess('Venda criada com sucesso!');
      } else {
        throw new Error('ID da venda não foi retornado pela API');
      }
      
    } catch (err) {
      console.error('Error creating sale:', err);
      
      if (err.response?.data?.errors) {
        const errorMessages = Object.values(err.response.data.errors).flat().join(', ');
        setError(`Erro ao criar venda: ${errorMessages}`);
      } else if (err.response?.data?.message) {
        setError(`Erro ao criar venda: ${err.response.data.message}`);
      } else {
        setError('Erro ao criar venda. Verifique os dados e tente novamente.');
      }
    } finally {
      setLoading(false);
    }
  };

  const addItem = async () => {
    console.log('addItem called');
    console.log('selectedVariation:', selectedVariation);
    console.log('quantity:', quantity);
    console.log('sale?.id:', sale?.id);
    
    if (!selectedVariation || quantity <= 0) {
      setError('Selecione um produto e quantidade válida');
      return;
    }

    if (!sale?.id) {
      setError('Venda não encontrada');
      return;
    }

    setError(null);
    setLoading(true);
    
    try {
      const payload = {
        saleId: parseInt(sale.id),
        variationId: parseInt(selectedVariation),
        quantity: parseInt(quantity)
      };
      
      console.log('Adding item with payload:', payload);
      
      await api.post('sales/add-item', payload);
      
      await loadSale(); 
      setSelectedVariation('');
      setQuantity(1);
      setShowAddItem(false); // Hide the add item form after successful addition
      setSuccess('Item adicionado com sucesso!');
    } catch (err) {
      console.error('Error adding item:', err);
      
      if (err.response?.data?.message) {
        setError(`Erro ao adicionar item: ${err.response.data.message}`);
      } else {
        setError('Erro ao adicionar item');
      }
    } finally {
      setLoading(false);
    }
  };

  const applyDiscount = async () => {
    if (!discount || discount < 0) {
      setError('Digite um valor de desconto válido');
      return;
    }

    if (parseFloat(discount) > (sale?.totalPrice || 0)) {
      setError('O desconto não pode ser maior que o valor total da venda');
      return;
    }
    
    setError(null);
    setLoading(true);
    
    try {
      const payload = {
        saleId: parseInt(sale.id),
        discountAmount: parseFloat(discount)
      };
      
      console.log('Applying discount with payload:', payload);
      
      await api.put('sales/apply-discount', payload);
      
      await loadSale();
      setSuccess('Desconto aplicado com sucesso!');
    } catch (err) {
      console.error('Error applying discount:', err);
      
      if (err.response?.data?.message) {
        setError(`Erro ao aplicar desconto: ${err.response.data.message}`);
      } else {
        setError('Erro ao aplicar desconto');
      }
    } finally {
      setLoading(false);
    }
  };

  const changePaymentMethod = async () => {
    if (!paymentMethod || paymentMethod < 1 || paymentMethod > 5) {
      setError('Selecione um método de pagamento válido');
      return;
    }

    setError(null);
    setLoading(true);
    
    try {
      const payload = {
        saleId: parseInt(sale.id),
        newPaymentMethod: parseInt(paymentMethod)
      };
      
      console.log('Changing payment method with payload:', payload);
      
      await api.put('sales/change-payment-method', payload);
      
      await loadSale();
      setSuccess('Método de pagamento alterado com sucesso!');
    } catch (err) {
      console.error('Error changing payment method:', err);
      
      if (err.response?.data?.message) {
        setError(`Erro ao alterar método de pagamento: ${err.response.data.message}`);
      } else {
        setError('Erro ao alterar método de pagamento');
      }
    } finally {
      setLoading(false);
    }
  };

  const completeSale = async () => {
    if (!selectedCustomer) {
      setError('Selecione um cliente para finalizar a venda');
      return;
    }

    if (!Array.isArray(sale.saleItems) || sale.saleItems.length === 0) {
      setError('Adicione pelo menos um item antes de finalizar a venda');
      return;
    }

    setError(null);
    
    try {
      console.log(`Completing sale ${sale.id} for customer ${selectedCustomer}`);
      
      await api.put(`sales/complete/${sale.id}/${selectedCustomer}`);
      
      await loadSale();
      setSuccess('Venda finalizada com sucesso! Email de confirmação enviado.');
    } catch (err) {
      console.error('Error completing sale:', err);
      
      if (err.response?.data?.message) {
        setError(`Erro ao finalizar venda: ${err.response.data.message}`);
      } else {
        setError('Erro ao finalizar venda');
      }
    }
  };

  const cancelSale = async () => {
    if (!confirm('Tem certeza que deseja cancelar esta venda?')) return;

    setError(null);
    
    try {
      console.log(`Canceling sale ${sale.id}`);
      
      await api.put(`sales/cancel/${sale.id}`);
      
      await loadSale();
      setSuccess('Venda cancelada com sucesso!');
    } catch (err) {
      console.error('Error canceling sale:', err);
      
      if (err.response?.data?.message) {
        setError(`Erro ao cancelar venda: ${err.response.data.message}`);
      } else {
        setError('Erro ao cancelar venda');
      }
    }
  };

  useEffect(() => {
    if (success) {
      const timer = setTimeout(() => setSuccess(null), 5000);
      return () => clearTimeout(timer);
    }
  }, [success]);

  useEffect(() => {
    if (error) {
      const timer = setTimeout(() => setError(null), 8000);
      return () => clearTimeout(timer);
    }
  }, [error]);

  if (loading && !sale) {
    return (
      <PageContainer>
        <Container>
          <div style={{ textAlign: 'center', padding: '40px' }}>
            <ShoppingCart size={48} color="#6b7280" />
            <p>Carregando...</p>
          </div>
        </Container>
      </PageContainer>
    );
  }

  if (isNewSale) {
    return (
      <PageContainer>
        <Container>
          <Header>
            <ShoppingCart size={32} color="#10b981" />
            <Title>Nova Venda</Title>
          </Header>

          {error && (
            <Alert type="error">
              <AlertCircle size={20} />
              {error}
            </Alert>
          )}

          {success && (
            <Alert type="success">
              <CheckCircle size={20} />
              {success}
            </Alert>
          )}

          <Card>
            <Form>
              <FormGroup>
                <Label>Método de Pagamento</Label>
                <Select 
                  value={paymentMethod} 
                  onChange={(e) => setPaymentMethod(parseInt(e.target.value))}
                  disabled={loading}
                  required
                >
                  <option value="">Selecione um método...</option>
                  {PAYMENT_METHODS.map(method => (
                    <option key={method.value} value={method.value}>
                      {method.label}
                    </option>
                  ))}
                </Select>
              </FormGroup>

              <ActionButtons>
                <SecondaryButton 
                  onClick={() => navigate('/sales')}
                  disabled={loading}
                >
                  <X size={20} />
                  Cancelar
                </SecondaryButton>
                <Button onClick={createSale} disabled={loading || !paymentMethod}>
                  <Plus size={20} />
                  {loading ? 'Criando...' : 'Criar Venda'}
                </Button>
              </ActionButtons>
            </Form>
          </Card>
        </Container>
      </PageContainer>
    );
  }

  if (!sale && !loading) {
    return (
      <PageContainer>
        <Container>
          <Header>
            <ShoppingCart size={32} color="#ef4444" />
            <Title>Venda não encontrada</Title>
          </Header>
          
          <Card>
            <div style={{ textAlign: 'center', padding: '40px' }}>
              <AlertCircle size={48} color="#ef4444" style={{ marginBottom: '16px' }} />
              <p>A venda solicitada não foi encontrada ou você não tem permissão para acessá-la.</p>
              <Button onClick={() => navigate('/sales')} style={{ marginTop: '16px' }}>
                Voltar para Vendas
              </Button>
            </div>
          </Card>
        </Container>
      </PageContainer>
    );
  }

  // More robust status checking - handle both number and string
  const saleStatus = sale?.status;
  const canModify = saleStatus === 1 || 
                    saleStatus === '1' || 
                    parseInt(saleStatus) === 1 ||
                    saleStatus === 'PaymentPending' ||
                    saleStatus === 'Pendente';
  
  console.log('Sale data:', sale);
  console.log('Sale status (raw):', saleStatus);
  console.log('Sale status type:', typeof saleStatus);
  console.log('Can modify:', canModify);

  return (
    <PageContainer>
      <Container>
        <Header>
          <ShoppingCart size={32} color="#10b981" />
          <div>
            <Title>Venda #{sale?.id}</Title>
            <StatusBadge status={sale?.status}>
              {SALE_STATUS[sale?.status] || 'Desconhecido'}
            </StatusBadge>
          </div>
        </Header>

        {error && (
          <Alert type="error">
            <AlertCircle size={20} />
            {error}
          </Alert>
        )}

        {success && (
          <Alert type="success">
            <CheckCircle size={20} />
            {success}
          </Alert>
        )}

        <Section>
          <SectionTitle>
            <ShoppingCart size={24} />
            Itens da Venda
            {canModify && sale.saleItems.length > 0 && (
              <Button onClick={() => {
                setShowAddItem(true);
              }}>
                <Plus size={16} />
                Adicionar Item
              </Button>
            )}
          </SectionTitle>

          {showAddItem && (
            <Card>
              <Form>
                <div style={{ marginBottom: '10px', padding: '10px', backgroundColor: '#f0f0f0' }}>
                  Debug: showAddItem = {showAddItem.toString()}, canModify = {canModify.toString()}
                </div>
                <FormGroup>
                  <Label>Produto</Label>
                  <Select 
                    value={selectedVariation} 
                    onChange={(e) => {
                      console.log('Variation selected:', e.target.value);
                      setSelectedVariation(e.target.value);
                    }}
                  >
                    <option value="">Selecione um produto...</option>
                    {Array.isArray(variations) && variations.map(variation => (
                      <option key={variation.id} value={variation.id}>
                        {variation.productName} - {variation.name} (R$ {variation.salePrice?.toFixed(2)})
                      </option>
                    ))}
                  </Select>
                </FormGroup>

                <FormGroup>
                  <Label>Quantidade</Label>
                  <Input 
                    type="number" 
                    min="1" 
                    value={quantity}
                    onChange={(e) => {
                      console.log('Quantity changed:', e.target.value);
                      setQuantity(e.target.value);
                    }}
                  />
                </FormGroup>

                <ActionButtons>
                  <SecondaryButton onClick={() => setShowAddItem(false)}>
                    Cancelar
                  </SecondaryButton>
                  <Button 
                    onClick={() => {
                      console.log('Add button clicked');
                      addItem();
                    }} 
                    disabled={loading || !selectedVariation || quantity <= 0}
                  >
                    <Plus size={16} />
                    {loading ? 'Adicionando...' : 'Adicionar'}
                  </Button>
                </ActionButtons>
              </Form>
            </Card>
          )}

          <ItemsList>
            {Array.isArray(sale?.saleItems) && sale.saleItems.length > 0 ? (
              sale.saleItems.map(item => (
                <ItemCard key={item.id}>
                  <ItemInfo>
                    <h4>{item.productName || 'Produto'}</h4>
                    <p>Quantidade: {item.quantity}</p>
                    <p>Preço unitário: R$ {item.priceAtSale?.toFixed(2)}</p>
                  </ItemInfo>
                  <PriceInfo>
                    R$ {(item.quantity * item.priceAtSale)?.toFixed(2)}
                  </PriceInfo>
                </ItemCard>
              ))
            ) : (
              <div style={{ textAlign: 'center', padding: '40px', color: '#6b7280' }}>
                <ShoppingCart size={48} color="#d1d5db" />
                <p>Nenhum item adicionado à venda</p>
                {canModify && (
                  <Button onClick={() => setShowAddItem(true)}>
                    <Plus size={16} />
                    Adicionar Primeiro Item
                  </Button>
                )}
              </div>
            )}
          </ItemsList>
        </Section>

        {canModify && (
          <Section>
            <SectionTitle>
              <CreditCard size={24} />
              Configurações da Venda
            </SectionTitle>

            <Card>
              <Form>
                <FormGroup>
                  <Label>Método de Pagamento</Label>
                  <div style={{ display: 'flex', gap: '10px', alignItems: 'center' }}>
                    <Select 
                      value={paymentMethod} 
                      onChange={(e) => setPaymentMethod(parseInt(e.target.value))}
                      style={{ flex: 1 }}
                    >
                      {PAYMENT_METHODS.map(method => (
                        <option key={method.value} value={method.value}>
                          {method.label}
                        </option>
                      ))}
                    </Select>
                    <Button onClick={changePaymentMethod} disabled={loading || !paymentMethod}>
                      <Save size={16} />
                      {loading ? 'Alterando...' : 'Alterar'}
                    </Button>
                  </div>
                </FormGroup>

                <FormGroup>
                  <Label>Desconto (R$)</Label>
                  <div style={{ display: 'flex', gap: '10px', alignItems: 'center' }}>
                    <Input 
                      type="number" 
                      step="0.01" 
                      min="0"
                      max={sale?.totalPrice || 0}
                      value={discount}
                      onChange={(e) => setDiscount(e.target.value)}
                      style={{ flex: 1 }}
                    />
                    <Button onClick={applyDiscount} disabled={loading || !discount || discount <= 0}>
                      <DollarSign size={16} />
                      {loading ? 'Aplicando...' : 'Aplicar'}
                    </Button>
                  </div>
                </FormGroup>

                <FormGroup>
                  <Label>Cliente</Label>
                  <Select 
                    value={selectedCustomer} 
                    onChange={(e) => setSelectedCustomer(e.target.value)}
                  >
                    <option value="">Selecione um cliente...</option>
                    {Array.isArray(customers) && customers.map(customer => (
                      <option key={customer.id} value={customer.id}>
                        {customer.name} - {customer.email}
                      </option>
                    ))}
                  </Select>
                </FormGroup>
              </Form>
            </Card>
          </Section>
        )}

        <TotalSection>
          <div>
            <strong>Total: R$ {sale?.totalPrice?.toFixed(2) || '0,00'}</strong>
            {sale?.discount > 0 && (
              <p>Desconto: R$ {sale?.discount?.toFixed(2)}</p>
            )}
            <h3>Total Final: R$ {sale?.finalPrice?.toFixed(2) || '0,00'}</h3>
          </div>
        </TotalSection>

        <ActionButtons>
          <SecondaryButton onClick={() => navigate('/sales')}>
            Voltar
          </SecondaryButton>
          
          {canModify && (
            <>
              <DangerButton onClick={cancelSale}>
                <X size={20} />
                Cancelar Venda
              </DangerButton>
              <Button 
                onClick={completeSale} 
                disabled={!selectedCustomer || !Array.isArray(sale?.saleItems) || sale.saleItems.length === 0}
              >
                <Check size={20} />
                Finalizar Venda
              </Button>
            </>
          )}
        </ActionButtons>
      </Container>
    </PageContainer>
  );
};

export default SaleManager;