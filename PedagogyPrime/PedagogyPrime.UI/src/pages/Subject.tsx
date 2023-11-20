import React, { useContext } from "react";
import { UserContext } from "../App";
import "bootstrap/dist/css/bootstrap.min.css";
import AppNavbar from "../components/AppNavbar";
import SubjectDetails from "../components/SubjectDetails";
import { useParams } from "react-router-dom";

const Subject: React.FC = () => {
  const { id } = useParams()

  return (
    <div style={{
      display: "flex",
      flexDirection: "column",
      justifyContent: "center",
      alignItems: "center"
    }}>
      <AppNavbar/>
      <SubjectDetails id={id}/>
    </div>
  );
};

export default Subject;
