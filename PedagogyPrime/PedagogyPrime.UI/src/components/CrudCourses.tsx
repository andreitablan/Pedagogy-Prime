import React, { useState, useEffect, Fragment } from "react";
import "bootstrap/dist/css/bootstrap.min.css";
import Card from "react-bootstrap/Card";
import Button from "react-bootstrap/Button";
import Modal from "react-bootstrap/Modal";
import Container from "react-bootstrap/Container";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import axiosInstance from "../AxiosConfig";
import { Course } from "../models/Course";



const CrudCourse = () => {
  const [show, setShow] = useState(false);
  const [selectedCourse, setSelectedCourse] = useState<Course | null>(null);
  const [name, setName] = useState("");
  const [description, setDescription] = useState("");
  const [coverage, setCoverage] = useState(0);
  const [contentUrl, setContentUrl] = useState("");
  const [subjectId, setSubjectId] = useState("");

  const handleClose = () => {
    setShow(false);
    setSelectedCourse(null);
  };

  const handleShow = (course: Course) => {
    setSelectedCourse(course);
    setShow(true);
  };

  const handleSave = () => {
    const url = "https://localhost:7136/api/v1/Courses";

    const data = {
      name: name,
      description: description,
      coverage: coverage,
      contentUrl: contentUrl,
      subjectId: subjectId,
    };
    axiosInstance.post(url, data).then(() => {
      getData();
      clear();
    });
  };

  const clear = () => {
    setName("");
    setContentUrl("");
    setCoverage(0);
    setDescription("");
    setSubjectId("");
  };

  const handleUpdate = () => {
    console.log(selectedCourse);
    if (!selectedCourse) {
      return;
    }

    const data = {
      name: selectedCourse.name,
      description: selectedCourse.description,
      coverage: selectedCourse.coverage,
      contentUrl: selectedCourse.contentUrl,
      subjectId: selectedCourse.subjectId,
    };

    axiosInstance
      .put(
        `https://localhost:7136/api/v1.0/Courses/${selectedCourse?.id}`,
        data
      )
      .then((result) => {
        getData();
        console.log(result.data.resource);
      })
      .catch((error) => {
        console.log(error);
      });
    handleClose();
  };

  const handleDelete = (id: string) => {
    if (window.confirm("Are you sure to delete this course") == true) {
      axiosInstance
        .delete(`https://localhost:7136/api/v1.0/Courses/${id}`)
        .then((result) => {
          console.log(result.data.resource);
          getData();
        })
        .catch((error) => {
          console.log(error);
        });
      handleClose();
    }
  };

  const [data, setData] = useState<Course[]>([]);
  useEffect(() => {
    getData();
  }, []);

  const getData = () => {
    axiosInstance
      .get("https://localhost:7136/api/v1.0/Courses")
      .then((result) => {
        const courses: Course[] = result.data.resource;
        console.log(courses);
        setData(courses);
      })
      .catch((error) => {
        console.log(error);
      });
  };

  return (
    <Fragment>
      <Card
        style={{
          background:
            "linear-gradient(to bottom right, #594CF5, #6000FC, #5107F5, #2A1AE1)",
          color: "white",
          marginBottom: "16px",
          marginLeft: "10px",
          marginRight: "10px",
          boxShadow: "4px 4px 5px rgba(0, 0, 0, 0.5)",
        }}
      >
        <Card.Body>
          <Container>
            <Row>
              <Col>
                <input
                  type="text"
                  className="form-control"
                  placeholder="Enter Name"
                  value={name}
                  onChange={(e) => setName(e.target.value)}
                />
              </Col>
              <Col>
                <input
                  type="text"
                  className="form-control"
                  placeholder="Enter Description"
                  value={description}
                  onChange={(e) => setDescription(e.target.value)}
                />
              </Col>
              <Col>
                <input
                  type="number"
                  className="form-control"
                  placeholder="Enter Coverage"
                  value={coverage}
                  onChange={(e) => setCoverage(parseInt(e.target.value))}
                />
              </Col>
              <Col>
                <input
                  type="text"
                  className="form-control"
                  placeholder="Enter Content URL"
                  value={contentUrl}
                  onChange={(e) => setContentUrl(e.target.value)}
                />
              </Col>
              <Col>
                <input
                  type="text"
                  className="form-control"
                  placeholder="Enter Subject ID"
                  value={subjectId}
                  onChange={(e) => setSubjectId(e.target.value)}
                />
              </Col>
              <Col>
                <button
                  className="btn btn-primary"
                  onClick={() => handleSave()}
                >
                  {" "}
                  Submit
                </button>
              </Col>
            </Row>
          </Container>
        </Card.Body>
      </Card>
      <Container fluid>
        <Row xs={1} md={2} lg={3} xl={4} className="g-4">
          {data && data.length > 0
            ? data.map((course, index) => (
              <Col key={index}>
                <Card
                  style={{
                    background:
                      "linear-gradient(to bottom right,  #594CF5, #6000FC, #5107F5, #2A1AE1, #550dba)",
                    color: "white",
                    marginBottom: "16px",
                    boxShadow: "4px 4px 5px rgba(0, 0, 0, 0.5)",
                  }}
                >
                  <Card.Body>
                    <Card.Title>{course.name}</Card.Title>
                    <Card.Subtitle className="mb-2 text-muted">
                      {course.contentUrl}
                    </Card.Subtitle>
                    <Card.Text>
                      Description: {course.description} | Type:{" "}
                      {course.coverage?.precentage} | Subject: {course.subjectId} |
                      Content: {course.contentUrl}
                    </Card.Text>
                    <Button
                      variant="primary"
                      onClick={() => handleShow(course)}
                    >
                      Edit
                    </Button>
                    <Button
                      variant="danger"
                      onClick={() => handleDelete(course.id)}
                    >
                      Delete
                    </Button>
                  </Card.Body>
                </Card>
              </Col>
            ))
            : "Loading...."}
        </Row>
      </Container>

      <Modal show={show} onHide={handleClose}>
        <Modal.Header closeButton>
          <Modal.Title>Modal / Update Course</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          {selectedCourse && (
            <Container>
              <Row>
                <input
                  type="text"
                  className="form-control"
                  placeholder="Enter Name"
                  value={selectedCourse.name}
                  onChange={(e) =>
                    setSelectedCourse({
                      ...selectedCourse,
                      name: e.target.value,
                    })
                  }
                />
              </Row>
              <Row>
                <input
                  type="text"
                  className="form-control"
                  placeholder="Enter Description"
                  value={selectedCourse.description}
                  onChange={(e) =>
                    setSelectedCourse({
                      ...selectedCourse,
                      description: e.target.value,
                    })
                  }
                />
              </Row>
              <Row>
                <input
                  type="number"
                  className="form-control"
                  placeholder="Enter Coverage"
                  value={selectedCourse.coverage.precentage}
                  onChange={(e) =>
                    setSelectedCourse({
                      ...selectedCourse,
                      coverage: {precentage: parseInt(e.target.value), badWords: [], goodWords: []},
                    })
                  }
                />
              </Row>
              <Row>
                <input
                  type="text"
                  className="form-control"
                  placeholder="Enter Content URL"
                  value={selectedCourse.contentUrl}
                  onChange={(e) =>
                    setSelectedCourse({
                      ...selectedCourse,
                      contentUrl: e.target.value,
                    })
                  }
                />
              </Row>
              <Row>
                <input
                  type="text"
                  className="form-control"
                  placeholder="Enter Subject ID"
                  value={selectedCourse.subjectId}
                  onChange={(e) =>
                    setSelectedCourse({
                      ...selectedCourse,
                      subjectId: e.target.value,
                    })
                  }
                />
              </Row>
            </Container>
          )}
        </Modal.Body>
        <Modal.Footer>
          <Container>
            <Row className="justify-content-center">
              <Col className="text-center">
                <Button variant="secondary" onClick={() => handleClose()}>
                  Close
                </Button>
              </Col>
              <Col className="text-center">
                <Button variant="primary" onClick={() => handleUpdate()}>
                  Save Changes
                </Button>
              </Col>
            </Row>
          </Container>
        </Modal.Footer>
      </Modal>
    </Fragment>
  );
};

export default CrudCourse;
