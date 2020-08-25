import * as React from "react";
import { render } from 'react-dom';
import { Admin, Resource } from 'react-admin';
import simpleRestProvider from 'ra-data-simple-rest';

import { ProdutosList, ProdutoEdit, ProdutoCreate, ProdutoIcon } from './produtos';

render(
    <Admin dataProvider={simpleRestProvider('http://localhost:5000/api/')}>
        <Resource name="produtos" list={ProdutosList} edit={ProdutoEdit} create={ProdutoCreate} icon={ProdutoIcon}/>
    </Admin>,
    document.getElementById('root')
);