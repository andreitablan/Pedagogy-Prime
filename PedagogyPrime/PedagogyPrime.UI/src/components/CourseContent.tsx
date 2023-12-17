import { useState } from "react";
import { Modal } from "react-bootstrap";
import "../css/coursecontent.scss";

const CourseContent = ({ contentUrl, name }) => {

    const [show, setShow] = useState(false);

    const handleClose = () => {
        setShow(false);
    }

    const handleShow = () => {
        setShow(true);
    }

    return (
        <div className="view-content-component">
            <button type="button" className="btn btn-success" onClick={handleShow}>View Content</button>
            <div className="course-content">
                <Modal show={show} onHide={handleClose} centered>
                    <Modal.Header closeButton>
                        <Modal.Title>{name}</Modal.Title>
                    </Modal.Header>
                    <Modal.Body>
                        <iframe className="content" src={contentUrl} ></iframe>
                    </Modal.Body>
                </Modal>
            </div>
        </div>
    );

}

export default CourseContent;