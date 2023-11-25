import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom"; // Import useNavigate instead of useHistory
import { ref, uploadBytes, getDownloadURL, listAll } from "firebase/storage";
import { storage } from "../firebase";
import { v4 } from "uuid";

const CourseForm = () => {
  const navigate = useNavigate(); // Use useNavigate instead of useHistory
  const [fileUpload, setFileUpload] = useState<File | null>(null);
  const [fileUrl, setFileUrl] = useState<string | null>(null);

  const filesListRef = ref(storage, "files/");

  const handleFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const file = event.target.files?.[0] || null;
    setFileUpload(file);
  };

  const uploadFile = () => {
    if (fileUpload == null) return;

    const fileRef = ref(storage, `files/${fileUpload.name + v4()}`);
    uploadBytes(fileRef, fileUpload).then((snapshot) => {
      getDownloadURL(snapshot.ref).then((url) => {
        setFileUrl(url);
        // Redirect to "/subjects" after successful upload
        navigate("/subjects");
      });
    });
  };

  useEffect(() => {
    setFileUrl(null); // Clear existing URL
    listAll(filesListRef).then((response) => {
      response.items.forEach((item) => {
        getDownloadURL(item).then((url) => {
          setFileUrl(url);
        });
      });
    });
  }, [filesListRef]);

  return (
    <div>
      <div className="container mt-5">
        <div className="row">
          <div className="col-md-6 offset-md-3">
            <h2 className="text-center mb-4">Course Form</h2>
            <form>
              <div className="mb-3">
                <label htmlFor="file" className="form-label">
                  Upload File:
                </label>
                <input
                  type="file"
                  className="form-control"
                  id="file"
                  onChange={handleFileChange}
                />
              </div>

              <div className="text-center">
                <button
                  type="button"
                  className="btn btn-primary"
                  onClick={uploadFile}
                >
                  Upload
                </button>
              </div>
            </form>
          </div>
        </div>
      </div>

      {fileUrl && (
        <div className="mt-4">
          <h3>Uploaded File URL:</h3>
          <div className="m-2">
            <strong>File:</strong>
            <a href={fileUrl} target="_blank" rel="noopener noreferrer">
              {fileUrl}
            </a>
          </div>
        </div>
      )}
    </div>
  );
};

export default CourseForm;
