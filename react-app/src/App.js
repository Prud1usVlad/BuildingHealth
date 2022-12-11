import logo from './logo.svg';
import './App.css';
import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import Login from './components/Login';
import Home from './components/Home';
import Builders from './components/Builders';
import Sensors from './components/Sensors';
import Statistics from './components/Statistics'

function App() {
  const isLogined = localStorage.getItem("token") !== null


  return (
    <BrowserRouter>
			<Routes>
          <Route path="/" element={<Home />}>
            { isLogined ? null : <Navigate to="/Login" />}
          </Route>
          <Route path="/Login" element={<Login />} />
          <Route path="/Builders" element={<Builders />} />
          <Route path="/Sensors" element={<Sensors />} />
          <Route path="/Statistics" element={<Statistics />} />
			</Routes>
		</BrowserRouter>
  );
}

export default App;
