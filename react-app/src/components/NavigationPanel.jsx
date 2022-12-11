import { useNavigate } from 'react-router-dom';
import { useTranslation, Trans } from "react-i18next";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faLanguage } from '@fortawesome/free-solid-svg-icons'

export default function NavigationPanel() {
    const { t, i18n } = useTranslation();
    const navigate = useNavigate();

    const changeLanguage = (lng) => {
        i18n.changeLanguage(lng);
    };


    const onLogOut = () => {
        localStorage.setItem("token", null);
        localStorage.setItem("role", null);
        localStorage.setItem("userId", null);

        navigate("/Login");
    }

    return (
        <nav class="navbar navbar-dark bg-dark fixed-top">
        <div class="container-fluid">
            <a class="navbar-brand" href="#">Building Health</a>
            <button class="navbar-toggler" type="button" data-bs-toggle="offcanvas" data-bs-target="#offcanvasDarkNavbar" aria-controls="offcanvasDarkNavbar">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="offcanvas offcanvas-end text-bg-dark" tabindex="-1" id="offcanvasDarkNavbar" aria-labelledby="offcanvasDarkNavbarLabel">
            <div class="offcanvas-header">
                <h5 class="offcanvas-title" id="offcanvasDarkNavbarLabel"><Trans i18nKey="menue"></Trans></h5>
                <button type="button" class="btn-close btn-close-white" data-bs-dismiss="offcanvas" aria-label="Close"></button>
            </div>
            <div class="offcanvas-body">
                <div class="row">
                    <div class="col"><Trans i18nKey="lang" /></div>
                    <div class="col-2">
                        <div class="btn-group col-1 dropstart">
                        <button type="button" class="btn btn-dark rounded" data-bs-toggle="dropdown" aria-expanded="false">
                            <FontAwesomeIcon icon={faLanguage} />
                        </button>
                        <ul class="dropdown-menu">
                            <li><a class="dropdown-item" onClick={ () => changeLanguage("en") }>English</a></li>
                            <li><a class="dropdown-item" onClick={ () => changeLanguage("ua") }>Українська</a></li>
                        </ul>
                        </div>
                    </div>
                </div>

                <hr />

                <ul class="navbar-nav justify-content-end flex-grow-1 pe-3">
                <li class="nav-item">
                    <a class="nav-link" aria-current="page" onClick={() => navigate("/")}>
                        <Trans i18nKey="home"/>
                    </a>
                </li>
                    <a class="nav-link" onClick={() => navigate("/Statistics")}>
                        <Trans i18nKey="stat"/>
                    </a>
                <li class="nav-item">
                    <a class="nav-link" onClick={() => navigate("/Sensors")}>
                        <Trans i18nKey="sens"/>
                    </a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" onClick={() => navigate("/Builders")}>
                        <Trans i18nKey="builders"/>
                    </a>
                </li>
                </ul>

                <hr />

                <div class="row justify-content-start my-5">
                    <div class="col">
                        <button class="btn btn-success" onClick={ () => onLogOut() }>
                            <Trans i18nKey="logout" />
                        </button>
                    </div>
                </div>

            </div>
            </div>
        </div>
        </nav>
    )

} 