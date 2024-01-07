import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { ref, uploadBytes, getDownloadURL } from "firebase/storage";
import { storage } from "../firebase";
import { v4 } from "uuid";
import axiosInstance from "../AxiosConfig";

interface CourseFormProps {
  noOfCourses: number;
  subjectName: string;
  subjectPeriod: string;
  setLoading: React.Dispatch<React.SetStateAction<boolean>>;
}

const CourseForm: React.FC<CourseFormProps> = ({
  noOfCourses,
  subjectName,
  subjectPeriod,
  setLoading,
}) => {
  const navigate = useNavigate();
  const [fileUploads, setFileUploads] = useState<File[]>(Array(noOfCourses).fill(null));
  const [courseDescriptions, setCourseDescriptions] = useState<string[]>(Array(noOfCourses).fill(''));

  const handleFileChange = (event: React.ChangeEvent<HTMLInputElement>, courseIndex: number) => {
    const file = event.target.files?.[0] || null;
    if (file) {
      setFileUploads((prevFileUploads) => {
        const newFileUploads = [...prevFileUploads];
        newFileUploads[courseIndex] = file;
        return newFileUploads;
      });
    }
  };

  const handleDescriptionChange = (event: React.ChangeEvent<HTMLInputElement>, courseIndex: number) => {
    const description = event.target.value;
    setCourseDescriptions((prevDescriptions) => {
      const newCourseDescriptions = [...prevDescriptions];
      newCourseDescriptions[courseIndex] = description;
      return newCourseDescriptions;
    });
  };

  const uploadFiles = async () => {
    setLoading(true);
    try {
      const subjectUrl = "https://localhost:7136/api/v1/Subjects";
      const subjectData = {
        name: subjectName,
        period: subjectPeriod,
        noOfCourses: noOfCourses,
      };

      const subjectResult = await axiosInstance.post(subjectUrl, subjectData);

      if (subjectResult.status === 201) {
        const subjectId = subjectResult.data.resource;

        const uploadedFileUrls: string[] = [];

        await Promise.all(
          fileUploads.map(async (file) => {
            if (!file) return;

            const fileRef = ref(storage, `files/${file.name + v4()}`);
            const snapshot = await uploadBytes(fileRef, file);
            const url = await getDownloadURL(snapshot.ref);
            uploadedFileUrls.push(url);
          })
        );

        const courseUrl = "https://localhost:7136/api/v1/Courses";

        await Promise.all(
          uploadedFileUrls.map(async (url, index) => {
            const description = courseDescriptions[index] || "";
            const courseData = {
              name: `Course ${index + 1}`,
              description,
              coverage: null,
              contentUrl: url,
              subjectId,
            };

            const result = await axiosInstance.post(courseUrl, courseData);

            if (result.status === 201) {
              console.log("Course created successfully:", result.data);
            } else {
              console.error("Error creating course:", result.data);
            }
          })
        );

        console.log("Course data saved successfully");
        navigate("/subjects");
      } else {
        console.error("Error creating subject:", subjectResult.data);
        alert("Error uploading files");
        navigate("/subjects");
      }
    } catch (error) {
      console.error("Error uploading files:", error);
      alert("Error uploading files");
      navigate("/subjects");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="create-subject">
      <h2 className="mb-4">Course Form</h2>
      {[...Array(noOfCourses)].map((_, index) => (
        <div key={index} className="mb-3">
          <label htmlFor={`file-${index}`} className="form-label">{`Course ${index + 1}`}</label>
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
      <button
        type="button"
        className="btn btn-primary"
        onClick={uploadFiles}
        disabled={fileUploads.some(file => !file)}
        style={{ width: '100px' }}
      >
        Upload
      </button>
    </div>
  );
};

export default CourseForm;
