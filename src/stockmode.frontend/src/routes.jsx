import React from 'react';
import { Routes, Route} from 'react-router-dom';


import Home from './pages/home';

export default function RoutesConfig(){
    return(
        <Routes>
            <Route path="/home" exact element={<Home />} />
        </Routes>
    )
}
