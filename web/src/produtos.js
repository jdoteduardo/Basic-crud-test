import * as React from "react";
import { List, Filter, SearchInput, NumberField, NumberInput, Datagrid, Edit, Create, SimpleForm, DateField, TextField, EditButton, TextInput, DateInput } from 'react-admin';
import BookIcon from '@material-ui/icons/Book';
export const ProdutoIcon = BookIcon;

const ProdutoFilter = props => (
    <Filter {...props}>
        <SearchInput source="q" alwaysOn />
    </Filter>
);

export const ProdutosList = (props) => (
    <List 
        {...props}
        filters={<ProdutoFilter />}>
        <Datagrid>
            <TextField source="id" />
            <TextField source="nome" />
            <NumberField source="quantidade" />
            <NumberField source="valor" />
            <EditButton basePath="/produtos" />
        </Datagrid>
    </List>
);

const ProdutoTitle = ({ record }) => {
    return <span>Produtos</span>;
};

export const ProdutoEdit = (props) => (
    <Edit title={<ProdutoTitle />} {...props}>
        <SimpleForm>
            <TextInput disabled source="ID" />
            <TextInput source="nome" />
            <NumberInput source="quantidade" />
            <NumberInput source="valor" />
        </SimpleForm>
    </Edit>
);

export const ProdutoCreate = (props) => (
    <Create title="Create a Post" {...props}>
        <SimpleForm>
        <TextInput disabled source="ID" />
            <TextInput source="Nome" />
            <NumberInput source="Quantidade" />
            <NumberInput source="Valor" />
        </SimpleForm>
    </Create>
);