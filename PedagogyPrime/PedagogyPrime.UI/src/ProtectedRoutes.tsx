import React from "react";
import { useContext } from "react";
import { useLocation, Navigate, Outlet } from "react-router-dom";
import { UserContext } from "./App";

const ProtectedRoutes = () => {
  const location = useLocation();
  const { user } = useContext(UserContext);

  return user.loggedIn ? (
    <Outlet />
  ) : (
    <Navigate to="/login" replace state={{ from: location }} />
  );
};

export default ProtectedRoutes;
