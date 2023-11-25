import { useEffect, useState } from "react";
import axiosInstance from "../AxiosConfig";
import "../css/subjectDetails.scss";
import { Course } from "../models/Course";
import mapToRole, { Role, UserDetails } from "../models/UserDetails";


const SubjectDetails = ({ id }) => {
    const [subject, setSubject] = useState({
        id: "",
        name: "Mama",
        period: "",
        noOfCourses: 0,
        coursesDetails: [],
    });

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



    const getParticipants = () => {
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

        getParticipants();

    }

    if (!subject) {
        return <p>Loading...</p>;
    }

    return (
        <div className="subject-wrapper">
            <div className="header">
                <div className="left">
                    <div className="name">{subject.name}</div>
                    <div>- {subject.noOfCourses} courses</div>
                </div>
                <div className="right">{subject.period}</div>
            </div>
            <div className="actions">
                <button className="add-sutdent btn btn-success">Add Participants</button>
                <button className="show-students btn btn-success" onClick={() => toggleParticipants()}>{shouldShowParticipants ? "Hide Participants" : "Show Participants"}</button>
            </div>
            <div className="accordion" id="accordionPanelsStayOpenExample">
                {subject.coursesDetails.map((course: Course, index) => (
                    <div key={index} className="accordion-item">
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
                                <div
                                    className={`coverage ${course.coverage < 50 ? "fail" : "success"
                                        }`}
                                >
                                    {course.coverage}%
                                </div>

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
                                <button className="btn btn-success">View Content</button>
                            </div>
                        </div>
                    </div>
                ))}
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
