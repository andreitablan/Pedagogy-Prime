import { Button, Col, Form, Modal } from "react-bootstrap";
import { useState } from "react";
import { Role } from "../models/UserDetails";
import Select from "react-select/base";
import axiosInstance from "../AxiosConfig";

const AddParticipant = ({ onModalUpdate }) => {
    const [show, setShow] = useState(false);
    const [formData, setFormData] = useState({
        email: '',
        firstName: '',
        lastName: '',
        role: Role.Student,
    });

    const [errors, setErrors] = useState({
        name: '',
        description: '',
    });

    const roles = Object.values(Role).filter(role => role !== ''); // Exclude the empty string from the options

    const handleClose = () => {
        setFormData({ email: '', firstName: '', lastName: '', role: Role.Student });
        setShow(false);
    };

    const handleShow = () => {
        setShow(true);
    };

    const handleInputChange = (e) => {
        const { name, value } = e.target;

        const newErrors = { ...errors };
        newErrors[name] = value.trim() === '' ? 'Field cannot be empty' : '';

        setErrors(newErrors);

        setFormData({
            ...formData,
            [name]: value,
        });
    };

    const handleSave = () => {
        const user = {
            username: formData.email.split('@')[0],
            email: formData.email,
            firstName: formData.firstName,
            lastName: formData.lastName,
            password: formData.email.split('@')[0],
            role: 0,
        };
        axiosInstance.post(`https://localhost:7136/api/v1/users`, user)
            .then((result) => {
                onModalUpdate({
                    id: result.data.resource,
                    firstName: user.firstName,
                    lastName: user.lastName,
                    email: user.email,
                    role: user.role
                });
            })
            .catch((error) => {
                console.log(error);
            });
        handleClose();
    };

    return (
        <div>
            <button type="button" className="btn btn-success" onClick={handleShow}>
                Add Participant
            </button>
            <Modal show={show} onHide={handleClose} centered>
                <Modal.Header closeButton>
                    <Modal.Title>Add Participant</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form>
                        <Form.Group className="mb-3">
                            <Form.Label>Email</Form.Label>
                            <Form.Control
                                type="email"
                                placeholder="John@Doe.com"
                                name="email"
                                value={formData.email}
                                onChange={handleInputChange}
                            />
                            {errors.name && <div style={{ color: 'red' }}>{errors.name}</div>}
                        </Form.Group>
                        <Form.Group className="mb-3">
                            <Form.Label>First Name</Form.Label>
                            <Form.Control
                                type="text"
                                placeholder="John"
                                name="firstName"
                                value={formData.firstName}
                                onChange={handleInputChange}
                            />
                            {errors.name && <div style={{ color: 'red' }}>{errors.name}</div>}
                        </Form.Group>
                        <Form.Group className="mb-3">
                            <Form.Label>Last Name</Form.Label>
                            <Form.Control
                                type="text"
                                placeholder="Doe"
                                name="lastName"
                                value={formData.lastName}
                                onChange={handleInputChange}
                            />
                            {errors.name && <div style={{ color: 'red' }}>{errors.name}</div>}
                        </Form.Group>
                        <Form.Group controlId="formRole">
                            <Form.Label column sm={2}>
                                Select Role:
                            </Form.Label>
                            <Col sm={10}>
                                <Form.Select
                                    name="role"
                                    value={formData.role}
                                    onChange={handleInputChange}
                                >
                                    {roles.map((role) => (
                                        <option key={role} value={role}>
                                            {role}
                                        </option>
                                    ))}
                                </Form.Select>
                            </Col>
                        </Form.Group>
                    </Form>
                </Modal.Body>
                <Modal.Footer className="d-flex justify-content-center">
                    <Button onClick={handleSave} disabled={errors.name.length > 0 || errors.description.length > 0}>Save</Button>
                </Modal.Footer>
            </Modal>
        </div>
    );
};


export default AddParticipant;