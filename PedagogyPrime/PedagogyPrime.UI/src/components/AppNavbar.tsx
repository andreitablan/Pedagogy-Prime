import React from "react";
import { Link } from "react-router-dom";
import LoginIcon from "@mui/icons-material/Login";
import MenuBookIcon from "@mui/icons-material/MenuBook";
import Navbar from "react-bootstrap/Navbar";
import Nav from "react-bootstrap/Nav";
import InfoIcon from "@mui/icons-material/Info";
import SubjectIcon from "@mui/icons-material/Subject";
import DocumentScannerIcon from "@mui/icons-material/DocumentScanner";

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
              to="/firebase"
              title="Firebase"
              style={{ color: "white" }}
            >
              <DocumentScannerIcon />
            </Link>
            <Link
              className="nav-link"
              to="/courses"
              title="Courses"
              style={{ color: "white" }}
            >
              <MenuBookIcon />
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
