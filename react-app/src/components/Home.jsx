import NavigationPanel from "./NavigationPanel"
import BuildingsView from "./BuildingsView"
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
const emptyBuilding = {
    id:0, 
    name:"", 
    adress:"", 
    handoverDate:"", 
    workStartedDate:"", 
    description:"", 
    architectId:userId
};

export default function Home() {

    const { t, i18n } = useTranslation();
    const navigate = useNavigate();
    const [selected, setSelected] = useState({});
    const [show, setShow] = useState(false);
    const [update, setUpdate] = useState(true);
    const [buildings, setBuildings] = useState([]);

    const onRowSelect = (row, isSelected, e) => {
        setSelected(row);
    }

    useEffect(() => {
        async function fetchData() {
            let responce = await axios.get(API_URL + "BuildingProjects", headers);
            console.log(responce);
            setBuildings(responce.data);
            setUpdate(false);
        }

        if (update === true)
            fetchData();
        
    }, [update]);

    const onDelete = async () => {
        if (selected.id !== 0) {
            if (window.confirm(t("onDelete"))) {
                await axios.delete(API_URL + "BuildingProjects/" + selected.id, headers)
            }
        }

        setUpdate(true);
        setShow(false);  
    }

    const onSave = async () => {
        if (selected.id === 0) {
            console.log(selected);
            console.log(JSON.stringify(selected));
            await axios.post(API_URL + "BuildingProjects", selected, headers)
            console.log(JSON.stringify(selected));
        }
        else {
            console.log(selected);
            console.log(JSON.stringify(selected));
            await axios.put(API_URL + "BuildingProjects/" + selected.id, selected, headers)
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
                <Modal.Title><Trans i18nKey="bProject"/></Modal.Title>
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
                                value={selected.name} 
                                onChange={e => updateSelected("name", e.target.value)}/>
                        </div>
                        </div>

                        <div class="row justify-content-md-center m-4">
                        <div class="col-8 align-self-center">
                            <label class="form-label"><Trans i18nKey="address"/></label>
                            <input type="email" class="form-control" 
                                value={selected.adress}
                                onChange={e => updateSelected("adress", e.target.value)}/>
                        </div>
                        </div>

                        <div class="row justify-content-md-center m-4">
                        <div class="col-8 align-self-center">
                            <label class="form-label"><Trans i18nKey="sDate"/></label>
                            <input type="date" class="form-control" 
                                value={selected.workStartedDate}
                                onChange={e => { updateSelected("workStartedDate", e.target.value);
                                console.log(e.target.value) }}/>
                        </div>
                        </div>

                        <div class="row justify-content-md-center m-4">
                        <div class="col-8 align-self-center">
                            <label class="form-label"><Trans i18nKey="hDate"/></label>
                            <input type="date" class="form-control" 
                                value={selected.handoverDate}
                                onChange={e => updateSelected("handoverDate", e.target.value)}/>
                        </div>
                        </div>

                        <div class="row justify-content-md-center m-4">
                        <div class="col-8 align-self-center">
                            <label class="form-label"><Trans i18nKey="description"/></label>
                            <textarea class="form-control" rows={3} 
                                value={selected.description}
                                onChange={e => updateSelected("description", e.target.value)}/>
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
                <div class="row mt-5"><h2><Trans i18nKey="bProjects"/></h2></div>

                <BuildingsView buildings={buildings} OnRowSelect={onRowSelect} />

                <div class="row my-5">
                    <div class="col-1">
                        <button class="btn btn-success" onClick={ () => {
                            let newObj = {};
                            Object.assign(newObj, emptyBuilding);
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

                                console.log(selected.handoverDate?.split("T")[0]);

                                let newObj = {};
                                Object.assign(newObj, selected);
                                newObj.handoverDate = selected.handoverDate?.split("T")[0];
                                newObj.workStartedDate = selected.workStartedDate?.split("T")[0]

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