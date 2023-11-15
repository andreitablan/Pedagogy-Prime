import React, { useContext } from "react";
import { UserContext } from "../App";
import "bootstrap/dist/css/bootstrap.min.css";
import AppNavbar from "../components/AppNavbar";

const Subjects: React.FC = () => {
  const { user } = useContext(UserContext);

  return (
    <div>
      <AppNavbar />

      {user && user.loggedIn ? (
        <div>
          <p>Welcome, {user.userName}!</p>
          <p>Email: {user.email}</p>
          <p>First Name: {user.firstName}</p>
        </div>
      ) : (
        <p>User not logged in.</p>
      )}
    </div>
  );
};

export default Subjects;
