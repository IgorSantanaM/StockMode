import React from "react";
import { Zap, Home, ShoppingCart, Package, DollarSign, Users, Settings } from "lucide-react";
import { SidebarContainer, SidebarHeader, LogoText, Nav, HomeButton } from "./styles";
import NavItem from "../NavItem";
import { useNavigate } from "react-router-dom";

const SideBar = () => {
  var navigate = useNavigate();
    return(
        <SidebarContainer>
            <SidebarHeader>
                <Zap style={{ height: '2rem', width: '2rem', color: '#4f46e5' }} />
                <HomeButton onClick={() => navigate("/")}>
                  <LogoText>StockMode</LogoText>
                </HomeButton>
            </SidebarHeader>
          <Nav>
            <NavItem icon={<Home size={20} />} label="Início" to="/" />
            <NavItem icon={<ShoppingCart size={20} />} label="Vendas" to="/sales"/>
            <NavItem icon={<Package size={20} />} label="Produtos" to="/products"/>
            <NavItem icon={<DollarSign size={20} />} label="Financeiro" to="/financial"/>
            <NavItem icon={<Users size={20} />} label="Clientes" to="/customers"/>
          </Nav>
          <div style={{ padding: '1rem', borderTop: '1px solid #e5e7eb' }}>
            <NavItem icon={<Settings size={20} />} label="Configurações" to="/settings"/>
          </div>
        </SidebarContainer>
    );
}

export default SideBar;