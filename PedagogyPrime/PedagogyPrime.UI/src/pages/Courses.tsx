import React from "react";
import Navbar from "../components/Navbar";
import CrudCourses from "../components/CrudCourses";

const Dashboard: React.FC = () => {
  return (
    <div>
      <Navbar />
      <CrudCourses />
    </div>
  );
};

export default Dashboard;
