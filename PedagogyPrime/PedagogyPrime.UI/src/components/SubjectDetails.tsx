import { useContext, useEffect, useState } from "react";
import axiosInstance from "../AxiosConfig";
import "../css/subjectDetails.scss";
import { Course } from "../models/Course";
import { CoverageDetails } from "../models/Coverage";
import mapToRole, { Role, UserDetails } from "../models/UserDetails";
import CourseContent from "./CourseContent";
import { Link, useLocation } from "react-router-dom";
import { Button, Spinner } from "react-bootstrap";
import { UserContext } from "../App";
import UpdateCourse from "./UpdateCourse";

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

  const [participants, setParticipants] = useState<UserDetails[] | undefined>(
    undefined
  );

  const [loadingCoverage, setLoadingCoverage] = useState(false);
  const [loadingCoverageId, setLoadingCoverageId] = useState([]);

  useEffect(() => {
    getData();
  }, []);

  const getData = () => {
    axiosInstance
      .get(`https://localhost:7136/api/v1.0/subjects/${id}`)
      .then((result) => {
        setSubject(result.data.resource);
      })
      .catch((error) => {
        console.log(error);
      });
  };

  const handleGetParticipants = () => {
    axiosInstance
      .get(`https://localhost:7136/api/v1.0/subjects/${id}/users`)
      .then((result) => {
        result.data.resource.map((x) => {
          x.role = mapToRole(x.role);
        });

        setParticipants(result.data.resource);
      })
      .catch((error) => {
        console.log(error);
      });
  };

  const toggleParticipants = () => {
    setShouldShowParticipants(!shouldShowParticipants);

    handleGetParticipants();

        setSubject({ ...subject });

    }

    const handleChangeCourseVisibility = (course: Course) => {

        course.isVisibleForStudents = !course.isVisibleForStudents;
        axiosInstance
            .put(
                `https://localhost:7136/api/v1.0/Courses/${course.id}`,
                course
            )
            .then((result) => {

                setSubject({ ...subject });
            })
            .catch(() => {
                course.isVisibleForStudents = false;
            });
    }

    const handleGenerateAllCourseCoverages = () => {
        subject.coursesDetails.forEach((course: Course) => {
            setLoadingCoverageId((prevIds) => [...prevIds, course.id]);
            let coverageData: CoverageDetails;
        
            axiosInstance
            .post("http://localhost:5000/check-course", {
                firebase_link: course.contentUrl,
                description: course.description,
            })
            .then((result) => {
                console.log("In generating coverage");
                const { coverage, good_keywords, bad_keywords } = result.data;
    
                subject.coursesDetails.forEach((x: Course) => {
                    console.log("Course: ", x)
                    if (x.id === course.id) {
                        console.log("Found course");
                        x.coverage = {
                            id: course.coverage.id,
                            percentage: coverage,
                            goodWords: good_keywords,
                            badWords: bad_keywords,
                        };
    
                        coverageData = {
                            id: course.coverage.id,
                            percentage: coverage,
                            goodWords: good_keywords,
                            badWords: bad_keywords,
                            courseId: course.id,
                        };
                    }
                    });
    
                setSubject({ ...subject });
    
                console.log("Updating course in API");
                (!course.coverage ? axiosInstance
                    .post(
                        `https://localhost:7136/api/v1.0/Coverage`,
                        coverageData
                    ) : 
                    axiosInstance
                        .put(
                            `https://localhost:7136/api/v1.0/Coverage/${course.coverage.id}`,
                            coverageData
                        )
                    )
                .then((result) => {
                    setLoadingCoverageId((prevIds) =>
                    prevIds.filter((id) => id !== course.id)
                    );
                    console.log(course.coverage);
                    console.log("Coverage was generated!");
                })
                .catch((error) => {
                    console.log(error);
                });
            })
            .catch(() => {
                console.log(error);
            });
        });
      };
        

    const handleGenerateCourseCoverage = (course: Course) => {
        setLoadingCoverageId((prevIds) => [...prevIds, course.id]);
        let coverageData: CoverageDetails;
        let nextCourse: Course | undefined;
        const currentIndex = subject.coursesDetails.findIndex((x: Course) => x.id === course.id);

        if (currentIndex !== -1 && currentIndex < subject.coursesDetails.length - 1) {
            nextCourse = subject.coursesDetails[currentIndex + 1];
        }
        axiosInstance
        .post("http://localhost:5000/check-course-v2", {
            firebase_link: course.contentUrl,
            description: course.description,
            course2_link: nextCourse ? nextCourse.contentUrl : "",
        })
            .then((result) => {
                console.log("In generating coverage");
                const { coverage, good_keywords, bad_keywords } = result.data;

                subject.coursesDetails.forEach((x: Course) => {
                    if (x.id === course.id) {
                        console.log("Found course");
                        x.coverage = {
                            id: course.coverage.id,
                            percentage: coverage,
                            goodWords: good_keywords,
                            badWords: bad_keywords,
                        };

                        coverageData =
                        {
                            id: course.coverage.id,
                            percentage: coverage,
                            goodWords: good_keywords,
                            badWords: bad_keywords,
                            courseId: course.id
                        };
                    }
                });

                setSubject({ ...subject });

                console.log("Updating course in API");
                (!course.coverage ? axiosInstance
                .post(
                    `https://localhost:7136/api/v1.0/Coverage`,
                    coverageData
                ) : 
                axiosInstance
                    .put(
                        `https://localhost:7136/api/v1.0/Coverage/${course.coverage.id}`,
                        coverageData
                    )
                )
                .then((result) => {
                    setLoadingCoverageId((prevIds) => prevIds.filter((id) => id !== course.id));
                    console.log(course.coverage);
                    console.log("Coverage was generated!");
                })
                .catch((error) => {
                    console.log(error);
                });
            })
            .catch(() => {
                console.log(error);
            });

    };

    const handleModalUpdate = (response) => {
        const course = response.resource;
        subject.coursesDetails.map((x: Course) => {
            if (x.id === course.id) {
                x.name = course.name;
                x.description = course.description;
                x.contentUrl = course.contentUrl;
                x.coverage = course.coverage;
            }
        });

        setSubject({ ...subject });
    };

    if (!subject) {
        return <p>Loading...</p>;
    }

    if (subject.id === '') {
        return <p>No subject</p>
    }

  return (
    <div className="subject-wrapper">
      <div className="back-button">
        <Link
          to="/subjects"
          style={{
            textDecoration: "none",
          }}
        >
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
        {[Role.Admin.toString(), Role.Teacher.toString()].includes(
          user.role
        ) && (
          <Button className="add-sutdent btn btn-success">
            Add Participants
          </Button>
        )}
        <button
          className="show-students btn btn-success"
          onClick={() => toggleParticipants()}
        >
          {shouldShowParticipants ? "Hide Participants" : "Show Participants"}
        </button>
        <button
          className="show-students btn btn-success"
          onClick={() => handleGenerateAllCourseCoverages()}
        >Generate All Coverages
        </button>
      </div>
      <div className="accordion" id="accordionPanelsStayOpenExample">
        {subject.coursesDetails.filter((x: Course) => x.isVisibleForStudents)
          .length == 0 &&
        ![Role.Admin.toString(), Role.Teacher.toString()].includes(
          user.role
        ) ? (
          <div>There are no courses available yet.</div>
        ) : (
          subject.coursesDetails.map((course: Course, index) => {
            if (
              course.isVisibleForStudents ||
              [Role.Admin.toString(), Role.Teacher.toString()].includes(
                user.role
              )
            ) {
              return (
                <div key={index} className="accordion-item">
                  <h2
                    className="accordion-header"
                    id={`panelsStayOpen-heading-${index}`}
                  >
                    <button
                      className={`accordion-button ${
                        index != 0 ? "collapsed" : ""
                      }`}
                      type="button"
                      data-bs-toggle="collapse"
                      data-bs-target={`#panelsStayOpen-collapse-${index}`}
                      aria-expanded={index == 0} // Expand the first item by default
                      aria-controls={`panelsStayOpen-collapse-${index}`}
                    >
                      {loadingCoverage ||
                      loadingCoverageId.includes(course.id) ? (
                        <Spinner animation="border" role="status">
                          <span className="visually-hidden">Loading...</span>
                        </Spinner>
                      ) : course.coverage ? (
                        <div
                          className={`coverage ${
                            course.coverage.percentage < 50 ? "fail" : "success"
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
                    className={`accordion-collapse collapse ${
                      index === 0 ? "show" : ""
                    }`}
                    aria-labelledby={`panelsStayOpen-heading-${index}`}
                  >
                    <div className="accordion-body">
                      <div className="coverage-words">
                        {course.coverage &&
                          [
                            Role.Admin.toString(),
                            Role.Teacher.toString(),
                          ].includes(user.role) && (
                            <>
                              <strong>Relevant topics: </strong>
                              {[...course.coverage.goodWords.keys()].map(
                                (i) => (
                                  <span key={i} className="good-word">
                                    {course.coverage.goodWords[i]}
                                    {i < course.coverage.goodWords.length - 1 &&
                                      " "}
                                  </span>
                                )
                              )}
                              {[...course.coverage.badWords.keys()].map((i) => (
                                <span key={i} className="bad-word">
                                  {course.coverage.badWords[i]}
                                  {i < course.coverage.badWords.length - 1 &&
                                    " "}
                                </span>
                              ))}
                            </>
                          )}
                      </div>
                      {course.description}
                      <div className="course-actions">
                        <CourseContent
                          contentUrl={course.contentUrl}
                          name={course.name}
                        ></CourseContent>
                        {[
                          Role.Admin.toString(),
                          Role.Teacher.toString(),
                        ].includes(user.role) && (
                          <Button
                            onClick={() => handleChangeCourseVisibility(course)}
                          >
                            {course.isVisibleForStudents
                              ? "Hide Course from Students"
                              : "Make Visible for Students"}
                          </Button>
                        )}
                        {[
                          Role.Admin.toString(),
                          Role.Teacher.toString(),
                        ].includes(user.role) && (
                          <Button
                            onClick={() => handleGenerateCourseCoverage(course)}
                          >
                            {course.coverage == null
                              ? "Generate Coverage"
                              : "Regenerate Coverage"}
                          </Button>
                        )}
                        {[
                          Role.Admin.toString(),
                          Role.Teacher.toString(),
                        ].includes(user.role) && 
                        <UpdateCourse item={course} onModalUpdate={handleModalUpdate}></UpdateCourse>}
                      </div>
                    </div>
                  </div>
                </div>
              );
            }
          })
        )}
      </div>
      {participants ? (
        shouldShowParticipants ? (
          <div className="participants">
            {participants.map((student: UserDetails, index) => (
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
        ) : null
      ) : null}
    </div>
  );
};

export default SubjectDetails;