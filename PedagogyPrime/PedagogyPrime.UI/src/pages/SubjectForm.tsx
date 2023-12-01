import React, { useState } from "react";
import AppNavbar from "../components/AppNavbar";
import CourseForm from "../components/CourseForm";
import '../css/subjectForm.scss';
import { Button } from "react-bootstrap";

interface SubjectFormProps {
  subjectId?: string;
}

export interface Subject {
  id: string;
  name: string;
  period: string;
  numberOfCourses: number;
}

const SubjectForm: React.FC<SubjectFormProps> = ({ subjectId }) => {
  const [step, setStep] = useState(1);
  const [name, setName] = useState("");
  const [period, setPeriod] = useState("");
  const [noOfCourses, setNoOfCourses] = useState(0);
  const [loading, setLoading] = useState(false);

  const handleNext = async () => {
    if (step === 1) {
      if (!name || !period || noOfCourses <= 0) {
        alert("Please fill in all fields before proceeding.");
        return;
      } else {
        setStep(2);
      }
    }
  };

  const handleBack = () => {
    if (step > 1) {
      setStep(step - 1);
    }
  };

  return (
    <div>
      <AppNavbar />
      <div className="create-subject">
        {step > 1 && (
          
          <Button variant="link" type="button" onClick={handleBack} className="back-button">
            Back
          </Button>
        )}
        {step === 1 && (
          <form>
            <div className="mb-3">
              <label htmlFor="name" className="form-label">
                Subject Name:
              </label>
              <input
                type="text"
                id="name"
                className="form-control"
                value={name}
                onChange={(e) => setName(e.target.value)}
              />
            </div>
            <div className="mb-3">
              <label htmlFor="period" className="form-label">
                Period:
              </label>
              <input
                type="text"
                id="period"
                className="form-control"
                value={period}
                onChange={(e) => setPeriod(e.target.value)}
              />
            </div>
            <div className="mb-3">
              <label htmlFor="noOfCourses" className="form-label">
                Number of Courses:
              </label>
              <input
                type="number"
                id="noOfCourses"
                className="form-control"
                value={noOfCourses}
                onChange={(e) => setNoOfCourses(Number(e.target.value))}
              />
            </div>
            <button
              type="button"
              className="btn btn-primary"
              onClick={handleNext}
            >
              Next
            </button>
          </form>
        )}
        {step === 2 && (
          <CourseForm
            noOfCourses={noOfCourses}
            subjectId={subjectId}
            subjectName={name}
            subjectPeriod={period}
            handleNext={() => null}
          />
        )}
      </div>
    </div>
  );
};

export default SubjectForm;
