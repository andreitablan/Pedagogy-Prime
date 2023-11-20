import React, { useState } from "react";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import "bootstrap/dist/css/bootstrap.min.css";
import Subjects from "./pages/Subjects";
import Login from "./pages/Login";
import Courses from "./pages/Courses";
import ProtectedRoutes from "./ProtectedRoutes";
import Informations from "./pages/Informations";
import Subject from "./pages/Subject";
import NotFound from "./pages/NotFound";

// Create a user context with initial values
export const UserContext = React.createContext({
  user: {
    loggedIn: false,
    id: "",
    email: "",
    userName: "",
    password: "",
    firstName: "",
    lastName: "",
    role: "",
  },
  setUser: (userData: any) => {},
});

function App() {
  let [user, setUser] = useState({
    loggedIn: false,
    id: "",
    email: "",
    userName: "",
    password: "",
    firstName: "",
    lastName: "",
    role: "",
  });

  const userDetails = localStorage.getItem("user");

  if(userDetails)
  {
    user = JSON.parse(userDetails);
  }

  return (
    <UserContext.Provider
      value={{
        user,
        setUser,
      }}
    >
      <Router>
        <Routes>
          <Route path="/" element={<Login />} />
          <Route path="/login" element={<Login />} />
          <Route element={<ProtectedRoutes />}>
            <Route path="/subjects">
            <Route path="" element={<Subjects />}/>
            <Route path=":id" element={<Subject />}/>
            </Route>
            <Route path="/courses" element={<Courses />} />
            <Route path="/info" element={<Informations />} />
            <Route path="*" element={<NotFound />} />
            
          </Route>
        </Routes>
      </Router>
    </UserContext.Provider>
  );
}

export default App;
