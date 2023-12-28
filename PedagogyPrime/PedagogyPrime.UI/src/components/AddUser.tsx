import { Button, Form, Modal } from "react-bootstrap";
import { useState } from "react";
import { mapNumberToRole, Role, mapRoleToNumber } from "../models/UserDetails";
import axiosInstance from "../AxiosConfig";

const AddUser = ({ onModalUpdate }) => {
  const [show, setShow] = useState(false);
  const [formData, setFormData] = useState({
    email: '',
    firstName: '',
    lastName: '',
    role: Role.Student,
  });

  const [errors, setErrors] = useState({
    email: '',
    firstName: '',
    lastName: '',
  });

  const roles = Object.values(Role).filter(role => role !== ''); // Exclude the empty string from the options

  const handleClose = () => {
    setFormData({ email: '', firstName: '', lastName: '', role: Role.Student });
    setErrors({ email: '', firstName: '', lastName: '' });
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

  const validateForm = () => {
    const newErrors = { ...errors };

    Object.entries(formData).forEach(([key, value]) => {
      newErrors[key] = value.trim() === '' ? 'Field cannot be empty' : '';
    });

    setErrors(newErrors);

    return !Object.values(newErrors).some(error => error !== '');
  };

  const handleSave = () => {
    if (!validateForm()) {
      return; 
    }

    const { email, firstName, lastName, role } = formData;

    const username = email.split('@')[0];
    const password = username;

    const user = {
      username,
      email,
      firstName,
      lastName,
      password,
      role: mapRoleToNumber(role),
    };

    axiosInstance.post(`https://localhost:7136/api/v1/users`, user)
      .then((result) => {
        const updatedUser = {
          id: result.data.resource,
          firstName: user.firstName,
          lastName: user.lastName,
          email: user.email,
          username: user.username,
          role: mapNumberToRole(user.role),
        };
        onModalUpdate(updatedUser);
      })
      .catch((error) => {
        console.log(error);
      });

    handleClose();
  };

  return (
    <div>
      <Button type="button" className="btn btn-success add-user" onClick={handleShow}>
        Add User
      </Button>
      <Modal show={show} onHide={handleClose} centered>
        <Modal.Header closeButton>
          <Modal.Title>Add User</Modal.Title>
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
              {errors.email && <div style={{ color: 'red' }}>{errors.email}</div>}
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
              {errors.firstName && <div style={{ color: 'red' }}>{errors.firstName}</div>}
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
              {errors.lastName && <div style={{ color: 'red' }}>{errors.lastName}</div>}
            </Form.Group>
            <Form.Group controlId="formRole">
              <Form.Label>Select Role:</Form.Label>
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
            </Form.Group>
          </Form>
        </Modal.Body>
        <Modal.Footer className="d-flex justify-content-center">
          <Button onClick={handleSave} disabled={Object.values(errors).some(error => error !== '')}>
            Save
          </Button>
        </Modal.Footer>
      </Modal>
    </div>
  );
};

export default AddUser;
