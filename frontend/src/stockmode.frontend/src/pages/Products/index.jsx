import React, { useState, useEffect, useMemo } from 'react';
import { Package, PlusCircle, MoreVertical, Edit, Trash2, Info } from 'lucide-react';
import api from '../../services/api';
import {toast} from "react-toastify";
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

const Products = () => {
  var navigate = useNavigate();
  const [searchTerm, setSearchTerm] = useState('');
  const [categoryFilter, setCategoryFilter] = useState('Todos');
  const [isLoading, setIsLoading] = useState(true);
  const [products, setProducts] = useState([]); 

  useEffect(() => {
    const fetchProducts = async () =>{
      try{
        const response = await api.get('/products');
        console.log(response.data);
        setProducts(response.data);
      }catch(error){
        console.error("Failed to fetch products: ", error)
      }
      finally{
        setIsLoading(false);
      }
    };

    fetchProducts();
  }, []);

  const deleteProduct = async (id) =>{
    try{
      await api.delete('/products/' + id);
      toast.success("Produto excluído com sucesso!")
    }catch{
      toast.error("Erro ao excluir o produto.")
    }
  }

  const filteredProducts = useMemo(() => {
    return products.filter(product => {
      const searchMatch = product.name.toLowerCase().includes(searchTerm.toLowerCase());
      const categoryMatch = categoryFilter === 'Todos' || product.category === categoryFilter;
      return searchMatch && categoryMatch;
    });
  }, [products, searchTerm, categoryFilter]);

  const formatCurrency = (value) => value.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });

  const formatPriceRange = (min, max) => {
    if (!min && !max) return 'N/A';
    if (min === max) return formatCurrency(min);
    return `${formatCurrency(min)} - ${formatCurrency(max)}`;
  };

  if(isLoading){
    return (
    <PageContainer>
      <div style={{ padding: '2rem', textAlign: 'center', color: '#6b7280' }}>
        Carregando produtos...
      </div>
    </PageContainer>
  )
  }
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
            placeholder="Buscar por nome..." 
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
                <Td>{formatPriceRange(product.minSalePrice, product.maxSalePrice)}</Td>
                <Td>
                  <StockStatus stock={product.totalStockQuantity}>
                    {product.totalStockQuantity} unidades
                  </StockStatus>
                </Td>
                <Td>
                  <div style={{display: 'flex', justifyContent: 'flex-end', gap: '0.5rem'}}>
                    <ActionButton title="Informações sobre o Produto">
                      <Info size={18} />
                    </ActionButton>
                    <ActionButton title="Editar Produto">
                      <Edit size={18} />
                    </ActionButton>
                    <ActionButton title="Excluir Produto" onClick={() => deleteProduct(product.id)}>
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
