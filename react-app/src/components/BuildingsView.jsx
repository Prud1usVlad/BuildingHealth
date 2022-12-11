import ReactDOM from "react-dom";
import { useTranslation, Trans } from "react-i18next";
import { Link, redirect } from "react-router-dom";
import {BootstrapTable, TableHeaderColumn} from 'react-bootstrap-table';
import React, { useState, useEffect } from 'react';
import Modal from 'react-bootstrap/Modal';
import axios from "axios";

const API_URL = "http://localhost:5254/api/";
const token = localStorage.getItem("token");
const headers = { headers: { 'Authorization': `Bearer ${token}`}};

export default function BuildingsView(props) {

    const { t, i18n } = useTranslation();

    const changeLanguage = (lng) => {
        i18n.changeLanguage(lng);
    };

    

    const selectRowProp = {
        mode: 'radio',
        clickToSelect: true,
        onSelect: props.OnRowSelect,
    };
    const dateFormatter = (cell, row) => {
        return cell.split("T")[0];
    }
 
    return(

        
            <div class="row">
                <BootstrapTable data={ props.buildings } search={ true } selectRow={ selectRowProp }> 
                    <TableHeaderColumn dataField='id' isKey><Trans i18nKey="id"/></TableHeaderColumn>
                    <TableHeaderColumn dataField='name'><Trans i18nKey="title"/></TableHeaderColumn>
                    <TableHeaderColumn dataField='adress'><Trans i18nKey="address"/></TableHeaderColumn>
                    <TableHeaderColumn dataField='workStartedDate' dataFormat={ dateFormatter }><Trans i18nKey="sDate"/></TableHeaderColumn>
                    <TableHeaderColumn dataField='handoverDate' dataFormat={ dateFormatter }><Trans i18nKey="hDate"/></TableHeaderColumn>
                    <TableHeaderColumn dataField='description'><Trans i18nKey="description"/></TableHeaderColumn>
                </BootstrapTable>
            </div>

    )
}