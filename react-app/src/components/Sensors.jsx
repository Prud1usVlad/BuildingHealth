
import NavigationPanel from "./NavigationPanel"
import BuildingsView from "./BuildingsView"
import ReactDOM from "react-dom";
import { useTranslation, Trans } from "react-i18next";
import React, { useState, useCallback, useEffect } from 'react';
import axios from "axios";
import { useNavigate } from 'react-router-dom';
import Modal from 'react-bootstrap/Modal';
import CommentsFeed from "./ComentsFeed";
import { PieChart, Pie, Sector } from "recharts";

const API_URL = "http://localhost:5254/api/";
const token = localStorage.getItem("token");
const userId = localStorage.getItem("userId");
const headers = { headers: { 'Authorization': `Bearer ${token}`}};





export default function Sensors() {

    const { t, i18n } = useTranslation();
    const [selected, setSelected] = useState({});
    const [update, setUpdate] = useState(true);
    const [updateComments, setUpdateComments] = useState(true);
    const [buildings, setBuildings] = useState([]);
    const [chartEntries, setChartEntries] = useState([]);
    const [recoms, setRecoms] = useState([]);
    const [activeIndex, setActiveIndex] = useState(0);

    const renderActiveShape = (props) => {
        const RADIAN = Math.PI / 180;
        const {
          cx,
          cy,
          midAngle,
          innerRadius,
          outerRadius,
          startAngle,
          endAngle,
          fill,
          payload,
          percent,
          value
        } = props;
        const sin = Math.sin(-RADIAN * midAngle);
        const cos = Math.cos(-RADIAN * midAngle);
        const sx = cx + (outerRadius + 10) * cos;
        const sy = cy + (outerRadius + 10) * sin;
        const mx = cx + (outerRadius + 30) * cos;
        const my = cy + (outerRadius + 30) * sin;
        const ex = mx + (cos >= 0 ? 1 : -1) * 22;
        const ey = my;
        const textAnchor = cos >= 0 ? "start" : "end";
      
        return (
          <g>
            <text x={cx} y={cy} dy={8} textAnchor="middle" fill={fill}>
              {payload.label}
            </text>
            <Sector
              cx={cx}
              cy={cy}
              innerRadius={innerRadius}
              outerRadius={outerRadius}
              startAngle={startAngle}
              endAngle={endAngle}
              fill={fill}
            />
            <Sector
              cx={cx}
              cy={cy}
              startAngle={startAngle}
              endAngle={endAngle}
              innerRadius={outerRadius + 6}
              outerRadius={outerRadius + 10}
              fill={fill}
            />
            <path
              d={`M${sx},${sy}L${mx},${my}L${ex},${ey}`}
              stroke={fill}
              fill="none"
            />
            <circle cx={ex} cy={ey} r={2} fill={fill} stroke="none" />
            <text
              x={ex + (cos >= 0 ? 1 : -1) * 12}
              y={ey}
              textAnchor={textAnchor}
              fill="#333"
            >{`PV ${value}`}</text>
            <text
              x={ex + (cos >= 0 ? 1 : -1) * 12}
              y={ey}
              dy={18}
              textAnchor={textAnchor}
              fill="#999"
            >
              {`(${t("state")}: ${(percent * 100).toFixed(2)}%)`}
            </text>
          </g>
        );
    };


    const onPieEnter = useCallback(
        (_, index) => {
        setActiveIndex(index);
        },
        [setActiveIndex]
    );

    const onRowSelect = async (row, isSelected, e) => {
        setSelected(row);

        let response = await axios.get(API_URL + "Charts/Sensors/Building/" + row.id, headers);
        let response1 = await axios.get(API_URL + "Recomendations/Building/" + row.id, headers);
        setChartEntries(response.data);
        setRecoms(response1.data);
        setUpdateComments(true);
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

    return (
        <div>
            <NavigationPanel />


            <div class="container my-5">
                <div class="row">
                    <div class="row my-5"></div>
                    <div class="col-4">
                        <CommentsFeed projectId={selected.id} 
                                      update={updateComments}
                                      callUpdate={() => setUpdateComments(!updateComments)} />
                    </div>
                    <div class="col-8">
                        <div class="container">
                            <div class="row mt-5"><h2><Trans i18nKey="bProjects"/></h2></div>

                            <BuildingsView buildings={buildings} OnRowSelect={onRowSelect}/>

                            <div class="row my-5"><h2><Trans i18nKey="charts"/></h2></div>

                            <PieChart width={800} height={400}>
                                <Pie
                                    activeIndex={activeIndex}
                                    activeShape={renderActiveShape}
                                    data={chartEntries}
                                    cx={300}
                                    cy={190}
                                    innerRadius={100}
                                    outerRadius={150}
                                    fill="#198754"
                                    dataKey="firstValue"
                                    onMouseEnter={onPieEnter}
                                />
                            </PieChart>

                            <div class="row my-5"><h2><Trans i18nKey="recoms"/></h2></div>

                            {recoms.map((rec, i) =>(
                                <div key={"rec"+i} class="toast m-3 bg-success">
                                <div class="toast-header">
                                    <i class="fa-solid fa-comment"></i>
                                    <strong class="me-auto">{rec.heading}</strong>
                                    <small class="text-muted">{rec.date}</small>
                                </div>
                                <div class="toast-body text-white">
                                    {rec.body}
                                </div>
                            </div>
                            ))}
                        </div>
                    </div>
                </div>
            </div>




        </div>
    )
}