import React from "react";
import { Link } from "react-router-dom";
import HomeIcon from "@mui/icons-material/Home";
import DashboardIcon from "@mui/icons-material/Dashboard";
import LoginIcon from "@mui/icons-material/Login";
import MenuBookIcon from "@mui/icons-material/MenuBook";

const Navbar: React.FC = () => {
  return (
    <nav
      className="navbar navbar-expand-lg navbar-dark bg-dark"
      style={{ marginBottom: "10px" }}
    >
      <div className="container">
        <Link className="navbar-brand" to="/">
          Pedagogy Prime
        </Link>
        <button
          className="navbar-toggler"
          type="button"
          data-toggle="collapse"
          data-target="#navbarNav"
          aria-controls="navbarNav"
          aria-expanded="false"
          aria-label="Toggle navigation"
        >
          <span className="navbar-toggler-icon"></span>
        </button>
        <div className="collapse navbar-collapse" id="navbarNav">
          <ul className="navbar-nav">
            <li className="nav-item">
              <Link className="nav-link" to="/home" title="Home">
                <HomeIcon />
              </Link>
            </li>
            <li className="nav-item">
              <Link className="nav-link" to="/dashboard" title="Dashboard">
                <DashboardIcon />
              </Link>
            </li>
            <li className="nav-item">
              <Link className="nav-link" to="/login" title="Logout">
                <LoginIcon />
              </Link>
            </li>
            <li className="nav-item">
              <Link className="nav-link" to="/courses" title="Courses">
                <MenuBookIcon />
              </Link>
            </li>
          </ul>
        </div>
      </div>
    </nav>
  );
};

export default Navbar;
