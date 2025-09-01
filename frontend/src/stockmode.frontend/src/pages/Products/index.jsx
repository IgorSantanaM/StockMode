import React, { useState, useEffect, useMemo } from 'react';
import { Package, PlusCircle, MoreVertical, Edit, Trash2, Info, SearchCheck } from 'lucide-react';
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
  PaginationContainer,
  PageInfo, 
  PaginationButton
} from './styles';
import { useNavigate } from 'react-router-dom';

const ITEMS_PER_PAGE = 10;

const Products = () => {
  var navigate = useNavigate();
  const [searchTerm, setSearchTerm] = useState('');
  const [categoryFilter, setCategoryFilter] = useState('Todos');
  const [isLoading, setIsLoading] = useState(true);
  const [products, setProducts] = useState([]); 

  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(0);

  useEffect(() => {
    setCurrentPage(1);
  }, [searchTerm, categoryFilter])

  useEffect(() => {
    const fetchProducts = async () =>{
      try{
        const params = new URLSearchParams();
        if (searchTerm) params.append('name', searchTerm);
        params.append('page', currentPage);
        params.append('pageSize', ITEMS_PER_PAGE)
        const response = await api.get(`/products?${params.toString()}`);
        console.log(response.data);
        setProducts(response.data.items);
        setTotalPages(response.data.totalPages);
      }catch(error){
        console.error("Failed to fetch products: ", error)
      }
      finally{
        setIsLoading(false);
      }
    };

    const handler = setTimeout(() => {
      fetchProducts();
    }, 500);

    return () => {
      clearTimeout(handler);
    };
  }, [searchTerm, currentPage]);


  const deleteProduct = async (id) =>{
    try{
      await api.delete('/products/' + id);
      toast.success("Produto excluído com sucesso!")
    }catch{
      toast.error("Erro ao excluir o produto.")
    }
  }

  const handlePageChange = (page) => {
    if (page >= 1 && page <= totalPages && page !== currentPage) {
      setCurrentPage(page);
    }
  };

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
              {/* <Th style={{width: '80px'}}></Th> */}
              <Th>Produto</Th>
              <Th>Preço de Venda</Th>
              <Th>Estoque Total</Th>
              <Th>Ações</Th>
            </tr>
          </thead>
          <tbody>
            {!isLoading && filteredProducts.map((product) => (
              <Tr key={product.id}>
                {/* <Td>
                  <ProductImage src={product.image} alt={product.name} />
                </Td> */}
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

        {isLoading && (
          <div style={{ padding: '2rem', textAlign: 'center', color: '#6b7280' }}>
            Carregando...
          </div>
        )}
        {!isLoading && products.length === 0 && (
          <div style={{ padding: '2rem', textAlign: 'center', color: '#6b7280' }}>
            Nenhum produto encontrado para os filtros selecionados.
          </div>
        )}
        {!isLoading && totalPages > 0 && (
                  <PaginationContainer>
                    <PageInfo>
                      Página {currentPage} de {totalPages}
                    </PageInfo>
                    <div style={{display: 'flex', gap: '0.5rem'}}>
                      <PaginationButton
                        onClick={() => handlePageChange(currentPage - 1)}
                        disabled={currentPage === 1}
                      >
                        Anterior
                      </PaginationButton>
                      <PaginationButton
                        onClick={() => handlePageChange(currentPage + 1)}
                        disabled={currentPage === totalPages}
                      >
                        Próxima
                      </PaginationButton>
                    </div>
                  </PaginationContainer>
                )}
      </Card>
    </PageContainer>
  );
};

export default Products;
