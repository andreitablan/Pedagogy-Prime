import React, { useContext } from "react";
import { UserContext } from "../App";
import "bootstrap/dist/css/bootstrap.min.css";
import Button from "react-bootstrap/Button";
import AppNavbar from "../components/AppNavbar";
import { Link } from "react-router-dom";
import DisplaySubjects from "../components/DisplaySubjects";

const Subjects: React.FC = () => {
  const { user } = useContext(UserContext);

  return (
    <div>
      <AppNavbar />
      
      <DisplaySubjects />
    </div>
  );
};

export default Subjects;
