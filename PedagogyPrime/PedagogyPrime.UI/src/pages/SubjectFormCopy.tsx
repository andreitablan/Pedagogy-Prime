import React from "react";
import { useState, useEffect } from "react";
import AppNavbar from "../components/AppNavbar";
import CourseForm from "../components/CourseForm";
interface SubjectForumCopyProps {
  subjectId?: string; // Make subjectId optional
}

const SubjectForumCopy: React.FC<SubjectForumCopyProps> = ({ subjectId }) => {
  const [name, setName] = useState("");
  const [period, setPeriod] = useState("");
  const [noOfCourses, setNoOfCourses] = useState(0);

  useEffect(() => {
    if (subjectId) {
      // Fetch and set data based on subjectId when in edit mode
      // Your logic to fetch and set data based on subjectId
    }
  }, [subjectId]);

  return (
    <div>
      <AppNavbar />
      <CourseForm />
    </div>
  );
};

export default SubjectForumCopy;
