import { useState, useEffect, useContext } from "react";
import { Link, useNavigate } from "react-router-dom";
import { Button, Card, Col, Row } from "react-bootstrap";
import DeleteIcon from "@mui/icons-material/Delete";
import EditIcon from "@mui/icons-material/Edit";
import { Role } from "../models/UserDetails";
import axiosInstance from "../AxiosConfig";
import { UserContext } from "../App";
import { ref, deleteObject } from "firebase/storage";
import { storage } from "../firebase";
import "../css/displaySubjects.scss";

const API_URLS = {
  subjects: "https://localhost:7136/api/v1.0/Subjects",
  userSubjects: "https://localhost:7136/api/v1.0/users/:userId/subjects",
};

interface Subject {
  id: string;
  name: string;
  period: string;
  noOfCourses: number;
}

const DisplaySubjects = () => {
  const [subjects, setSubjects] = useState<Subject[]>([]);
  const navigate = useNavigate();
  const { user } = useContext(UserContext);

  useEffect(() => {
    getData();
  }, []);

  const getData = () => {
    const url = user.role === Role.Admin ? API_URLS.subjects : API_URLS.userSubjects.replace(':userId', user.id);

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
    const start = str.indexOf("%2F") + 3;
    const end = str.indexOf("?");
    return str.substring(start, end);
  };

  const handleFileDeletion = (coursesDetails: any[]) => {
    coursesDetails.forEach((course) => {
      const fileurl = course.contentUrl;
      const filePath = getPathStorageFromUrl(fileurl);
      const fileRef = ref(storage, "files/" + filePath);

      deleteObject(fileRef)
        .then(() => {
          console.log("File deleted successfully");
        })
        .catch((error) => {
          console.log("Error deleting file:", error);
        });
    });
  };

  const handleSubjectDeletion = (subjectId: string) => {
    axiosInstance
      .delete(`${API_URLS.subjects}/${subjectId}`)
      .then((result) => {
        if (result.data.resource) {
          const newSubjects = subjects.filter((x) => x.id !== subjectId);
          setSubjects(newSubjects);
        }
        navigate("/subjects");
      })
      .catch((error) => {
        console.log("Error deleting subject:", error);
      });
  };

  const handleDelete = (subjectId: string) => {
    axiosInstance
      .get(`${API_URLS.subjects}/${subjectId}`)
      .then((result) => {
        handleFileDeletion(result.data.resource.coursesDetails);
      })
      .catch((error) => {
        console.log(error);
      });

    handleSubjectDeletion(subjectId);
  };

  return (
    <div className="subjects">
      <Link to="/subjects/create">
        <Button
          className="btn btn-primary"
          style={{
            background:
              "linear-gradient(to bottom right, #594CF5, #6000FC, #5107F5, #2A1AE1)",
            color: "white",
            marginTop: "15px",
            marginBottom: "25px",
            boxShadow: "4px 4px 5px rgba(0, 0, 0, 0.5)",
          }}
        >
          New Subject
        </Button>
      </Link>
      <Row xs={1} md={2} lg={3} xl={4} className="g-4">
        {subjects.map((subject) => (
          <Col key={subject.id}>
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
              <Card.Body style={{color: 'white', display: 'flex'}}>
                <Link to={`/subjects/${subject.id}`} style={{ textDecoration: "none", color: 'white', width: '100%' }}>
                  <Card.Title>{subject.name}</Card.Title>
                  <Card.Subtitle className="mb-2 text-whitesmoke">{subject.period}</Card.Subtitle>
                  <Card.Text>No of Courses: {subject.noOfCourses}</Card.Text>
                </Link>

                <div style={{ display: "flex", justifyContent: "center", alignItems: 'center' }}>
                  <Link to={`/subjects/${subject.id}/edit`} style={{ marginRight: "8px" }}>
                    <EditIcon style={{ color: "white" }} />
                  </Link>
                  <Button
                    variant="link"
                    onClick={() => handleDelete(subject.id)}
                    style={{ color: "white", padding: 0 }}
                  >
                    <DeleteIcon />
                  </Button>
                </div>
              </Card.Body>
            </Card>
          </Col>
        ))}
      </Row>
    </div>
  );
};

export default DisplaySubjects;
