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
        <div>
            <button type="button" className="btn btn-success" onClick={handleShow}>View Content</button>

            <Modal className="something" show={show} onHide={handleClose} centered>
                <Modal.Header closeButton>
                    <Modal.Title>{name}</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <iframe className="content" src="http://www.africau.edu/images/default/sample.pdf" ></iframe>

                </Modal.Body>
            </Modal>
        </div>
    );

}

export default CourseContent;