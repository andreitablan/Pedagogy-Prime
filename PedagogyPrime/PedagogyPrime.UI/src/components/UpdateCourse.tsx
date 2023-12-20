import { Form, Modal } from "react-bootstrap";
import { ref, uploadBytes, getDownloadURL } from "firebase/storage";
import { useState } from "react";
import '../css/courseContent.scss';
import { storage } from "../firebase";
import { v4 } from "uuid";
import axiosInstance from "../AxiosConfig";
import '../css/updateCourse.scss';

const UpdateCourse = ({ item, onModalUpdate }) => {
  const [show, setShow] = useState(false);
  const [formData, setFormData] = useState({
    name: item.name,
    description: item.description,
  });
  const [fileUpload, setFileUpload] = useState<File>();

  const [errors, setErrors] = useState({
    name: '',
    description: '',
  });

  const handleClose = () => {
    setFormData({ name: item.name, description: item.description, file: null });
    setShow(false);
  };

  const handleShow = () => {
    setShow(true);
  };

  const handleFileChange = (e) => {
    const file = e.target.files[0];
    if (file) {
      setFileUpload(file);
    }
  }

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

  const handleUpdate = async () => {
    let url = item.contentUrl;

    if (fileUpload) {
      const fileRef = ref(storage, `files/${fileUpload.name + v4()}`);
      const snapshot = await uploadBytes(fileRef, fileUpload);
      url = await getDownloadURL(snapshot.ref);
    }

    axiosInstance.put(`https://localhost:7136/api/v1/Courses/${item.id}`, {
      name: formData.name,
      description: formData.description,
      contentUrl: url,
      isVisibleForStudents: item.isVisibleForStudents
    })
      .then((result) => {
        onModalUpdate(result.data);
      })
      .catch((error) => {
        console.log(error);
      });


    handleClose();
  };

  return (
    <div className="update-course-component">
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
              <Form.Control type="file" name="file" onChange={handleFileChange} />
            </Form.Group>
          </Form>
        </Modal.Body>
        <Modal.Footer className="d-flex justify-content-center">
          <button type="button" className="btn btn-success" onClick={handleUpdate} disabled={errors.name.length > 0 || errors.description.length > 0}>Update</button>
        </Modal.Footer>
      </Modal>
    </div>
  );
};


export default UpdateCourse;