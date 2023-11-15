import React from "react";
import AppNavbar from "../components/AppNavbar";
import CrudDocument from "../components/CrudDocument";

const Dashboard: React.FC = () => {
  return (
    <div>
      <AppNavbar />
      <CrudDocument />
    </div>
  );
};

export default Dashboard;
