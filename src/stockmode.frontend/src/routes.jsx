import React from 'react';
import { Routes, Route} from 'react-router-dom';

import Home from './pages/home';
import ProductRegistration from './pages/ProductRegistration';

export default function RoutesConfig(){
    return(
        <Routes>
            <Route path="/" exact element={<Home />} />
            <Route path="/ProductRegistration" exact element={<ProductRegistration />} />
        </Routes>
    )
}
