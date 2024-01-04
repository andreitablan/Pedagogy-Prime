import React, { useState, useContext, useEffect } from "react";
import { useNavigate } from "react-router";
import { UserContext } from "../App";
import "bootstrap/dist/css/bootstrap.min.css";
import mapNumberToRole from "../models/UserDetails";

function LoginFormAI() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const { setUser } = useContext(UserContext);
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
    console.log("loginAttempts");
  };

  const verifyLogin = async (response) => {
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

  const handleSubmit = async (e) => {
    observeLoginAttempt();
    e.preventDefault();

    if (isBlocked) {
      alert("Login blocked. Please try again later.");
      return;
    }

    const loginUser = {
      username,
      password,
    };

    try {
      const response = await fetch(
        "https://localhost:7136/api/v1/Authentication/login",
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(loginUser),
        }
      );

      if (await verifyLogin(response)) {
        const data = await response.json();

        localStorage.setItem("accessToken", data.resource.accessToken);
        const userDetails = data.resource.userDetails;
        const user = {
          loggedIn: true,
          ...userDetails,
          role: mapNumberToRole(userDetails.role),
        };

        setUser(user);

        localStorage.setItem("user", JSON.stringify(user));

        navigate("/subjects");
      }
    } catch (error) {
      console.error(error);
      if (error.message === "Wrong username or password") {
        setUsername("");
        setPassword("");
      }
    }
  };

  return (
    <div className="container">
      <div className="row">
        <div className="col-sm-6 offset-sm-3">
          <form onSubmit={handleSubmit}>
            <h2 className="text-center mb-4" style={{ color: "#1B3B7B" }}>
              Login
            </h2>
            <div className="mb-3">
              <input
                type="text"
                className="form-control"
                placeholder="Username"
                value={username}
                onChange={(e) => setUsername(e.target.value)}
              />
            </div>
            <div className="mb-3">
              <input
                type="password"
                className="form-control"
                placeholder="Password"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
              />
            </div>
            <button type="submit" className="btn btn-primary w-100">
              Login
            </button>
          </form>
        </div>
      </div>
    </div>
  );
}

export default LoginFormAI;
