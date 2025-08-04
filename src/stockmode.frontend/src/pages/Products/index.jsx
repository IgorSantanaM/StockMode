import React, { useState, useMemo } from 'react';
import { Package, PlusCircle, MoreVertical, Edit, Trash2 } from 'lucide-react';
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
  ProductImage,
  StockStatus,
  ActionButton,
} from './styles';
import { useNavigate } from 'react-router-dom';

const allProducts = [
  { id: 1, name: 'Camiseta Gola V', sku: 'CGV', minSalePrice: 39.90, maxSalePrice: 45.90, totalStockQuantity: 150, category: 'Roupas', image: 'https://placehold.co/100x100/FFFFFF/333333?text=Camiseta' },
  { id: 2, name: 'Calça Jeans Skinny', sku: 'CJS', minSalePrice: 129.90, maxSalePrice: 129.90, totalStockQuantity: 80, category: 'Roupas', image: 'https://placehold.co/100x100/336699/FFFFFF?text=Calça' },
  { id: 3, name: 'Vestido Floral', sku: 'VF', minSalePrice: 159.90, maxSalePrice: 159.90, totalStockQuantity: 5, category: 'Roupas', image: 'https://placehold.co/100x100/FFCCCC/333333?text=Vestido' },
  { id: 4, name: 'Moletom com Capuz', sku: 'MCC', minSalePrice: 199.90, maxSalePrice: 219.90, totalStockQuantity: 0, category: 'Roupas', image: 'https://placehold.co/100x100/CCCCCC/FFFFFF?text=Moletom' },
  { id: 5, name: 'Tênis de Corrida', sku: 'TNC', minSalePrice: 299.90, maxSalePrice: 349.90, totalStockQuantity: 45, category: 'Calçados', image: 'https://placehold.co/100x100/99FF99/333333?text=Tênis' },
];

const Products = () => {
  var navigate = useNavigate();
  const [searchTerm, setSearchTerm] = useState('');
  const [categoryFilter, setCategoryFilter] = useState('Todos');

  const filteredProducts = useMemo(() => {
    return allProducts.filter(product => {
      const searchMatch = product.name.toLowerCase().includes(searchTerm.toLowerCase()) ||
                          product.sku.toLowerCase().includes(searchTerm.toLowerCase());
      const categoryMatch = categoryFilter === 'Todos' || product.category === categoryFilter;
      return searchMatch && categoryMatch;
    });
  }, [searchTerm, categoryFilter]);

  const formatCurrency = (value) => value.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });

  const formatPriceRange = (min, max) => {
    if (!min && !max) return 'N/A';
    if (min === max) return formatCurrency(min);
    return `${formatCurrency(min)} - ${formatCurrency(max)}`;
  };

  return (
    <PageContainer>
      <PageHeader>
        <TitleContainer>
          <Package size={32} color="#4f46e5" />
          <Title>Meus Produtos</Title>
        </TitleContainer>
        <PrimaryButton onClick={() => navigate("/Products/Create")} >
          <PlusCircle size={20} />
          Adicionar Novo Produto
        </PrimaryButton>
      </PageHeader>

      <Card>
        <FilterContainer>
          <Input 
            type="text" 
            placeholder="Buscar por nome ou SKU..." 
            value={searchTerm}
            onChange={(e) => setSearchTerm(e.target.value)}
            style={{flexGrow: 1}}
          />
          <Select value={categoryFilter} onChange={(e) => setCategoryFilter(e.target.value)}>
            <option value="Todos">Todas as Categorias</option>
            <option value="Roupas">Roupas</option>
            <option value="Calçados">Calçados</option>
            <option value="Acessórios">Acessórios</option>
          </Select>
        </FilterContainer>

        <Table>
          <thead>
            <tr>
              <Th style={{width: '80px'}}></Th>
              <Th>Produto</Th>
              <Th>SKU</Th>
              <Th>Preço de Venda</Th>
              <Th>Estoque Total</Th>
              <Th>Ações</Th>
            </tr>
          </thead>
          <tbody>
            {filteredProducts.map((product) => (
              <Tr key={product.id}>
                <Td>
                  <ProductImage src={product.image} alt={product.name} />
                </Td>
                <Td style={{ fontWeight: 500 }}>{product.name}</Td>
                <Td style={{ color: '#6b7280' }}>{product.sku}</Td>
                <Td>{formatPriceRange(product.minSalePrice, product.maxSalePrice)}</Td>
                <Td>
                  <StockStatus stock={product.totalStockQuantity}>
                    {product.totalStockQuantity} unidades
                  </StockStatus>
                </Td>
                <Td>
                  <div style={{display: 'flex', justifyContent: 'flex-end', gap: '0.5rem'}}>
                    <ActionButton title="Editar Produto">
                      <Edit size={18} />
                    </ActionButton>
                    <ActionButton title="Excluir Produto">
                      <Trash2 size={18} />
                    </ActionButton>
                  </div>
                </Td>
              </Tr>
            ))}
          </tbody>
        </Table>
        {filteredProducts.length === 0 && (
          <div style={{ padding: '2rem', textAlign: 'center', color: '#6b7280' }}>
            Nenhum produto encontrado para os filtros selecionados.
          </div>
        )}
      </Card>
    </PageContainer>
  );
};

export default Products;
