import ReactDOM from "react-dom";
import { useTranslation, Trans } from "react-i18next";
import { Link, redirect } from "react-router-dom";
import {BootstrapTable, TableHeaderColumn} from 'react-bootstrap-table';
import React, { useState, useEffect } from 'react';
import Modal from 'react-bootstrap/Modal';
import axios from "axios";


export default function CommentsList(props) {
    
    return (
        <div>
            {props.comments.map( comment => (
            <div key={comment.id} class="toast m-1">
                <div class="toast-header">
                    <i class="fa-solid fa-comment"></i>
                    <strong class="me-auto">{comment.user.firstName + " " + comment.user.secondName}</strong>
                    <small class="text-muted">{comment.date}</small>
                </div>
                <div class="toast-body">
                    {comment.text}
                </div>
            </div>
            ))}
        </div>
    )
}