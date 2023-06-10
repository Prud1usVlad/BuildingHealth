import ReactDOM from "react-dom";
import { useTranslation, Trans } from "react-i18next";
import { Link, redirect } from "react-router-dom";
import {BootstrapTable, TableHeaderColumn} from 'react-bootstrap-table';
import React, { useState, useEffect } from 'react';
import Modal from 'react-bootstrap/Modal';
import axios from "axios";
import CommentsList from "./CommentsList";

const API_URL = "http://localhost:5254/api/";
const token = localStorage.getItem("token");
const userId = localStorage.getItem("userId");
const headers = { headers: { 'Authorization': `Bearer ${token}`}};

export default function CommentsFeed(props) {

    const { t, i18n } = useTranslation();
    const [ comments, setComments ] = useState([]);
    const [ comment, setComment ] = useState("");

    useEffect(() => {
        async function fetchData() {
            let responce = await axios.get(API_URL + "Comments/Project/" + props.projectId, headers);
            setComments(responce.data);
            console.log("+++");
            console.log(responce.data);
        }

        if (props.projectId !== undefined)
            fetchData();
    }, [props.update, props.projectId])

    const onSend = async () => {
        let commentObj = {
            id:0,
            buildingProjectId:props.projectId,
            userId:userId,
            date: new Date(),
            text: comment,
        }

        await axios.post(API_URL + "Comments", commentObj, headers);
        setComment("");
        props.callUpdate();
    }

    return (

        <div class="container">
            <div class="row align-items-center">
                <h3 class="mb-5"><Trans i18nKey="comments"/></h3>
                <div class="container comments-block">
                    <CommentsList comments={comments}/>
                </div>
            </div>
            <div class="row bg-light" fixed="true">
                <div class="col-5">
                    <textarea class="form-control" 
                                rows={3}
                                placeholder={t("comment") + "..."} 
                                value={comment}
                                onChange={e => setComment(e.target.value)}/>

                </div>
                <div class="col-3 align-self-center">
                    <button type="button" class="btn btn-success" onClick={() => { onSend(); }}>
                        <Trans i18nKey="send"/> 
                    </button>
                </div>
            </div>
        </div>
    )
}