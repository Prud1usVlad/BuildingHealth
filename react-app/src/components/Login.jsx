import ReactDOM from "react-dom";
import { useTranslation, Trans } from "react-i18next";
import { Link, redirect } from "react-router-dom";
import React, { useState, useEffect } from 'react';
import axios from "axios";
import { useNavigate } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faLanguage } from '@fortawesome/free-solid-svg-icons'

const API_URL = "http://localhost:5254/api/UserManager/Login";

export default function Login() {

    const { t, i18n } = useTranslation();
    const [ password, setPassword ] = useState("");
    const [ email, setEmail ] = useState("");
    const navigate = useNavigate();

    const changeLanguage = (lng) => {
        i18n.changeLanguage(lng);
    };

    


    const onSubmit = async () => {
        try {
            let response = await axios.post(API_URL, { Password:password, Email:email });
            console.log(response);

            localStorage.setItem("token", response.data.token);
            localStorage.setItem("role", response.data.role);
            localStorage.setItem("userId", response.data.id);

            navigate("/");
        }
        catch {
            alert(t("loginError"));
        }

        setPassword("");
    }
  
    return(
        <div class="container p-5">
            <div class="row justify-content-end my-5">
                <div class="btn-group col-1">
                <button type="button" class="btn btn-dark rounded" data-bs-toggle="dropdown" aria-expanded="false">
                    <FontAwesomeIcon icon={faLanguage} />
                </button>
                <ul class="dropdown-menu">
                    <li><a class="dropdown-item" onClick={ () => changeLanguage("en") }>English</a></li>
                    <li><a class="dropdown-item" onClick={ () => changeLanguage("ua") }>Українська</a></li>
                </ul>
                </div>
            </div>
            <div class="row justify-content-center my-5">
                <form class="col-4 bg-light shadow rounded">
                    <h3 className="p-3"><Trans i18nKey="loginHeader"></Trans></h3>
                    <div class="mb-3">
                        <label for="inputEmail" class="form-label"><Trans i18nKey="email"></Trans></label>
                        <input type="email" class="form-control" id="inputEmail" aria-describedby="emailHelp"
                            value={email}
                            onChange={e => setEmail(e.target.value)} />
                    </div>
                    <div class="mb-3">
                        <label for="inputPassword" class="form-label"><Trans i18nKey="password"></Trans></label>
                        <input type="password" class="form-control" id="inputPassword"
                            value={password}
                            onChange={e => setPassword(e.target.value)} />
                    </div>
                </form>
                
            </div>
            <div class="row justify-content-center my-5">
                <div class="col-4">
                    <button class="btn btn-dark" onClick={ () => onSubmit() }><Trans i18nKey="submit"></Trans></button>
                </div>
            </div>
        </div>
    )
}