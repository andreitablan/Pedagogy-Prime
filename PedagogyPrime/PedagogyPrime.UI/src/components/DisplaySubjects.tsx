import { Link, useNavigate } from "react-router-dom";
import React, { useState, useEffect, Fragment, useContext } from "react";
import "bootstrap/dist/css/bootstrap.min.css";
import Card from "react-bootstrap/Card";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import axiosInstance from "../AxiosConfig";
import DeleteIcon from "@mui/icons-material/Delete";
import EditIcon from "@mui/icons-material/Edit";
import { Role } from "../models/UserDetails";
import "../css/displaySubjects.scss";
import { Button } from "react-bootstrap";
import { UserContext } from "../App";
import { getStorage, ref, deleteObject } from "firebase/storage";
import { storage } from "../firebase";

interface Subject {
  id: string;
  name: string;
  period: string;
  noOfCourses: number;
}

const DisplaySubjects = () => {
  const [subjects, setSubjects] = useState<Subject[]>([]);
  const navigate = useNavigate();
  const storage = getStorage();
  useEffect(() => {
    getData();
  }, []);

  const { user } = useContext(UserContext);


  const getData = () => {
    let url;
    if(user.role === Role.Admin){
      url = "https://localhost:7136/api/v1.0/Subjects"
      
    }
    else{
      url = `https://localhost:7136/api/v1.0/users/${user.id}/subjects`;
    }

    axiosInstance
      .get(url)
      .then((result) => {
        const fetchedSubjects: Subject[] = result.data.resource;
        setSubjects(fetchedSubjects);
      })
      .catch((error) => {
        console.log(error);
      });
  };

  const getPathStorageFromUrl = (url: string): string => {
    const str = url.toString();
    console.log(str);
    const start = str.indexOf("%2F") + 3;
    const end = str.indexOf("?");
    return str.substring(start, end);
  };

  const handleDelete = (subjectId: string) => {
    axiosInstance
      .get(`https://localhost:7136/api/v1.0/Subjects/${subjectId}`)
      .then((result) => {
        for (let index = 0; index < result.data.resource.noOfCourses; index++) {
          const fileurl = result.data.resource.coursesDetails[index].contentUrl;
          const filePath = getPathStorageFromUrl(fileurl);
          const fileRef = ref(storage, "files/" + filePath);
          deleteObject(fileRef)
            .then(() => {
              console.log("File deleted successfully");
            })
            .catch((error) => {
              console.log("Error deleting file:", error);
            });
        }
      })
      .catch((error) => {
        console.log(error);
      });

    axiosInstance
      .delete(`https://localhost:7136/api/v1.0/Subjects/${subjectId}`)
      .then((result) => {
        if(result.data.resource){
          const newSubjects = subjects.filter(x => x.id != subjectId);

          setSubjects(newSubjects);
        }
        navigate("/subjects");
      })
      .catch((error) => {
        console.log("Error deleting subject:", error);
      });
  };

  return (
    <div className="subjects">
      <Link to="/subjects/create">
        <Button
          style={{
            background:
              "linear-gradient(to bottom right, #594CF5, #6000FC, #5107F5, #2A1AE1)",
            color: "white",
            marginTop: "15px",
            marginBottom: "25px",
            boxShadow: "4px 4px 5px rgba(0, 0, 0, 0.5)",
          }}
          className="btn btn-primary"
        >
          New Subject
        </Button>
      </Link>
        <Row xs={1} md={2} lg={3} xl={4} className="g-4" >
          {subjects.map((subject, index) => (
            <Col key={index}>
              <Link
                to={`/subjects/${subject.id}`}
                style={{ textDecoration: "none" }}
              >
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
                    <Link to={`/subjects/${subject.id}/edit`}>
                      <DeleteIcon
                        style={{ color: "white", float: "right" }}
                        onClick={() => handleDelete(subject.id)}
                      />
                    </Link>
                    <Link to={`/subjects/${subject.id}/edit`}>
                      <EditIcon style={{ color: "white", float: "right" }} />
                    </Link>
                    <Card.Title>{subject.name}</Card.Title>
                    <Card.Subtitle className="mb-2 text-whitesmoke">
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
    </div>
  );
};

export default DisplaySubjects;
