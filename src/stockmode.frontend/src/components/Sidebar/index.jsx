import React from "react";
import { Zap, Home, ShoppingCart, Package, DollarSign, Users, Settings } from "lucide-react";
import { SidebarContainer, SidebarHeader, LogoText, Nav } from "./styles";
import NavItemComponent from "../NavItemComponent";

const SideBar = () => {
    return(
        <SidebarContainer>
            <SidebarHeader>
                <Zap style={{ height: '2rem', width: '2rem', color: '#4f46e5' }} />
                <LogoText>StockMode</LogoText>
            </SidebarHeader>
          <Nav>
            <NavItemComponent icon={<Home size={20} />} label="Início" active />
            <NavItemComponent icon={<ShoppingCart size={20} />} label="Vendas" />
            <NavItemComponent icon={<Package size={20} />} label="Produtos" />
            <NavItemComponent icon={<DollarSign size={20} />} label="Financeiro" />
            <NavItemComponent icon={<Users size={20} />} label="Clientes" />
          </Nav>
          <div style={{ padding: '1rem', borderTop: '1px solid #e5e7eb' }}>
            <NavItemComponent icon={<Settings size={20} />} label="Configurações" />
          </div>
        </SidebarContainer>
    );
}

export default SideBar;