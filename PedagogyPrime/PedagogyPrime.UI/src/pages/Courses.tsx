import React from "react";
import AppNavbar from "../components/AppNavbar";
import CrudCourses from "../components/CrudCourses";

const Dashboard: React.FC = () => {
  return (
    <div>
      <AppNavbar />
      <CrudCourses />
    </div>
  );
};

export default Dashboard;
