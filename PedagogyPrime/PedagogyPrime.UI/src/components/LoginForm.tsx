import React, { useState, useContext, useEffect } from "react";
import { useNavigate } from "react-router";
import { UserContext } from "../App";
import "bootstrap/dist/css/bootstrap.min.css";
import mapToRole from "../models/UserDetails";

function LoginForm() {
  const [userName, setUserName] = useState("");
  const [password, setPassword] = useState("");
  const { user, setUser } = useContext(UserContext);
  const navigate = useNavigate();

  const [loginAttempts, setLoginAttempts] = useState(0);
  const [isBlocked, setIsBlocked] = useState(false);

  useEffect(() => {
    const resetAttemptsTimer = setTimeout(() => {
      setLoginAttempts(0);
      setIsBlocked(false);
    }, 5 * 60 * 1000);

    return () => clearTimeout(resetAttemptsTimer);
  }, [loginAttempts]);

  const observeLoginAttempt = () => {
    console.log("User attempted to log in.");
  };

  const verifyLogin = (response) => {
    if (response.status === 200) {
      setLoginAttempts(0);
      return true;
    } else {
      setLoginAttempts((prevAttempts) => prevAttempts + 1);

      console.log(loginAttempts);
      if (loginAttempts + 1 > 5) {
        setIsBlocked(true);
        alert("Login blocked. Please try again later.");
      }

      alert("Wrong username or password!");
      throw new Error("Wrong username or password");
    }
  };

  const handleSubmit = (e) => {
    observeLoginAttempt();
    e.preventDefault();

    if (isBlocked) {
      alert("Login blocked. Please try again later.");
      return;
    }

    const loginUser = {
      username: userName,
      password: password,
    };

    fetch("https://localhost:7136/api/v1/Authentication/login", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(loginUser),
    })
      .then((response) => {
        if (verifyLogin(response)) {
          return response.json();
        } else {
          throw new Error("Wrong username or password");
        }
      })
      .then((data) => {
        localStorage.setItem("accessToken", data.resource.accessToken);
        const userDetails = data.resource.userDetails;
        const user = {
          loggedIn: true,
          ...userDetails,
          role: mapToRole(userDetails.role),
        };

        setUser(user);

        localStorage.setItem("user", JSON.stringify(user));

        navigate("/subjects");
      })
      .catch((error) => {
        console.error(error);
        if (error.message === "Wrong username or password") {
          setUserName("");
          setPassword("");
        }
      });
  };
  return (
    <div className="container">
      <div className="row">
        <div
          className="col-sm-6 offset-sm-3"
          style={{
            backgroundColor: "white",
            fontSize: "16px",
            padding: "45px",
            opacity: 0.9,
            borderRadius: "10px",
            border: "1px solid black",
            boxShadow: "4px 4px 5px rgba(0, 0, 0, 0.5)",
          }}
        >
          <form onSubmit={handleSubmit}>
            <div className="text-center">
              <div
                className="text-wrap fs-2"
                style={{ color: "#1B3B7B", fontWeight: "bold" }}
              >
                Login
              </div>
            </div>
            <div className="mb-3">
              <label htmlFor="username" className="form-label">
                Username
              </label>
              <input
                type="text"
                className="form-control"
                id="username"
                value={userName}
                placeholder="Enter your username"
                onChange={(e) => setUserName(e.target.value)}
              />
            </div>
            <div className="mb-3">
              <label htmlFor="password" className="form-label">
                Password
              </label>
              <input
                type="password"
                className="form-control"
                id="password"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
              />
            </div>
            <button
              type="submit"
              className="btn btn-success"
              style={{
                background: "linear-gradient(to right,  #594CF5, #6000FC)",
                color: "white",
                fontWeight: "bold",
                display: "block",
                margin: "auto",
                border: "none",
              }}
            >
              Login
            </button>
          </form>
        </div>
      </div>
    </div>
  );
}
export default LoginForm;
