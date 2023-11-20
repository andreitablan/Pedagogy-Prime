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
      <Link to="/subjects/create">
        <Button
          style={{
            background:
              "linear-gradient(to bottom right, #594CF5, #6000FC, #5107F5, #2A1AE1)",
            color: "white",
            marginBottom: "16px",
            marginLeft: "16px",
            boxShadow: "4px 4px 5px rgba(0, 0, 0, 0.5)",
          }}
          className="btn btn-primary"
        >
          New Subject
        </Button>
      </Link>
      <DisplaySubjects />
    </div>
  );
};

export default Subjects;
