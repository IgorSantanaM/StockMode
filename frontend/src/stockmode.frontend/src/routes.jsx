import React from 'react';
import { Routes, Route} from 'react-router-dom';

import Home from './pages/home';
import Login from './pages/Login';
import Sales from './pages/Sales';
import Products from './pages/Products';
import Financial from './pages/Financial';
import Customers from './pages/Customers';
import Stock from './pages/Stock';
import ReceiveStock from './pages/ReceiveStock';
import NewSale from './pages/Sales/Create';
import ProductCreation from './pages/Products/Create';
import CustomerCreation from './pages/Customers/Create';
import RegisterRevenue from './pages/RegisterRevenue';
import RegisterExpense from './pages/RegisterExpense';
import NotFound from './pages/NotFound';
import Help from './pages/Help';
import Profile from './pages/Profile';
import SettingsPage from './pages/Settings';
import Suppliers from './pages/Suppliers';
import SupplierCreation from './pages/Suppliers/Create';
import AuthTest from './pages/AuthTest';

export default function RoutesConfig(){
    return(
        <Routes>
            <Route path="/" exact element={<Home />} />
            <Route path="/Home" exact element={<Home />} />
            <Route path="/products" exact element={<Products />} />
            <Route path="/products/create" exact element={<ProductCreation />} />
            <Route path="/signin-oidc" exact element={<Login />} />
            <Route path="/sales" exact element={<Sales />} />
            <Route path="/sales/create" exact element={<NewSale />} />
            <Route path="/financial" exact element={<Financial />}/>
            <Route path="/customers" exact element={<Customers />}/>
            <Route path="/customers/create" exact element={<CustomerCreation />}/>
            <Route path="/stock" exact element={<Stock />}/>
            <Route path="/receivestock" exact element={<ReceiveStock />}/>
            <Route path="/revenue/register" exact element={<RegisterRevenue />} />
            <Route path="/expense/register" exact element={<RegisterExpense />} />
            <Route path="/help" exact element={<Help />} />
            <Route path="/profile" exact element={<Profile />} />
            <Route path="/settings" exact element={<SettingsPage />} />
            <Route path="/suppliers" exact element={<Suppliers />} />
            <Route path="/suppliers/register" exact element={<SupplierCreation />} />
            <Route path="/login" exact element={<Login />} />'
            <Route path="/authtest" exact element={<AuthTest />} />
            <Route path="*" exact element={<NotFound />} />
         </Routes>
    )
}
