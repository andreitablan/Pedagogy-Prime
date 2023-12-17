import React from "react";
import AppNavbar from "../components/AppNavbar";
import DisplayUsers from "../components/DisplayUsers";

const Users = () => {
    return (
        <div style={{
            display: "flex",
            flexDirection: "column",
            justifyContent: "center",
            alignItems: "center"
        }}>
            <AppNavbar />
            <DisplayUsers />
        </div>
    );
};

export default Users;
