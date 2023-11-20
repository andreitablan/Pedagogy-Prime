import React from "react";
import AppNavbar from "../components/AppNavbar";
import CrudCourse from "../components/CrudCourses";

const Dashboard: React.FC = () => {
  return (
    <div>
      <AppNavbar />
      <CrudCourse />
    </div>
  );
};

export default Dashboard;
