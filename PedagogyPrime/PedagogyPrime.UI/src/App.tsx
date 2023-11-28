import React, { useState } from "react";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import "bootstrap/dist/css/bootstrap.min.css";
import Subjects from "./pages/Subjects";
import Login from "./pages/Login";
import Courses from "./pages/Courses";
import ProtectedRoutes from "./ProtectedRoutes";
import Informations from "./pages/Informations";
import SubjectForm from "./pages/SubjectForm";
import Subject from "./pages/Subject";
import SubjectForumCopy from "./pages/SubjectFormCopy";
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

  if (userDetails) {
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
              <Route path="" element={<Subjects />} />
              <Route path=":id" element={<Subject />} />
              <Route path="create" element={<SubjectForm />} />
              <Route path=":id/edit" element={<SubjectForm />} />
            </Route>
            <Route path="/firebase" element={<SubjectForumCopy />} />
            <Route path="/courses" element={<Courses />} />
            <Route path="/info" element={<Informations />} />
            <Route path="*" element={<Login />} />
          </Route>
        </Routes>
      </Router>
    </UserContext.Provider>
  );
}

export default App;
