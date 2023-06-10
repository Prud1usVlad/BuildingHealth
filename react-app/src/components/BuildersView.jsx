import { useTranslation, Trans } from "react-i18next";
import {BootstrapTable, TableHeaderColumn} from 'react-bootstrap-table';
import React, { useState, useEffect } from 'react';

const API_URL = "http://localhost:5254/api/";
const token = localStorage.getItem("token");
const headers = { headers: { 'Authorization': `Bearer ${token}`}};

export default function BuildersView(props) {

    const { t, i18n } = useTranslation();

    const changeLanguage = (lng) => {
        i18n.changeLanguage(lng);
    };

    

    const selectRowProp = {
        mode: 'radio',
        clickToSelect: true,
        onSelect: props.OnRowSelect,
    };
    
    const firstNameFormatter = (cell, row) => {
        return row.idNavigation.firstName;
    }
 
    const secondNameFormatter = (cell, row) => {
        return row.idNavigation.secondName;
    }

    const emailFormatter = (cell, row) => {
        return row.idNavigation.email;
    }

    const phoneFormatter = (cell, row) => {
        return row.idNavigation.phone;
    }


    return(

        
            <div class="row">
                <BootstrapTable data={ props.buildings } search={ true } selectRow={ selectRowProp }> 
                    <TableHeaderColumn dataField='id' isKey><Trans i18nKey="id"/></TableHeaderColumn>
                    <TableHeaderColumn dataField='idNavigation.firstName' dataFormat={ firstNameFormatter }><Trans i18nKey="name"/></TableHeaderColumn>
                    <TableHeaderColumn dataField='idNavigation.secondName' dataFormat={ secondNameFormatter }><Trans i18nKey="lastName"/></TableHeaderColumn>
                    <TableHeaderColumn dataField='idNavigation.email' dataFormat={ emailFormatter }><Trans i18nKey="email"/></TableHeaderColumn>
                    <TableHeaderColumn dataField='idNavigation.phone' dataFormat={ phoneFormatter }><Trans i18nKey="phone"/></TableHeaderColumn>
                </BootstrapTable>
            </div>

    )
}