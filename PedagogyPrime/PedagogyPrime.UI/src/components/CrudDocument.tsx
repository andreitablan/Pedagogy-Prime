import React, { useState, useEffect, Fragment } from "react";
import "bootstrap/dist/css/bootstrap.min.css";
import Button from "react-bootstrap/Button";
import Modal from "react-bootstrap/Modal";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import Table from "react-bootstrap/Table";
import Container from "react-bootstrap/Container";
import axiosInstance from "../AxiosConfig";

interface Document {
  state: number;
  documentType: number;
  contentUrl: string;
  userId: string;
  firebaseLink: string;
}

const CrudDocument = () => {
  const [show, setShow] = useState(false);

  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);

  //new form
  const [state, setState] = useState("");
  const [documentType, setDocumentType] = useState("");
  const [contentUrl, setContentUrl] = useState("");
  const [userId, setUserId] = useState("");
  const [firebaseLink, setFirebaseLink] = useState("");

  //edit form
  const [editState, setEditState] = useState("");
  const [editDocumentType, setEditDocumentType] = useState("");
  const [editContentUrl, setEditContentUrl] = useState("");
  const [editUserId, setEditUserId] = useState("");
  const [editFirebaseLink, setEditFirebaseLink] = useState("");

  const [data, setData] = useState<Document[]>([]);
  useEffect(() => {
    getData();
  }, []);

  const getData = () => {
    axiosInstance
      .get("https://localhost:7136/api/v1.0/Documents")
      .then((result) => {
        console.log(result.data.resource);
        const documents: Document[] = result.data.resource;
        setData(documents);
      })
      .catch((error) => {
        console.log(error);
      });
  };
  const handleEdit = (name) => {
    //alert(name);
    handleShow();
  };
  const handleDelete = (name) => {
    if (window.confirm("Are you sure to delete this employee") == true) {
      //alert(name);
    }
  };
  const handleUpdate = () => {};
  return (
    <Fragment>
      {/* <Container>
                        <Row>
                            <Col>
                                <input type="text" className="form-control" placeholder="Enter Name"
                                value={name} onChange={(e)=>setName(e.target.value)}/>
                            </Col>
                            <Col>       
                                <input type="text" className="form-control" placeholder="Enter Age"
                                value={age} onChange={(e)=>setAge(e.target.value)}/>
                            </Col>
                            <Col>        
                                <input type="checkbox"
                                checked = {isActive === 1 ? true : false}
                                onChange={(e)=>setIsActive(e)} value={isActive}/>
                                <label>IsActive</label>
                            </Col>
                        </Row>
                    </Container> */}
      <br></br>
      <Table striped bordered hover>
        <thead>
          <tr>
            <th>UserId</th>
            <th>Content</th>
            <th>State</th>
            <th>Type</th>
            <th>FirebaseLink</th>
          </tr>
        </thead>
        <tbody>
          {data && data.length > 0
            ? data.map((item, index) => {
                return (
                  <tr key={index}>
                    <td>{item.userId}</td>
                    <td>{item.contentUrl}</td>
                    <td>{item.state}</td>
                    <td>{item.documentType}</td>
                    <td>{item.firebaseLink}</td>
                    <td colSpan={2}>
                      <button
                        className="btn btn-primary"
                        onClick={() => handleEdit(item.contentUrl)}
                      >
                        {" "}
                        Edit
                      </button>
                      <button
                        className="btn btn-danger"
                        onClick={() => handleDelete(item.contentUrl)}
                      >
                        Delete
                      </button>
                    </td>
                  </tr>
                );
              })
            : "Loading...."}
        </tbody>
      </Table>
      <Modal show={show} onHide={handleClose}>
        <Modal.Header closeButton>
          <Modal.Title>Modal / Update Employee</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Container>
            <Row>
              <Col>
                <input
                  type="text"
                  className="form-control"
                  placeholder="Enter User Id"
                  value={editUserId}
                  onChange={(e) => setEditUserId(e.target.value)}
                />
              </Col>
              <Col>
                <input
                  type="text"
                  className="form-control"
                  placeholder="Enter Content"
                  value={editContentUrl}
                  onChange={(e) => setEditContentUrl(e.target.value)}
                />
              </Col>
              <Col>
                <input
                  type="text"
                  className="form-control"
                  placeholder="Enter State"
                  value={editState}
                  onChange={(e) => setEditState(e.target.value)}
                />
              </Col>
              <Col>
                <input
                  type="text"
                  className="form-control"
                  placeholder="Enter Document Type"
                  value={editDocumentType}
                  onChange={(e) => setEditDocumentType(e.target.value)}
                />
              </Col>
              <Col>
                <input
                  type="text"
                  className="form-control"
                  placeholder="Enter Firebase link"
                  value={editFirebaseLink}
                  onChange={(e) => setEditFirebaseLink(e.target.value)}
                />
              </Col>
            </Row>
          </Container>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={handleClose}>
            Close
          </Button>
          <Button variant="primary" onClick={handleUpdate}>
            Save Changes
          </Button>
        </Modal.Footer>
      </Modal>
    </Fragment>
  );
};

export default CrudDocument;
