import { Button, Form, Modal } from "react-bootstrap";
import { ref, uploadBytes, getDownloadURL } from "firebase/storage";
import { useState } from "react";

const UpdateCourse = ({ item }) => {
    const [show, setShow] = useState(false);
    const [formData, setFormData] = useState({
      name: item.name,
      description: item.description,
      file: null,
    });
  
    const [errors, setErrors] = useState({
      name: '',
      description: '',
    });
  
    const handleClose = () => {
        setFormData({name: item.name, description: item.description, file: null});
      setShow(false);
    };
  
    const handleShow = () => {
      setShow(true);
    };
  
    const handleInputChange = (e) => {
      const { name, value, files } = e.target;
  
      const newErrors = { ...errors };
      newErrors[name] = value.trim() === '' ? 'Field cannot be empty' : '';
  
      setErrors(newErrors);
  
      setFormData({
        ...formData,
        [name]: value || (files && files[0]),
      });
    };
  
    const handleUpdate = () => {
        
        handleClose();
    };
  
    return (
      <div>
        <button type="button" className="btn btn-success" onClick={handleShow}>
          Update
        </button>
        <Modal show={show} onHide={handleClose} centered>
          <Modal.Header closeButton>
            <Modal.Title>Update Course</Modal.Title>
          </Modal.Header>
          <Modal.Body>
            <Form>
              <Form.Group className="mb-3">
                <Form.Label>Name</Form.Label>
                <Form.Control
                  type="text"
                  placeholder="Name"
                  name="name"
                  value={formData.name}
                  onChange={handleInputChange}
                />
                {errors.name && <div style={{ color: 'red' }}>{errors.name}</div>}
              </Form.Group>
              <Form.Group className="mb-3">
                <Form.Label>Description</Form.Label>
                <Form.Control
                  as="textarea"
                  rows={5}
                  placeholder="Description"
                  name="description"
                  value={formData.description}
                  onChange={handleInputChange}
                />
                {errors.description && <div style={{ color: 'red' }}>{errors.description}</div>}
              </Form.Group>
              <Form.Group className="mb-3">
                <Form.Label>Content</Form.Label>
                <Form.Control type="file" name="file" onChange={handleInputChange} />
              </Form.Group>
            </Form>
          </Modal.Body>
          <Modal.Footer className="d-flex justify-content-center">
            <Button onClick={handleUpdate} disabled={errors.name.length > 0 || errors.description.length > 0}>Update</Button>
          </Modal.Footer>
        </Modal>
      </div>
    );
  };
  

export default UpdateCourse;