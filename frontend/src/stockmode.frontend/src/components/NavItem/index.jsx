import React from "react";
import { NavItemLink } from "./styles";

const NavItem = ({ to, icon, label }) => {
  return (
    <NavItemLink to={to} end>
      {icon}
      <span>{label}</span>
    </NavItemLink>
  );
};

export default NavItem;

