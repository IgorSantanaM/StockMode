import React from 'react';
import { Zap, Home, ShoppingCart, Package, DollarSign, Users, Settings } from "lucide-react";
import { SidebarContainer, SidebarHeader, LogoText, Nav, Backdrop } from "./styles";
import NavItem from "../NavItem";

const Sidebar = ({ isMobileOpen, onClose }) => {
    return(
        <>
            <Backdrop isOpen={isMobileOpen} onClick={onClose} />
            <SidebarContainer isOpen={isMobileOpen}>
                <SidebarHeader to="/" onClick={onClose}>
                    <Zap style={{ height: '2rem', width: '2rem', color: '#4f46e5' }} />
                    <LogoText>StockMode</LogoText>
                </SidebarHeader>
              <Nav>
                <div onClick={onClose}><NavItem to="/" icon={<Home size={20} />} label="Início" /></div>
                <div onClick={onClose}><NavItem to="/sales" icon={<ShoppingCart size={20} />} label="Vendas" /></div>
                <div onClick={onClose}><NavItem to="/products" icon={<Package size={20} />} label="Produtos" /></div>
                <div onClick={onClose}><NavItem to="/financial" icon={<DollarSign size={20} />} label="Financeiro" /></div>
                <div onClick={onClose}><NavItem to="/customers" icon={<Users size={20} />} label="Clientes" /></div>
              </Nav>
              <div style={{ padding: '1rem', borderTop: '1px solid #e5e7eb' }}>
                <div onClick={onClose}><NavItem to="/settings" icon={<Settings size={20} />} label="Configurações" /></div>
              </div>
            </SidebarContainer>
        </>
    );
}

export default Sidebar;
