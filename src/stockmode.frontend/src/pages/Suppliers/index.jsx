import React, { useState, useMemo } from 'react';
import { Truck, PlusCircle, Edit, Trash2, Info } from 'lucide-react';
import {
  PageContainer,
  PageHeader,
  TitleContainer,
  Title,
  PrimaryButton,
  Card,
  FilterContainer,
  Input,
  Table,
  Th,
  Td,
  Tr,
  ActionButton,
} from './styles';
import { useNavigate } from 'react-router-dom';

const allSuppliers = [
  { id: 1, name: 'Fornecedor Têxtil S.A.', contactPerson: 'Carlos Pereira', email: 'carlos@textilsa.com', phone: '(11) 99999-1111' },
  { id: 2, name: 'Malhas & Cia', contactPerson: 'Fernanda Lima', email: 'fernanda@malhasecia.com.br', phone: '(21) 98888-2222' },
  { id: 3, name: 'Importados de Luxo', contactPerson: 'Roberto Martins', email: 'roberto@luxoimport.com', phone: '(41) 97777-3333' },
  { id: 4, name: 'Jeans Brasil', contactPerson: 'Mariana Costa', email: 'mariana.costa@jeansbr.com', phone: '(81) 96666-4444' },
];

const Suppliers = () => {
  const [searchTerm, setSearchTerm] = useState('');
  var navigate = useNavigate();

  const filteredSuppliers = useMemo(() => {
    return allSuppliers.filter(supplier =>
      supplier.name.toLowerCase().includes(searchTerm.toLowerCase()) ||
      supplier.email.toLowerCase().includes(searchTerm.toLowerCase())
    );
  }, [searchTerm]);

  return (
    <PageContainer>
      <PageHeader>
        <TitleContainer>
          <Truck size={32} color="#4f46e5" />
          <Title>Fornecedores</Title>
        </TitleContainer>
        <PrimaryButton onClick={() => navigate("/suppliers/register")}>
          <PlusCircle size={20} />
          Adicionar Fornecedor
        </PrimaryButton>
      </PageHeader>

      <Card>
        <FilterContainer>
          <Input 
            type="text" 
            placeholder="Buscar por nome ou email do fornecedor..." 
            value={searchTerm}
            onChange={(e) => setSearchTerm(e.target.value)}
          />
        </FilterContainer>

        <Table>
          <thead>
            <tr>
              <Th>Nome do Fornecedor</Th>
              <Th>Contacto Principal</Th>
              <Th>Email</Th>
              <Th>Telefone</Th>
              <Th>Ações</Th>
            </tr>
          </thead>
          <tbody>
            {filteredSuppliers.map((supplier) => (
              <Tr key={supplier.id}>
                <Td style={{ fontWeight: 500 }}>{supplier.name}</Td>
                <Td>{supplier.contactPerson}</Td>
                <Td style={{ color: '#6b7280' }}>{supplier.email}</Td>
                <Td>{supplier.phone}</Td>
                <Td>
                  <div style={{display: 'flex', justifyContent: 'flex-end', gap: '0.5rem'}}>
                    <ActionButton title="Editar Cliente">
                        <Info size={18} />
                    </ActionButton>
                    <ActionButton title="Editar Fornecedor">
                      <Edit size={18} />
                    </ActionButton>
                    <ActionButton title="Excluir Fornecedor">
                      <Trash2 size={18} />
                    </ActionButton>
                  </div>
                </Td>
              </Tr>
            ))}
          </tbody>
        </Table>
        {filteredSuppliers.length === 0 && (
          <div style={{ padding: '2rem', textAlign: 'center', color: '#6b7280' }}>
            Nenhum fornecedor encontrado.
          </div>
        )}
      </Card>
    </PageContainer>
  );
};

export default Suppliers;
