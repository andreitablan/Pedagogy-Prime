import React, { useState, useEffect, Fragment } from "react";
import { Link } from "react-router-dom";
import "bootstrap/dist/css/bootstrap.min.css";
import Card from "react-bootstrap/Card";
import Container from "react-bootstrap/Container";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import axiosInstance from "../AxiosConfig";
import SubjectForm from "../pages/SubjectForm";

interface Subject {
  id: string;
  name: string;
  period: string;
  noOfCourses: number;
}

const DisplaySubjects = () => {
  const [subjects, setSubjects] = useState<Subject[]>([]);

  useEffect(() => {
    getData();
  }, []);

  const getData = () => {
    axiosInstance
      .get("https://localhost:7136/api/v1.0/Subjects")
      .then((result) => {
        const fetchedSubjects: Subject[] = result.data.resource;
        console.log(fetchedSubjects);
        setSubjects(fetchedSubjects);
      })
      .catch((error) => {
        console.log(error);
      });
  };

  return (
    <Fragment>
      <Container fluid>
        <Row xs={1} md={2} lg={3} xl={4} className="g-4">
          {subjects.map((subject, index) => (
            <Col key={index}>
              <Link
                to={`/subject/${subject.id}`}
                style={{ textDecoration: "none" }}
              >
                <Card
                  style={{
                    background:
                      "linear-gradient(to bottom right, #594CF5, #6000FC, #5107F5, #2A1AE1)",
                    color: "white",
                    marginBottom: "16px",
                    border: "1px solid black",
                    boxShadow: "4px 4px 5px rgba(0, 0, 0, 0.5)",
                  }}
                >
                  <Card.Body>
                    <Card.Title>{subject.name}</Card.Title>
                    <Card.Subtitle className="mb-2 text-muted">
                      {subject.period}
                    </Card.Subtitle>
                    <Card.Text>No of Courses: {subject.noOfCourses}</Card.Text>
                  </Card.Body>
                </Card>
              </Link>
              {/* Render SubjectForm with subjectId prop */}
            </Col>
          ))}
        </Row>
      </Container>
    </Fragment>
  );
};

export default DisplaySubjects;
