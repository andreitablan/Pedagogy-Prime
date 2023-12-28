import { useContext, useEffect, useState } from "react";
import axiosInstance from "../AxiosConfig";
import "../css/subjectDetails.scss";
import { Course } from "../models/Course";
import { CoverageDetails } from "../models/Coverage";
import mapNumberToRole, { Role, UserDetails } from "../models/UserDetails";
import CourseContent from "./CourseContent";
import { Link } from "react-router-dom";
import { Button, Spinner } from "react-bootstrap";
import { UserContext } from "../App";
import UpdateCourse from "./UpdateCourse";
import AddUser from "./AddUser";
import AddParticipant from "./AddParticipant";
import ChatIcon from '@mui/icons-material/Chat';
import SubjectChat from "./SubjectChat";

const SubjectDetails = ({ id }) => {
  const [subject, setSubject] = useState({
    id: "",
    name: "",
    period: "",
    noOfCourses: 0,
    coursesDetails: [],
  });

  const { user } = useContext(UserContext);
  const [shouldShowParticipants, setShouldShowParticipants] = useState(false);
  const [participants, setParticipants] = useState<UserDetails[] | undefined>(undefined);
  const [loadingCoverage, setLoadingCoverage] = useState(false);
  const [loadingCoverageId, setLoadingCoverageId] = useState([]);

  useEffect(() => {
    getData();
  }, []);

  const getData = async () => {
    try {
      const result = await axiosInstance.get(`https://localhost:7136/api/v1.0/subjects/${id}`);
      setSubject(result.data.resource);
    } catch (error) {
      console.log(error);
    }
  };

  const handleGetParticipants = async () => {
    try {
      const result = await axiosInstance.get(`https://localhost:7136/api/v1.0/subjects/${id}/users`);
      const updatedParticipants = result.data.resource.map((x) => ({ ...x, role: mapNumberToRole(x.role) }));
      setParticipants(updatedParticipants);
    } catch (error) {
      console.log(error);
    }
  };

  const toggleParticipants = () => {
    setShouldShowParticipants(!shouldShowParticipants);
    handleGetParticipants();
    setSubject({ ...subject });
  };

  const handleChangeCourseVisibility = async (course) => {
    try {
      course.isVisibleForStudents = !course.isVisibleForStudents;
      await axiosInstance.put(`https://localhost:7136/api/v1.0/Courses/${course.id}`, course);
      setSubject({ ...subject });
    } catch (error) {
      course.isVisibleForStudents = false;
    }
  };

  const handleGenerateAllCourseCoverages = async () => {
    try {
      await Promise.all(subject.coursesDetails.map((course) => generateCourseCoverage(course)));
    } catch (error) {
      console.log(error);
    }
  };

  const generateCourseCoverage = async (course) => {
    setLoadingCoverageId((prevIds) => [...prevIds, course.id]);
    let coverageData = await fetchCoverageData(course);
    await updateCourseInAPI(course, coverageData);
  };

  const fetchCoverageData = async (course) => {
    try {
      const result = await axiosInstance.post("http://localhost:5000/check-course", {
        firebase_link: course.contentUrl,
        description: course.description,
      });
      const { coverage, good_keywords, bad_keywords } = result.data;
      return {
        id: course.coverage.id,
        percentage: coverage,
        goodWords: good_keywords,
        badWords: bad_keywords,
        courseId: course.id,
      };
    } catch (error) {
      console.log(error);
    }
  };

  const updateCourseInAPI = async (course, coverageData) => {
    try {
      subject.coursesDetails.forEach((x) => {
        if (x.id === course.id) {
          x.coverage = {
            id: course.coverage.id,
            percentage: coverageData.percentage,
            goodWords: coverageData.goodWords,
            badWords: coverageData.badWords,
          };
        }
      });
      setSubject({ ...subject });

      const url = course.coverage ? `https://localhost:7136/api/v1.0/Coverage/${course.coverage.id}` : "https://localhost:7136/api/v1.0/Coverage";
      await axiosInstance[url ? 'put' : 'post'](url, coverageData);

      setLoadingCoverageId((prevIds) => prevIds.filter((id) => id !== course.id));
      console.log("Coverage was generated!");
    } catch (error) {
      console.log(error);
    }
  };

  const handleGenerateCourseCoverage = async (course) => {
    try {
      setLoadingCoverageId((prevIds) => [...prevIds, course.id]);
      const coverageData = await fetchCoverageData(course);
      await updateCourseInAPI(course, coverageData);
    } catch (error) {
      console.log(error);
    }
  };

  const handleModalUpdate = (response) => {
    const course = response.resource;
    subject.coursesDetails.forEach((x) => {
      if (x.id === course.id) {
        x.name = course.name;
        x.description = course.description;
        x.contentUrl = course.contentUrl;
        x.coverage = course.coverage;
      }
    });
    setSubject({ ...subject });
  };

  const handleAddParticipant = (response) => {
    setParticipants((prevParticipants) => (prevParticipants ? [...prevParticipants, response] : [response]));
  };

  if (!subject.id) {
    return <p>Loading...</p>;
  }

  return (
    <div className="subject-wrapper">
      <div className="back-button">
        <Link to="/subjects" style={{ textDecoration: "none" }}>
          Back to subjects
        </Link>
      </div>
      <div className="header">
        <div className="left">
          <div className="name">{subject.name}</div>
          <div>- {subject.noOfCourses} courses</div>
        </div>
        <div className="right">{subject.period}</div>
      </div>
      <div className="actions">
        <SubjectChat subjectId={subject.id} subjectName={subject.name}></SubjectChat>
        {[Role.Admin.toString(), Role.Teacher.toString()].includes(user.role) && (
          <AddParticipant subjectId={subject.id} onParticipantAdd={handleAddParticipant}></AddParticipant>
        )}
        <button className="show-students btn btn-success" onClick={toggleParticipants}>
          {shouldShowParticipants ? "Hide Participants" : "Show Participants"}
        </button>
        <button className="show-students btn btn-success" onClick={handleGenerateAllCourseCoverages}>
          Generate All Coverages
        </button>
      </div>
      <div className="accordion" id="accordionPanelsStayOpenExample">
        {subject.coursesDetails.filter((x) => x.isVisibleForStudents).length === 0 &&
        ![Role.Admin.toString(), Role.Teacher.toString()].includes(user.role) ? (
          <div>There are no courses available yet.</div>
        ) : (
          subject.coursesDetails.map((course, index) => {
            if (course.isVisibleForStudents || [Role.Admin.toString(), Role.Teacher.toString()].includes(user.role)) {
              return (
                <div key={index} className="accordion-item">
                  <h2
                    className="accordion-header"
                    id={`panelsStayOpen-heading-${index}`}
                  >
                    <button
                      className={`accordion-button ${index !== 0 ? "collapsed" : ""
                        }`}
                      type="button"
                      data-bs-toggle="collapse"
                      data-bs-target={`#panelsStayOpen-collapse-${index}`}
                      aria-expanded={index === 0} // Expand the first item by default
                      aria-controls={`panelsStayOpen-collapse-${index}`}
                    >
                      {loadingCoverage || loadingCoverageId.includes(course.id) ? (
                        <Spinner animation="border" role="status">
                          <span className="visually-hidden">Loading...</span>
                        </Spinner>
                      ) : course.coverage ? (
                        <div
                          className={`coverage ${course.coverage.percentage < 50 ? "fail" : "success"
                            }`}
                        >
                          {course.coverage.percentage}%
                        </div>
                      ) : (
                        <div className="coverage fail"> 0% </div>
                      )}
                      <div className="course-name">
                        Course {index + 1}: {course.name}
                      </div>
                    </button>
                  </h2>
                  <div
                    id={`panelsStayOpen-collapse-${index}`}
                    className={`accordion-collapse collapse ${index === 0 ? "show" : ""
                      }`}
                    aria-labelledby={`panelsStayOpen-heading-${index}`}
                  >
                    <div className="accordion-body">
                      <div className="coverage-words">
                        {course.coverage &&
                        [Role.Admin.toString(), Role.Teacher.toString()].includes(user.role) && (
                          <>
                            <strong>Relevant topics: </strong>
                            {[...course.coverage.goodWords.keys()].map((i) => (
                              <span key={i} className="good-word">
                                {course.coverage.goodWords[i]}
                                {i < course.coverage.goodWords.length - 1 && " "}
                              </span>
                            ))}
                            {[...course.coverage.badWords.keys()].map((i) => (
                              <span key={i} className="bad-word">
                                {course.coverage.badWords[i]}
                                {i < course.coverage.badWords.length - 1 && " "}
                              </span>
                            ))}
                          </>
                        )}
                      </div>
                      {course.description}
                      <div className="course-actions">
                        <CourseContent contentUrl={course.contentUrl} name={course.name}></CourseContent>
                        {[Role.Admin.toString(), Role.Teacher.toString()].includes(user.role) && (
                          <Button onClick={() => handleChangeCourseVisibility(course)}>
                            {course.isVisibleForStudents
                              ? "Hide Course from Students"
                              : "Make Visible for Students"}
                          </Button>
                        )}
                        {[Role.Admin.toString(), Role.Teacher.toString()].includes(user.role) && (
                          <Button onClick={() => handleGenerateCourseCoverage(course)}>
                            {course.coverage === null
                              ? "Generate Coverage"
                              : "Regenerate Coverage"}
                          </Button>
                        )}
                        {[Role.Admin.toString(), Role.Teacher.toString()].includes(user.role) && (
                          <UpdateCourse item={course} onModalUpdate={handleModalUpdate}></UpdateCourse>
                        )}
                      </div>
                    </div>
                  </div>
                </div>
              );
            }
          })
        )}
      </div>
      {participants && shouldShowParticipants && (
        <div className="participants">
          {participants.map((student, index) => (
            <div key={index} className="card">
              <div className="card-header">
                {student.lastName} {student.firstName}
              </div>
              <div className="card-body">
                <p className="card-text">Email: {student.email}</p>
                <p className="card-text">Role: {student.role}</p>
              </div>
            </div>
          ))}
        </div>
      )}
    </div>
  );
};

export default SubjectDetails;
