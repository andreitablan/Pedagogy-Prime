import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { ref, uploadBytes, getDownloadURL } from "firebase/storage";
import { storage } from "../firebase";
import { v4 } from "uuid";
import axiosInstance from "../AxiosConfig";

interface CourseFormProps {
  noOfCourses: number;
  subjectId?: string;
  subjectName?: string;
  subjectPeriod?: string;
  handleNext: () => React.ReactNode | null;
}

const CourseForm: React.FC<CourseFormProps> = ({
  noOfCourses,
  subjectId,
  subjectName,
  subjectPeriod,
  handleNext,
}) => {
  const navigate = useNavigate();
  const [fileUploads, setFileUploads] = useState<File[]>([]);
  const [fileUrls, setFileUrls] = useState<string[]>([]);
  const [courseDescriptions, setCourseDescriptions] = useState<string[]>([]);

  const handleFileChange = (
    event: React.ChangeEvent<HTMLInputElement>,
    courseIndex: number
  ) => {
    const file = event.target.files?.[0] || null;
    if (file) {
      const newFileUploads = [...fileUploads];
      newFileUploads[courseIndex] = file;
      setFileUploads(newFileUploads);
    }
  };

  const handleDescriptionChange = (
    event: React.ChangeEvent<HTMLInputElement>,
    courseIndex: number
  ) => {
    const description = event.target.value;
    const newCourseDescriptions = [...courseDescriptions];
    newCourseDescriptions[courseIndex] = description;
    setCourseDescriptions(newCourseDescriptions);
  };

  const uploadFiles = async () => {
    try {
      const url = "https://localhost:7136/api/v1/Subjects";

      const data = {
        name: subjectName,
        period: subjectPeriod,
        noOfCourses: noOfCourses,
      };

      const result = await axiosInstance.post(url, data);

      if (result.status === 201) {
        subjectId = result.data.resource;
      }
    } catch (error) {
      console.error("Error creating subject:", error);
      alert("Error uploading files");
      navigate("/subjects");
    }
    try {
      const uploadedFileUrls: string[] = [];

      await Promise.all(
        fileUploads.map(async (file) => {
          const fileRef = ref(storage, `files/${file.name + v4()}`);
          const snapshot = await uploadBytes(fileRef, file);
          const url = await getDownloadURL(snapshot.ref);
          uploadedFileUrls.push(url);
        })
      );

      const apiUrl = "https://localhost:7136/api/v1/Courses";

      // Sequentially make Axios requests
      for (let index = 0; index < uploadedFileUrls.length; index++) {
        const url = uploadedFileUrls[index];
        const description = courseDescriptions[index] || "";

        const courseData = {
          name: `Course ${index + 1}`,
          description,
          coverage: 0,
          contentUrl: url,
          subjectId,
        };
        // Await the Axios request
        const result = await axiosInstance.post(apiUrl, courseData);

        if (result.status === 201) {
          console.log("Course created successfully:", result.data);
        } else {
          console.error("Error creating course:", result.data);
        }
      }

      console.log("Course data saved successfully");
      navigate("/subjects");
    } catch (error) {
      console.error("Error uploading files:", error);
      alert("Error uploading files");
      navigate("/subjects");
    }
  };

  return (
    <div>
      <h2 className="mb-4">Course Form</h2>
      {[...Array(noOfCourses)].map((_, index) => (
        <div key={index} className="mb-3">
          <label htmlFor={`file-${index}`} className="form-label">
            {`Course ${index + 1}`}
          </label>
          <input
            type="file"
            id={`file-${index}`}
            className="form-control"
            onChange={(e) => handleFileChange(e, index)}
          />
          <input
            type="text"
            placeholder="Course Description"
            className="form-control mt-2"
            onChange={(e) => handleDescriptionChange(e, index)}
          />
        </div>
      ))}
      <button type="button" className="btn btn-primary" onClick={uploadFiles}>
        Upload
      </button>
    </div>
  );
};

export default CourseForm;
