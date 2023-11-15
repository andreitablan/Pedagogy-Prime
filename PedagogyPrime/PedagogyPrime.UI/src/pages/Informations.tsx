import React from "react";
import AppNavbar from "../components/AppNavbar";
import { Card, Container, Row, Col } from "react-bootstrap";
import CarouselAuthors from "../components/CarouselAuthors";
import ReactMarkdown from "react-markdown";
const readmeContent = `# TPACK-APP
  The architecture of an online tool for the analysis of academic curricular materials used in Humanities & Social Sciences in the context of the TPACK competency model.
  This project is done for [Advanced Software Engineering Techniques](https://profs.info.uaic.ro/~adiftene/Scoala/2024/ASET/index.htm?fbclid=IwAR10FNcD2G9hsoqALrrjuGcjkoKXhdoUwgk-fGPCgT3QaS9hAjPy35rcDas) course at the [Faculty of Computer Science Iasi](https://www.info.uaic.ro/).
  - [Trello Board](https://trello.com/b/HcLIIuT6)
  - [Tasks](https://docs.google.com/document/d/1bJmPpIYV2hopaFNLbNVE2fENTN-3CqJLhOjUI7_ayYs/edit#heading=h.98ibuv5xh786)

  Naming convention for branches is **/feature/lab[n]-v[m]/name_person**, where **n** is the number of the laboratory, **m** is the version of the branch (this helps to create multiple branches for separate purposes). 

  ## Laboratory 6
  - Security on frontend and backend
  ## Laboratory 5
  - [Iteration 2](https://drive.google.com/drive/folders/1Ws48RU9TueaKKtXjONbFM_QL16Of_lxD)
  - [BPMN](https://drive.google.com/drive/folders/1delZzB24iUtgdn6AyC3iCBKtIQ17oH-Q)
  ## Laboratory 4
  - [Iteration 1](https://drive.google.com/drive/folders/1Ws48RU9TueaKKtXjONbFM_QL16Of_lxD)
  - Modeling, Design Patterns found in Documentation, Code
  ## Laboratory 3
  - [Requirement analysis](https://docs.google.com/document/d/1lAsE5mVDQusDkmSFsGU7FP6S6FCjtHQH_reb62fKUa0/edit?usp=sharing)
  - [UML Diagrams](https://drive.google.com/drive/folders/1an3M8JxSFjJzNQUVav8LHmsHyzchS7dm?usp=drive_link)
  ## Laboratory 2
  - [Tasks](https://docs.google.com/document/d/1bJmPpIYV2hopaFNLbNVE2fENTN-3CqJLhOjUI7_ayYs/edit#heading=h.98ibuv5xh786)
  - [State of the art](https://docs.google.com/document/d/1mEsqO5sSPORfZqgE9qFCyTFXUMeDh0L_j7NiWWXlLAA/edit?usp=sharing)
  - [Trello Board](https://trello.com/b/HcLIIuT6)
  #### Relevant links:
  - https://core.ac.uk/download/pdf/230028433.pdf
  - https://iopscience.iop.org/article/10.1088/1742-6596/1387/1/012035/pdf 
  - https://gheorghemariana.wordpress.com/documente-curriculare/
  - https://trello.com/b/HcLIIuT6
  ## Research Coordinator
  ###### [Iftene Adrian](https://profs.info.uaic.ro/~adiftene/)`;
const Informations: React.FC = () => {
  return (
    <div>
      <AppNavbar />

      <Container fluid style={{ height: "100vh", overflowY: "auto" }}>
        {/* Left Card with Text Content */}
        <Row>
          <Col md={8} style={{ padding: "20px" }}>
            <Card
              style={{
                minHeight: "100%",
                boxShadow: "4px 4px 5px rgba(0, 0, 0, 0.5)",
              }}
            >
              <Card.Body>
                <Card.Title>About Our Application</Card.Title>
                <Card.Text>
                  <ReactMarkdown>{readmeContent}</ReactMarkdown>
                </Card.Text>
                {/* Add more Card.Text or custom content as needed */}
              </Card.Body>
            </Card>
          </Col>

          {/* Right Card with Carousel */}
          <Col md={4} style={{ padding: "20px" }}>
            <Card
              style={{
                minHeight: "100%",
                boxShadow: "4px 4px 5px rgba(0, 0, 0, 0.5)",
              }}
            >
              <Card.Body>
                <Card.Title>Authors</Card.Title>
                <CarouselAuthors />
              </Card.Body>
            </Card>
          </Col>
        </Row>
      </Container>
    </div>
  );
};

export default Informations;
