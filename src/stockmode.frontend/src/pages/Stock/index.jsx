import React, { useState, useMemo } from 'react';
import { Layers, PlusCircle, Edit } from 'lucide-react';
import {
  PageContainer,
  PageHeader,
  TitleContainer,
  Title,
  PrimaryButton,
  Card,
  FilterContainer,
  Input,
  Select,
  Table,
  Th,
  Td,
  Tr,
  StockStatusBadge,
  ActionButton,
} from './styles';

// --- DADOS MOCK (Numa aplicação real, viriam da sua API) ---
const allVariations = [
  { id: 1, productName: 'Camiseta Gola V', variationName: 'Branca M', sku: 'CGV-BR-M', costPrice: 15.50, salePrice: 39.90, stockQuantity: 50 },
  { id: 2, productName: 'Camiseta Gola V', variationName: 'Preta G', sku: 'CGV-PT-G', costPrice: 15.50, salePrice: 39.90, stockQuantity: 8 },
  { id: 3, productName: 'Calça Jeans Skinny', variationName: 'Azul 42', sku: 'CJS-AZ-42', costPrice: 70.00, salePrice: 129.90, stockQuantity: 25 },
  { id: 4, productName: 'Calça Jeans Skinny', variationName: 'Preta 40', sku: 'CJS-PT-40', costPrice: 70.00, salePrice: 129.90, stockQuantity: 0 },
  { id: 5, productName: 'Vestido Floral', variationName: 'Estampa A - P', sku: 'VF-EA-P', costPrice: 85.00, salePrice: 159.90, stockQuantity: 15 },
  { id: 6, productName: 'Moletom com Capuz', variationName: 'Cinza G', sku: 'MCC-CZ-G', costPrice: 90.00, salePrice: 199.90, stockQuantity: 30 },
];

const Stock = () => {
  const [searchTerm, setSearchTerm] = useState('');
  const [statusFilter, setStatusFilter] = useState('Todos');

  const getStatus = (stock) => {
    if (stock === 0) return 'Esgotado';
    if (stock <= 10) return 'Baixo Estoque';
    return 'Em Estoque';
  };

  const filteredVariations = useMemo(() => {
    return allVariations.filter(variation => {
      const searchMatch = 
        variation.productName.toLowerCase().includes(searchTerm.toLowerCase()) ||
        variation.variationName.toLowerCase().includes(searchTerm.toLowerCase()) ||
        variation.sku.toLowerCase().includes(searchTerm.toLowerCase());
      
      const statusMatch = statusFilter === 'Todos' || getStatus(variation.stockQuantity) === statusFilter;
      
      return searchMatch && statusMatch;
    });
  }, [searchTerm, statusFilter]);

  const formatCurrency = (value) => {
    return value.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });
  };

  return (
    <PageContainer>
      <PageHeader>
        <TitleContainer>
          <Layers size={32} color="#4f46e5" />
          <Title>Controle de Estoque</Title>
        </TitleContainer>
        <PrimaryButton>
          <PlusCircle size={20} />
          Receber Mercadoria
        </PrimaryButton>
      </PageHeader>

      <Card>
        <FilterContainer>
          <Input 
            type="text" 
            placeholder="Buscar por produto, variação ou SKU..." 
            value={searchTerm}
            onChange={(e) => setSearchTerm(e.target.value)}
            style={{flexGrow: 1}}
          />
          <Select value={statusFilter} onChange={(e) => setStatusFilter(e.target.value)}>
            <option value="Todos">Todos os Status</option>
            <option value="Em Estoque">Em Estoque</option>
            <option value="Baixo Estoque">Baixo Estoque</option>
            <option value="Esgotado">Esgotado</option>
          </Select>
        </FilterContainer>

        <Table>
          <thead>
            <tr>
              <Th>Item</Th>
              <Th>SKU</Th>
              <Th>Preço de Custo</Th>
              <Th>Preço de Venda</Th>
              <Th>Quantidade</Th>
              <Th>Status</Th>
              <Th>Ações</Th>
            </tr>
          </thead>
          <tbody>
            {filteredVariations.map((variation) => (
              <Tr key={variation.id}>
                <Td>
                  <p style={{ fontWeight: 500 }}>{variation.productName}</p>
                  <p style={{ fontSize: '0.8rem', color: '#6b7280' }}>{variation.variationName}</p>
                </Td>
                <Td style={{ color: '#6b7280' }}>{variation.sku}</Td>
                <Td>{formatCurrency(variation.costPrice)}</Td>
                <Td style={{ fontWeight: 500 }}>{formatCurrency(variation.salePrice)}</Td>
                <Td style={{ fontWeight: 'bold' }}>{variation.stockQuantity}</Td>
                <Td>
                  <StockStatusBadge stock={variation.stockQuantity}>
                    {getStatus(variation.stockQuantity)}
                  </StockStatusBadge>
                </Td>
                <Td>
                  <div style={{display: 'flex', justifyContent: 'flex-end'}}>
                    <ActionButton title="Ajustar Estoque">
                      <Edit size={18} />
                    </ActionButton>
                  </div>
                </Td>
              </Tr>
            ))}
          </tbody>
        </Table>
        {filteredVariations.length === 0 && (
          <div style={{ padding: '2rem', textAlign: 'center', color: '#6b7280' }}>
            Nenhum item encontrado para os filtros selecionados.
          </div>
        )}
      </Card>
    </PageContainer>
  );
};

export default Stock;
