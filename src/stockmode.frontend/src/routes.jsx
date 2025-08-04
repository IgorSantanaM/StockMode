import React from 'react';
import { Routes, Route} from 'react-router-dom';

import Home from './pages/home';
import ProductRegistration from './pages/ProductRegistration';
import Login from './pages/Login';

export default function RoutesConfig(){
    return(
        <Routes>
            <Route path="/" exact element={<Home />} />
            <Route path="/Home" exact element={<Home />} />
            <Route path="/ProductRegistration" exact element={<ProductRegistration />} />
            <Route path="/signin-oidc" exact element={<Login />} />
        </Routes>
    )
}
