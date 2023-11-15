import React from "react";
import { Carousel } from "react-bootstrap";
import { Link } from "react-router-dom";
import FVImage from "../assets/images/FV.jpg";
import ORImage from "../assets/images/RO.jpg";
import SRImage from "../assets/images/SR.jpg";
import TAImage from "../assets/images/TA.jpg";

const CarouselAuthors = () => {
  return (
    <Carousel>
      <Carousel.Item>
        <img className="d-block w-100" src={FVImage} alt="First slide" />
        <Carousel.Caption>
          <h5>Fanaru Victor</h5>
          <p>
            <Link to="https://github.com/FanaruVictor">GitHub</Link>
          </p>
        </Carousel.Caption>
      </Carousel.Item>
      <Carousel.Item>
        <img className="d-block w-100" src={ORImage} alt="Second slide" />
        <Carousel.Caption>
          <h5>Opariuc Rares</h5>
          <p>
            <Link to="https://github.com/OpariucRares">GitHub</Link>
          </p>
        </Carousel.Caption>
      </Carousel.Item>
      <Carousel.Item>
        <img className="d-block w-100" src={SRImage} alt="Third slide" />
        <Carousel.Caption>
          <h5>Savin Rares</h5>
          <p>
            <Link to="https://github.com/SavRares">GitHub</Link>
          </p>
        </Carousel.Caption>
      </Carousel.Item>
      <Carousel.Item>
        <img className="d-block w-100" src={TAImage} alt="Third slide" />
        <Carousel.Caption>
          <h5>Tablan Andrei</h5>
          <p>
            <Link to="https://github.com/andreitablan">GitHub</Link>
          </p>
        </Carousel.Caption>
      </Carousel.Item>
    </Carousel>
  );
};

export default CarouselAuthors;
