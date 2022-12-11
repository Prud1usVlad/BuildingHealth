
import NavigationPanel from "./NavigationPanel"
import BuildingsView from "./BuildingsView"
import ReactDOM from "react-dom";
import { useTranslation, Trans } from "react-i18next";
import React, { useState, useEffect } from 'react';
import axios from "axios";
import { useNavigate } from 'react-router-dom';
import Modal from 'react-bootstrap/Modal';
import { LineChart, Line, XAxis, YAxis, CartesianGrid, Tooltip, Legend, ResponsiveContainer } from 'recharts';

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

export default function Statistics() {

    const { t, i18n } = useTranslation();
    const navigate = useNavigate();
    const [selected, setSelected] = useState({});
    const [show, setShow] = useState(false);
    const [update, setUpdate] = useState(true);
    const [buildings, setBuildings] = useState([]);
    const [chartEntries, setChartEntries] = useState([]);

    const onRowSelect = async (row, isSelected, e) => {
        setSelected(row);

        let response = await axios.get(API_URL + "Charts/Building/" + row.id, headers);
        setChartEntries(response.data);
        console.log(response.data);
    }

    useEffect(() => {
        async function fetchData() {
            let response = await axios.get(API_URL + "BuildingProjects", headers);

            setBuildings(response.data);
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

    return (
        <div>
            <NavigationPanel />

            <div class="container my-5">
                <div class="row my-5"></div>
                
                <div class="row mt-5"><h2><Trans i18nKey="charts"/></h2></div>

                <div class="row mt-5">
                <LineChart
                    width={1200}
                    height={500}
                    data={chartEntries}>
                    <CartesianGrid strokeDasharray="3 3" />
                    <XAxis dataKey="name" />
                    <YAxis />
                    <Tooltip />
                    <Legend />
                    <Line type="monotone" 
                        dataKey="firstValue" 
                        name={t("genState")} 
                        stroke="#8884d8" 
                        activeDot={{ r: 8 }} />
                    <Line type="monotone" 
                        dataKey="secondValue" 
                        name={t("constState")}
                        stroke="#82ca9d" 
                        activeDot={{ r: 8 }}/>
                    <Line type="monotone" 
                        dataKey="thirdValue"
                        name={t("gndState")} 
                        activeDot={{ r: 8 }}/>
                </LineChart>
                </div>

                <div class="row mt-5"><h2><Trans i18nKey="bProjects"/></h2></div>

                <BuildingsView buildings={buildings} OnRowSelect={onRowSelect} />
            </div>
        </div>
    )
}