import React from "react";
import { useState, useEffect } from "react";
import AppNavbar from "../components/AppNavbar";

interface SubjectFormProps {
  subjectId?: string; // Make subjectId optional
}

const SubjectForm: React.FC<SubjectFormProps> = ({ subjectId }) => {
  const [name, setName] = useState("");
  const [period, setPeriod] = useState("");
  const [noOfCourses, setNoOfCourses] = useState(0);

  useEffect(() => {
    if (subjectId) {
      // Fetch and set data based on subjectId when in edit mode
      // Your logic to fetch and set data based on subjectId
    }
  }, [subjectId]);

  const handleSubmit = (event: React.FormEvent) => {
    event.preventDefault();

    // Your logic to handle form submission
    if (subjectId) {
      // Handle editing an existing subject
    } else {
      // Handle creating a new subject
    }
  };

  return (
    <div>
      <AppNavbar />
      <form onSubmit={handleSubmit}>
        <label>Name:</label>
        <input
          type="text"
          value={name}
          onChange={(e) => setName(e.target.value)}
        />

        <label>Period:</label>
        <input
          type="text"
          value={period}
          onChange={(e) => setPeriod(e.target.value)}
        />

        <label>No of Courses:</label>
        <input
          type="number"
          value={noOfCourses}
          onChange={(e) => setNoOfCourses(Number(e.target.value))}
        />

        {/* Other form fields... */}

        <button type="submit">Submit</button>
      </form>
    </div>
  );
};

export default SubjectForm;
