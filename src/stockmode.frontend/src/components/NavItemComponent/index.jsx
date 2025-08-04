import React from "react";
import { NavItemLink } from "./styles";

const NavItemComponent = ({ icon, label, active }) => (
  <NavItemLink href="#" active={active}>
    {icon}
    <span>{label}</span>
  </NavItemLink>
);

export default NavItemComponent;