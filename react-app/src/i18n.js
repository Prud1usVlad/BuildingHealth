import i18n from "i18next";
import LanguageDetector from "i18next-browser-languagedetector";
import { initReactI18next } from "react-i18next";

i18n
  .use(LanguageDetector)
  .use(initReactI18next)
  .init({
    // we init with resources
    resources: {
      en: {
        translations: {
          email:"Email",
          password: "Password",
          submit: "Submit",
          loginHeader: "System authorization",
          menue: "Menue",
          name: "Name",
          lastName:"Last name",
          lang:"Interface language",
          actions: "Actions",
          delete: "Delete",
          add: "Add",
          details: "Details",
          phone: "Phone number",
          save: "Save changes",
          back: "Back",
          basicData: "Basic data",
          charts: "Charts",
          search: "Search",
          onDelete: "Do you rely want to delete",
          loginError: "An error occurred in process of authorization, please check your credentials.",
          accessDenied: "Resource access denied to this account.",
          adminPanelHeader: "Adminpanel",
          admins:"Admins",
          description:"Description",
          title:"Title",
          value: "Value: ",
          home:"Home page",
          stat:"Statistics page",
          sens:"Sensors page",
          builders:"Builders page",
          logout:"Log out",
          id: "Identifier",
          address: "Address",
          sDate:"Work started date",
          hDate: "Handover Date",
          bProjects: "Building projects",
          bProject: "Building project",
          noItemSelected: "Please select item in the list",
          genState: "General state",
          constState: "Constructions state",
          gndState: "Ground state",
          send: "Send",
          comments:"Comments",
          comment:"Comment",
          state:"State",
          recoms:"Recomendations"
        }
      },
      ua: {
        translations: {
            email:"Електронна пошта",
            password: "Пароль",
            submit: "Відправити",
            loginHeader: "Авторизація в системі",
            menue: "Меню",
            name: "Ім'я",
            lastName:"Прізвище",
            lang:"Мова інтерфесу",
            actions: "Дії",
            delete: "Видалити",
            add:"Додати",
            details: "Детальніше",
            phone: "Номер телефону",
            save: "зберегти зміни",
            back: "Назад",
            basicData: "Базова інформація",
            charts: "Графіки",
            search: "Шукати",
            onDelete: "Ви дійсно хочете видалити?",
            chartWarning: "Сталася помилка під час спроби отримати дані графіку для поточного користувача. Перевірки правильність введених даних або спробуйте ще раз пізніше.",
            loginError: "Сталася помилка під час авторизації, перевірте введені дані.",
            accessDenied: "Для цього аккаунту доступ до ресурсу заборонений.",
            description:"Описання",
            title:"Назва",
            value: "Значення: ",
            home:"Домашня сторінка",
            stat:"Сторінка статистики",
            sens:"Сторінка сенсорів",
            builders:"Сторінка будівників",
            logout:"Вийти з аккаунту",
            id: "Ідентифікатор",
            address: "Адресса",
            sDate:"Дата початку робіт",
            hDate: "Дата здачі",
            bProjects: "Проекти будівлі",
            bProject: "Проект будівлі",
            noItemSelected: "Будь ласка оберіть елемент зі списку",
            genState: "Загальний стан",
            constState: "Стан  конструкцій",
            gndState: "Стан грунту",
            send: "Надіслати",
            comments:"Коментарі",
            comment:"Коментар",
            state:"Стан",
            recoms:"Рекомендації"
        }
      }
    },
    fallbackLng: "en",
    debug: true,

    // have a common namespace used around the full app
    ns: ["translations"],
    defaultNS: "translations",

    keySeparator: false, // we use content as keys

    interpolation: {
      escapeValue: false
    }
  });

export default i18n;