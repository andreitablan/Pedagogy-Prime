import { useEffect, useState } from "react";
import { Course } from "./CrudCourses";
import axiosInstance from "../AxiosConfig";
import '../css/subjectDetails.scss';

export interface SubjectDetails {
    id: string;
    name: string;
    period: string;
    noOfCourses: number;
  }

export interface Subject extends SubjectDetails {
    coursesDetails: Course[];
}

const SubjectDetails = ({id}) => {
    const [subject, setSubject] = useState({
        id: "",
        name: "Mama",
        period: "",
        noOfCourses: 0,
        coursesDetails: []
    });


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

    if (!subject) {
        return <p>Loading...</p>;
    }

    return (<div className="subject-wrapper">
        <div className="header">
            <div className="left">
                <div className="name">
                    {subject.name}
                </div>
                <div>-    {subject.noOfCourses} courses</div>
            </div>
            <div className="right">{subject.period}</div>
        </div>
        <div className="accordion" id="accordionPanelsStayOpenExample">
        {
            subject.coursesDetails.map((course: Course, index) => (
                <div key={index} className="accordion-item">
                    <h2 className="accordion-header" id={`panelsStayOpen-heading-${index}`}>
                    <button
                        className={`accordion-button ${index !=0 ? 'collapsed':''}`}
                        type="button"
                        data-bs-toggle="collapse"
                        data-bs-target={`#panelsStayOpen-collapse-${index}`}
                        aria-expanded={index == 0} // Expand the first item by default
                        aria-controls={`panelsStayOpen-collapse-${index}`}
                        >
                        <div className={`coverage ${course.coverage < 50 ? 'fail': 'success'}`}>{course.coverage}%</div>

                        <div className="course-name">Course {index + 1}: {course.name}</div>
                    </button>
                    </h2>
                    <div
                        id={`panelsStayOpen-collapse-${index}`}
                        className={`accordion-collapse collapse ${index === 0 ? 'show' : ''}`}
                        aria-labelledby={`panelsStayOpen-heading-${index}`}
                    >
                        <div className="accordion-body">
                            {course.description}
                            <button className="btn btn-success">View Content</button>

                        </div>
                    </div>

                </div>
            ))
        }
            
        </div>
    </div>);
}

export default SubjectDetails;