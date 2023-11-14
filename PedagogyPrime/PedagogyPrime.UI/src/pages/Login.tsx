import React from "react";
import LoginForm from "../components/LoginForm";
import "bootstrap/dist/css/bootstrap.min.css";

const Login: React.FC = () => {
  return (
    <div
      style={{
        background:
          "linear-gradient(to bottom right, #07DADA,#60CAEB,#594CF5,#6000FC,#5107F5, #2A1AE1)",
        backgroundRepeat: "no-repeat",
        backgroundSize: "cover",
        backgroundPosition: "center",
        opacity: 0.9,
        minHeight: "100vh",
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
      }}
    >
      <LoginForm />
    </div>
  );
};

export default Login;
