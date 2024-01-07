import React, { useEffect, useState } from "react";
import { Card, Col, Row } from "react-bootstrap";
import DeleteIcon from "@mui/icons-material/Delete";
import AddUser from "./AddUser";
import axiosInstance from "../AxiosConfig";
import mapNumberToRole from "../models/UserDetails";
import "../css/displayUsers.scss";

interface UserCardProps {
  user: UserDetails;
  onDelete: (id: string) => void;
}

const UserCard: React.FC<UserCardProps> = ({ user, onDelete }) => {
  return (
    <Card
      style={{
        background:
          "linear-gradient(to bottom right, #594CF5, #6000FC, #5107F5, #2A1AE1)",
        height: "150px",
        color: "white",
        marginBottom: "16px",
        boxShadow: "4px 4px 5px rgba(0, 0, 0, 0.5)",
      }}
    >
      <Card.Body>
        <DeleteIcon
          style={{ color: "white", float: "right" }}
          onClick={() => onDelete(user.id)}
        />
        <Card.Title>{`${user.firstName} ${user.lastName}`}</Card.Title>
        <Card.Subtitle className="mb-2 text-whitesmoke">{user.username}</Card.Subtitle>
        <Card.Text>Email: {user.email}</Card.Text>
        <Card.Text>Role: {user.role}</Card.Text>
      </Card.Body>
    </Card>
  );
};

const DisplayUsers: React.FC = () => {
  const [users, setUsers] = useState<UserDetails[]>([]);

  useEffect(() => {
    getData();
  }, []);

  const getData = () => {
    axiosInstance
      .get("https://localhost:7136/api/v1.0/Users")
      .then((result) => {
        const updatedUsers = result.data.resource.map((x) => ({
          ...x,
          role: mapNumberToRole(x.role),
        }));
        setUsers(updatedUsers);
      })
      .catch((error) => {
        console.log(error);
      });
  };

  const handleDelete = (id: string) => {
    axiosInstance
      .delete(`https://localhost:7136/api/v1.0/Users/${id}`)
      .then(() => {
        const newUsers = users.filter((x) => x.id !== id);
        setUsers(newUsers);
      })
      .catch((error) => {
        console.log(error);
      });
  };

  const handleAddUser = (user: UserDetails) => {
    setUsers((prevUsers) => [...prevUsers, user]);
  };

  return (
    <div className="content-container">
      <AddUser onModalUpdate={handleAddUser} />
      <Row xs={1} md={2} lg={3} xl={4} className="g-4">
        {users.map((user, index) => (
          <Col key={index}>
            <UserCard user={user} onDelete={handleDelete} />
          </Col>
        ))}
      </Row>
    </div>
  );
};

export default DisplayUsers;
