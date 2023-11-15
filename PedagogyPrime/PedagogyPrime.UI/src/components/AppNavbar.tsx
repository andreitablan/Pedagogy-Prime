import React from "react";
import { Link } from "react-router-dom";
import HomeIcon from "@mui/icons-material/Home";
import DashboardIcon from "@mui/icons-material/Dashboard";
import LoginIcon from "@mui/icons-material/Login";
import MenuBookIcon from "@mui/icons-material/MenuBook";
import Navbar from "react-bootstrap/Navbar";
import Nav from "react-bootstrap/Nav";
import InfoIcon from "@mui/icons-material/Info";

const AppNavbar: React.FC = () => {
  return (
    <Navbar
      bg="dark"
      expand="lg"
      variant="dark"
      style={{
        background:
          "linear-gradient(to bottom right, #07DADA, #60CAEB, #594CF5, #6000FC)",
        marginBottom: "10px",
        border: "1px solid black",
      }}
    >
      <div className="container">
        <Link className="navbar-brand" to="/home">
          Pedagogy Prime
        </Link>
        <Navbar.Toggle aria-controls="navbarNav" />
        <Navbar.Collapse id="navbarNav">
          <Nav className="ml-auto">
            <Link className="nav-link" to="/home" title="Home">
              <HomeIcon />
            </Link>
            <Link className="nav-link" to="/dashboard" title="Dashboard">
              <DashboardIcon />
            </Link>
            <Link className="nav-link" to="/courses" title="Courses">
              <MenuBookIcon />
            </Link>
            <Link className="nav-link" to="/info" title="Informations">
              <InfoIcon />
            </Link>
            <Link className="nav-link" to="/login" title="Logout">
              <LoginIcon />
            </Link>
          </Nav>
        </Navbar.Collapse>
      </div>
    </Navbar>
  );
};

export default AppNavbar;
