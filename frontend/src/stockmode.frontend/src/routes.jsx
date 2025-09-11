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
import ProtectedRoute from './components/ProtectedRoute';

export default function RoutesConfig(){
    return(
        <Routes>
            <Route path="/" exact element={<ProtectedRoute element={<Home />} />} />
            <Route path="/Home" exact element={<ProtectedRoute element={<Home />} />} />
            <Route path="/products" exact element={<ProtectedRoute element={<Products />} />} />
            <Route path="/products/create" exact element={<ProtectedRoute element={<ProductCreation />} />} />
            <Route path="/signin-oidc" exact element={<Login />} />
            <Route path="/sales" exact element={<ProtectedRoute element={<Sales />} />} />
            <Route path="/sales/create" exact element={<ProtectedRoute element={<NewSale />} />} />
            <Route path="/financial" exact element={<ProtectedRoute element={<Financial />} />}/>
            <Route path="/customers" exact element={<ProtectedRoute element={<Customers />} />}/>
            <Route path="/customers/create" exact element={<ProtectedRoute element={<CustomerCreation />} />}/>
            <Route path="/stock" exact element={<ProtectedRoute element={<Stock />} />}/>
            <Route path="/receivestock" exact element={<ProtectedRoute element={<ReceiveStock />} />}/>
            <Route path="/revenue/register" exact element={<ProtectedRoute element={<RegisterRevenue />} />} />
            <Route path="/expense/register" exact element={<ProtectedRoute element={<RegisterExpense />} />} />
            <Route path="/help" exact element={<ProtectedRoute element={<Help />} />} />
            <Route path="/profile" exact element={<ProtectedRoute element={<Profile />} />} />
            <Route path="/settings" exact element={<ProtectedRoute element={<SettingsPage />} />} />
            <Route path="/suppliers" exact element={<ProtectedRoute element={<Suppliers />} />} />
            <Route path="/suppliers/register" exact element={<ProtectedRoute element={<SupplierCreation />} />} />
            <Route path="/login" exact element={<Login />} />'
            <Route path="/authtest" exact element={<ProtectedRoute element={<AuthTest />} />} />
            <Route path="*" exact element={<ProtectedRoute element={<NotFound />} />} />
         </Routes>
    )
}
