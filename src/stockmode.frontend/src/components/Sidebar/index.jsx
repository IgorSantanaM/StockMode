import React from "react";
import { Zap, Home, ShoppingCart, Package, DollarSign, Users, Settings, NonBinary } from "lucide-react";
import { SidebarContainer, SidebarHeader, LogoText, Nav } from "./styles";
import NavItem from "../NavItem";

const SideBar = () => {
    return(
        <SidebarContainer>
            <SidebarHeader>
                <Zap style={{ height: '2rem', width: '2rem', color: '#4f46e5' }} />
                <LogoText>StockMode</LogoText>
            </SidebarHeader>
          <Nav>
            <NavItem icon={<Home size={20} />} label="Início" active />
            <NavItem icon={<ShoppingCart size={20} />} label="Vendas" to="/sales"/>
            <NavItem icon={<Package size={20} />} label="Produtos" to="/products"/>
            <NavItem icon={<DollarSign size={20} />} label="Financeiro" to="/financial"/>
            <NavItem icon={<Users size={20} />} label="Clientes" to="/customers"/>
          </Nav>
          <div style={{ padding: '1rem', borderTop: '1px solid #e5e7eb' }}>
            <NavItem icon={<Settings size={20} />} label="Configurações" />
          </div>
        </SidebarContainer>
    );
}

export default SideBar;