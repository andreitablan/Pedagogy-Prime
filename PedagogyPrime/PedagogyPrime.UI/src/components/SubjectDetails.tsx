import { useContext, useEffect, useState } from "react";
import axiosInstance from "../AxiosConfig";
import "../css/subjectDetails.scss";
import { Course } from "../models/Course";
import mapToRole, { Role, UserDetails } from "../models/UserDetails";
import { UserContext } from "../App";
import { Link } from "react-router-dom";
import { Button } from "react-bootstrap";
import CourseContent from "./CourseContent";


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
                result.data.resource.map(x => {
                    x.role = mapToRole(x.role);
                })

                setParticipants(result.data.resource);
            })
            .catch((error) => {
                console.log(error);
            });

    }

    const toggleParticipants = () => {
        setShouldShowParticipants(!shouldShowParticipants);

        handleGetParticipants();

        setSubject({...subject});
        
    }

    const handleChangeCourseVisibility = (course: Course) => {
       
        course.isVisibleForStudents = !course.isVisibleForStudents;
        axiosInstance
        .put(
            `https://localhost:7136/api/v1.0/Courses/${course.id}`,
            course
        )
        .then((result) => {
            subject.coursesDetails.forEach((x: Course) => {
                if(x.id === course.id){
                    x.isVisibleForStudents = course.isVisibleForStudents;
                }
            });

            setSubject({...subject});
        })
        .catch(() => {
            course.isVisibleForStudents = false;
        });
    }

    if (!subject) {
        return <p>Loading...</p>;
    }

    if(subject.id === ''){
        return <p>No subject</p>
    }

    return (
        <div className="subject-wrapper">
            <div className="back-button">
                <Link to="/subjects" style={{
                    textDecoration: "none",
                }}>
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
            { [Role.Admin.toString(), Role.Teacher.toString()].includes(user.role) && <Button className="add-sutdent btn btn-success">Add Participants</Button>}
                <button className="show-students btn btn-success" onClick={() => toggleParticipants()}>{shouldShowParticipants ? "Hide Participants" : "Show Participants"}</button>
            </div>
            <div className="accordion" id="accordionPanelsStayOpenExample">
                {
                    subject.coursesDetails.filter((x : Course)=> x.isVisibleForStudents).length == 0 && ![Role.Admin.toString(), Role.Teacher.toString()].includes(user.role) 
                    ?(<div>There are no courses available yet.</div>)
                    :subject.coursesDetails.map((course: Course, index) => {
                    
                        if(course.isVisibleForStudents || [Role.Admin.toString(), Role.Teacher.toString()].includes(user.role)){
                            return (
                                <div  key={index} className="accordion-item">
                                    <h2
                                        className="accordion-header"
                                        id={`panelsStayOpen-heading-${index}`}
                                    >
                                        <button
                                            className={`accordion-button ${index != 0 ? "collapsed" : ""}`}
                                            type="button"
                                            data-bs-toggle="collapse"
                                            data-bs-target={`#panelsStayOpen-collapse-${index}`}
                                            aria-expanded={index == 0} // Expand the first item by default
                                            aria-controls={`panelsStayOpen-collapse-${index}`}
                                        >
                                            {
                                                course.coverage ?
                                                (<div
                                                    className={`coverage ${course.coverage.percentage < 50 ? "fail" : "success"
                                                        }`}
                                                >
                                                    {course.coverage.percentage}%
                                                </div>
                                                )
                                                : 
                                                (
                                                    <div className="coverage fail" > 0% </div>
                                                )
                                            }
            
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
                                            {course.description}
                                            <div className="course-actions">
                                                <CourseContent contentUrl={course.contentUrl} name={course.name}></CourseContent>
                                                { [Role.Admin.toString(), Role.Teacher.toString()].includes(user.role) && <Button onClick={() => handleChangeCourseVisibility(course)} >{course.isVisibleForStudents ?  "Hide Course from Students" : "Make Visible for Students"}</Button>}
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            )
                        }
                    })
                }
            </div>
            {
                participants ? shouldShowParticipants ? (
                    <div className="participants">
                        {
                            participants.map((student: UserDetails, index) => (
                                <div key={index} className="card">
                                    <div className="card-header">
                                        {student.lastName} {student.firstName}
                                    </div>
                                    <div className="card-body">
                                        <p className="card-text">Email: {student.email}</p>
                                        <p className="card-text">Role: {student.role}</p>
                                    </div>
                                </div>
                            ))
                        }
                    </div>
                ) : null : null
            }

        </div >
    );
};

export default SubjectDetails;
