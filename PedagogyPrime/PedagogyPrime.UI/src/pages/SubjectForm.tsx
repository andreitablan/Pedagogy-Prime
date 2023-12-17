import React, { useState } from "react";
import AppNavbar from "../components/AppNavbar";
import CourseForm from "../components/CourseForm";
import '../css/subjectForm.scss';
import { Button } from "react-bootstrap";
import { useNavigate } from "react-router";

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
  const navigate = useNavigate();

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

  const handleCancel = () => {
    navigate('/subjects');
  }

  return (
    <div>
      <AppNavbar />
      {step === 1 && (
        <form className="create-subject">
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
            style={{
              width: '100px',
              marginTop: '15px'
            }}
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
          loading={loading}
          setLoading={setLoading}
        />
      )}
      {step > 1 && !loading && (
        <div className="create-subject">
          <button type="button" className="btn btn-primary" onClick={handleBack} style={{
            width: '100px',
            marginTop: '15px'
          }}>
            Back
          </button>
        </div>
      )}

      <div className="create-subject">
        <Button type="button" className="btn btn-primary" onClick={handleCancel} style={{
          width: '100px',
          marginTop: '15px'
        }}>
          Cancel
        </Button>
      </div>
    </div>
  );
};

export default SubjectForm;
