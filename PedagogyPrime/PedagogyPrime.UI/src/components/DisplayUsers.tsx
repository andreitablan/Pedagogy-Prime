import AddUser from "./AddUser";
import '../css/displayUsers.scss';
import { Card, Col, Row } from "react-bootstrap";
import { useEffect, useState } from "react";
import UserDetails from "../models/UserDetails";
import axiosInstance from "../AxiosConfig";
import { Link } from "react-router-dom";
import DeleteIcon from "@mui/icons-material/Delete";
import mapNumberToRole from "../models/UserDetails";

interface UserDetails {
    id: string;
    email: string;
    firstName: string;
    lastName: string;
    role: string;
    username: string;
}

const DisplayUsers = () => {
    const [users, setUsers] = useState<UserDetails[]>([]);

    useEffect(() => {
        getData();
    }, []);

    const getData = () => {
        axiosInstance
            .get('https://localhost:7136/api/v1.0/Users')
            .then((result) => {
                result.data.resource.map((x) => {
                    x.role = mapNumberToRole(x.role);
                });
                setUsers(result.data.resource);
            })
            .catch((error) => {
                console.log(error);
            });
    }

    const handleDelete = (id: string) => {
        axiosInstance
            .delete(`https://localhost:7136/api/v1.0/Users/${id}`)
            .then((result) => {
                const newUsers = users.filter(x => x.id != result.data.resource);
                setUsers(newUsers);
            })
            .catch((error) => {
                console.log(error);
            });
    }

    const handleAddUser = (user: UserDetails) => {
        const newUsers = users.map(x => x);
        newUsers.push(user);
        setUsers(newUsers);
    }

    return (
        <div className="content-container">
            <AddUser onModalUpdate={handleAddUser} />
            <Row xs={1} md={2} lg={3} xl={4} className="g-4" >
                {users.map((user, index) => (
                    <Col key={index}>

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
                                    onClick={() => handleDelete(user.id)}
                                />
                                <Card.Title>{user.firstName} {user.lastName}</Card.Title>
                                <Card.Subtitle className="mb-2 text-whitesmoke">
                                    {user.username}
                                </Card.Subtitle>
                                <Card.Text>Email: {user.email}</Card.Text>
                                <Card.Text>role: {user.role}</Card.Text>
                            </Card.Body>
                        </Card>
                    </Col>
                ))}
            </Row>
        </div >
    )
}

export default DisplayUsers;