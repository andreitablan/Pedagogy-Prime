import React from "react";
import Navbar from "../components/Navbar";
import CrudDocument from "../components/CrudDocument";

const Dashboard: React.FC = () => {
  return (
    <div>
      <Navbar />
      <CrudDocument />
    </div>
  );
};

export default Dashboard;
