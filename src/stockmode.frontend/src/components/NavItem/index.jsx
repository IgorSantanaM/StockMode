import React from "react";
import { NavItemLink } from "./styles";
import { NavLink } from "react-router-dom";

const NavItem = ({ icon, label, to }) => (
  <NavItemLink>
    <NavLink
      to={to}
      end 
      className={({ isActive }) =>
        `flex items-center px-4 py-3 text-md rounded-lg transition-colors ${
          isActive
            ? 'bg-indigo-600 text-white shadow-md' 
            : 'text-gray-600 hover:bg-gray-100'
        }`
      }
    >
      {icon}
      <span className="ml-3">{label}</span>
    </NavLink>
  </NavItemLink>
);

export default NavItem;