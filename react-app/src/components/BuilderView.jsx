import NavigationPanel from "./NavigationPanel"
import BuildersView from "./BuildersView";
import ReactDOM from "react-dom";
import { useTranslation, Trans } from "react-i18next";
import React, { useState, useEffect } from 'react';
import axios from "axios";
import { useNavigate } from 'react-router-dom';
import Modal from 'react-bootstrap/Modal';

const API_URL = "http://localhost:5254/api/";
const token = localStorage.getItem("token");
const userId = localStorage.getItem("userId");
const headers = { headers: { 'Authorization': `Bearer ${token}`}};
const emptyBuilder = {
id: 0,
phone: "",
password: "",
architectId: userId,
idNavigation: {
  id: 0,
  firstName: "",
  secondName: "",
  email: "",
  phone: "",
  role: "Builder",
}
};

export default function BuilderView() {

    const { t, i18n } = useTranslation();
    const navigate = useNavigate();
    const [selected, setSelected] = useState({});
    const [show, setShow] = useState(false);
    const [update, setUpdate] = useState(true);
    const [builders, setBuilders] = useState([]);

    const onRowSelect = (row, isSelected, e) => {
        setSelected(row);
    }

    useEffect(() => {
        async function fetchData() {
            let responce = await axios.get(API_URL + "Builders", headers);
            console.log(responce);
            setBuilders(responce.data);
            setUpdate(false);
        }

        if (update === true)
            fetchData();
        
    }, [update]);

    const onDelete = async () => {
        if (selected.id !== 0) {
            if (window.confirm(t("onDelete"))) {
                await axios.delete(API_URL + "Builders/" + selected.id, headers)
            }
        }

        setUpdate(true);
        setShow(false);  
    }

    const onSave = async () => {
        if (selected.id === 0) {
            console.log(JSON.stringify(selected));
            selected.architectId = userId;
            console.log(selected);
            await axios.post(API_URL + "Builders", selected, headers)
            console.log(JSON.stringify(selected));
        }
        else {
            console.log(selected);
            console.log(JSON.stringify(selected));
            await axios.put(API_URL + "Builders/" + selected.id, selected, headers)
            console.log(JSON.stringify(selected));
        }

        setUpdate(true);
        setShow(false);   
    }

    const updateSelected = (field, value) => {
        let newObj = {};
        Object.assign(newObj, selected);

        newObj[field] = value;

        setSelected(newObj);
    }

    return (
        <div>
            <NavigationPanel />


            <Modal className="modal-lg" show={show} scrollable={true} onHide={() => setShow(false)}>
                <Modal.Header closeButton>
                <Modal.Title><Trans i18nKey="builders"/></Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <div class="container">
                        <div class="row justify-content-md-center m-4">
                        <div class="col align-self-center">
                            <h2><Trans i18nKey="basicData"/></h2>
                        </div>
                        </div>

                        <div class="row justify-content-md-center m-4">
                        <div class="col-8 align-self-center">
                            <label class="form-label"><Trans i18nKey="name"/></label>
                            <input class="form-control" 
                                value={selected.idNavigation.firstName} 
                                onChange={e => updateSelected("idNavigation.firstName", e.target.value)}/>
                        </div>
                        </div>

                        <div class="row justify-content-md-center m-4">
                        <div class="col-8 align-self-center">
                            <label class="form-label"><Trans i18nKey="lastname"/></label>
                            <input class="form-control" 
                                value={selected.idNavigation.secondName} 
                                onChange={e => updateSelected("idNavigation.secondName", e.target.value)}/>
                        </div>
                        </div>

                        <div class="row justify-content-md-center m-4">
                        <div class="col-8 align-self-center">
                            <label class="form-label"><Trans i18nKey="phone"/></label>
                            <input type="phone" class="form-control" 
                                value={selected.idNavigation.phone}
                                onChange={e => updateSelected("idNavigation.phone", e.target.value)}/>
                        </div>
                        </div>

                        <div class="row justify-content-md-center m-4">
                        <div class="col-8 align-self-center">
                            <label class="form-label"><Trans i18nKey="email"/></label>
                            <input type="email" class="form-control" 
                                value={selected.idNavigation.email}
                                onChange={e => { updateSelected("idNavigation.email", e.target.value);
                                console.log(e.target.value) }}/>
                        </div>
                        </div>               
                    </div>
                </Modal.Body>
                <Modal.Footer>
                    <button type="button" class="btn btn-dark" onClick={() => { setShow(false); }}>
                        <Trans i18nKey="back"/>
                    </button>
                    <button variant="primary" type="button" class="btn btn-success" onClick={() => { onSave(); }}>
                        <Trans i18nKey="save"/> 
                    </button>
                    <button variant="primary" type="button" class="btn btn-danger" onClick={() => { onDelete(); }}>
                        <Trans i18nKey="delete"/> 
                    </button>
                </Modal.Footer>
            </Modal>



            <div class="container my-5">
                <div class="row my-5"></div>
                <div class="row mt-5"><h2><Trans i18nKey="builders"/></h2></div>

                <BuildersView buildings={builders} OnRowSelect={onRowSelect} />

                <div class="row my-5">
                    <div class="col-1">
                        <button class="btn btn-success" onClick={ () => {
                            let newObj = {};
                            Object.assign(newObj, emptyBuilder);
                            setSelected(newObj);
                            setShow(true);
                        } }>
                            <Trans i18nKey="add" />
                        </button>
                    </div>
                    <div class="col-1">
                        <button class="btn btn-success" onClick={ () => {
                            if (selected.id === 0 || selected.id === undefined) {
                                alert(t("noItemSelected"))
                            }
                            else {

                                console.log(selected.idNavigation);

                                let newObj = {};
                                Object.assign(newObj, selected);


                                setSelected(newObj);
                                setShow(true);
                            }
                        } }>
                            <Trans i18nKey="details" />
                        </button>
                    </div>
                </div>
            </div>
        </div>
    )
}