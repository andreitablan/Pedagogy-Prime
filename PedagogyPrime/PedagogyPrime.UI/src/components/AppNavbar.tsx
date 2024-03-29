import React from "react";
import { Link } from "react-router-dom";
import LoginIcon from "@mui/icons-material/Login";
import MenuBookIcon from "@mui/icons-material/MenuBook";
import Navbar from "react-bootstrap/Navbar";
import Nav from "react-bootstrap/Nav";
import InfoIcon from "@mui/icons-material/Info";
import SubjectIcon from "@mui/icons-material/Subject";
import GroupIcon from '@mui/icons-material/Group';

const AppNavbar: React.FC = () => {
  const logout = () => {
    localStorage.removeItem("accessToken");
    localStorage.removeItem("user");
  };
  return (
    <Navbar
      bg="dark"
      expand="lg"
      variant="dark"
      style={{
        background:
          "linear-gradient(to bottom right, #07DADA, #60CAEB, #594CF5, #6000FC)",
        marginBottom: "10px",
        width: "100%",
      }}
    >
      <div className="container">
        <Link
          className="navbar-brand"
          to="/subjects"
          style={{
            fontWeight: "bold",
            fontSize: "25px",
          }}
        >
          Pedagogy Prime
        </Link>
        <Navbar.Toggle aria-controls="navbarNav" />
        <Navbar.Collapse
          id="navbarNav"
          style={{
            flexGrow: "0",
            color: "white",
          }}
        >
          <Nav className="ml-auto">
            <Link
              className="nav-link"
              to="/subjects"
              title="Subjects"
              style={{ color: "white" }}
            >
              <SubjectIcon />
            </Link>
            <Link
              className="nav-link"
              to="/users"
              title="Users"
              style={{ color: "white" }}
            >
              <GroupIcon />
            </Link>
            <Link
              className="nav-link"
              to="/info"
              title="Informations"
              style={{ color: "white" }}
            >
              <InfoIcon />
            </Link>
            <Link
              className="nav-link"
              to="/login"
              title="Logout"
              style={{ color: "white" }}
              onClick={logout}
            >
              <LoginIcon />
            </Link>
          </Nav>
        </Navbar.Collapse>
      </div>
    </Navbar>
  );
};

export default AppNavbar;
