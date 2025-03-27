import React, { useState } from "react";
import { Modal, ModalBody, Input, Label, Card, CardBody } from "reactstrap";
import { Link } from "react-router-dom";

//Import Images
import jobImages2 from "../../../assets/images/featured-job/img-02.png";

const RightSideContent = () => {
  //Apply Now Model
  const [modal, setModal] = useState(false);
  const openModal = () => setModal(!modal);

  return (
    <React.Fragment>
      <div className="side-bar ms-lg-4">
        <Card className="job-overview">
          <CardBody className="p-4">
            <h6 className="fs-17">Job Overview</h6>
            <ul className="list-unstyled mt-4 mb-0">
              <li>
                <div className="d-flex mt-4">
                  <i className="uil uil-user icon bg-primary-subtle text-primary"></i>
                  <div className="ms-3">
                    <h6 className="fs-14 mb-2">Job Title</h6>
                    <p className="text-muted mb-0">Product Designer</p>
                  </div>
                </div>
              </li>
              <li>
                <div className="d-flex mt-4">
                  <i className="uil uil-star-half-alt icon bg-primary-subtle text-primary"></i>
                  <div className="ms-3">
                    <h6 className="fs-14 mb-2">Experience</h6>
                    <p className="text-muted mb-0"> 0-3 Years</p>
                  </div>
                </div>
              </li>
              <li>
                <div className="d-flex mt-4">
                  <i className="uil uil-location-point icon bg-primary-subtle text-primary"></i>
                  <div className="ms-3">
                    <h6 className="fs-14 mb-2">Location</h6>
                    <p className="text-muted mb-0"> New york</p>
                  </div>
                </div>
              </li>
              <li>
                <div className="d-flex mt-4">
                  <i className="uil uil-usd-circle icon bg-primary-subtle text-primary"></i>
                  <div className="ms-3">
                    <h6 className="fs-14 mb-2">Offered Salary</h6>
                    <p className="text-muted mb-0">$35k - $45k</p>
                  </div>
                </div>
              </li>
              <li>
                <div className="d-flex mt-4">
                  <i className="uil uil-graduation-cap icon bg-primary-subtle text-primary"></i>
                  <div className="ms-3">
                    <h6 className="fs-14 mb-2">Qualification</h6>
                    <p className="text-muted mb-0">Bachelor Degree</p>
                  </div>
                </div>
              </li>
              <li>
                <div className="d-flex mt-4">
                  <i className="uil uil-building icon bg-primary-subtle text-primary"></i>
                  <div className="ms-3">
                    <h6 className="fs-14 mb-2">Industry</h6>
                    <p className="text-muted mb-0">Private</p>
                  </div>
                </div>
              </li>
              <li>
                <div className="d-flex mt-4">
                  <i className="uil uil-history icon bg-primary-subtle text-primary"></i>
                  <div className="ms-3">
                    <h6 className="fs-14 mb-2">Date Posted</h6>
                    <p className="text-muted mb-0">Posted 2 hrs ago</p>
                  </div>
                </div>
              </li>
            </ul>
            <div className="mt-3">
              <Link
                to="#applyNow"
                onClick={openModal}
                className="btn btn-primary btn-hover w-100 mt-2"
              >
                Apply Now <i className="uil uil-arrow-right"></i>
              </Link>
              <Link
                to="/bookmarkjobs"
                className="btn btn-soft-warning btn-hover w-100 mt-2"
              >
                <i className="uil uil-bookmark"></i> Add Bookmark
              </Link>
            </div>
          </CardBody>
        </Card>

        <div
          className="modal fade"
          id="applyNow"
          tabIndex="-1"
          aria-labelledby="applyNow"
          aria-hidden="true"
        >
          <div className="modal-dialog modal-dialog-centered">
            <Modal isOpen={modal} toggle={openModal} centered>
              <ModalBody className="modal-body p-5">
                <div className="text-center mb-4">
                  <h5 className="modal-title" id="staticBackdropLabel">
                    Apply For This Job
                  </h5>
                </div>
                <div className="position-absolute end-0 top-0 p-3">
                  <button
                    type="button"
                    onClick={openModal}
                    className="btn-close"
                    data-bs-dismiss="modal"
                    aria-label="Close"
                  ></button>
                </div>
                <div className="mb-3">
                  <Label for="nameControlInput" className="form-label">
                    Name
                  </Label>
                  <Input
                    type="text"
                    className="form-control"
                    id="nameControlInput"
                    placeholder="Enter your name"
                  />
                </div>
                <div className="mb-3">
                  <Label for="emailControlInput2" className="form-label">
                    Email Address
                  </Label>
                  <Input
                    type="email"
                    className="form-control"
                    id="emailControlInput2"
                    placeholder="Enter your email"
                  />
                </div>
                <div className="mb-3">
                  <Label for="messageControlTextarea" className="form-label">
                    Message
                  </Label>
                  <textarea
                    className="form-control"
                    id="messageControlTextarea"
                    rows="4"
                    placeholder="Enter your message"
                  ></textarea>
                </div>
                <div className="mb-4">
                  <Label className="form-label" for="inputGroupFile01">
                    Resume Upload
                  </Label>
                  <Input
                    type="file"
                    className="form-control"
                    id="inputGroupFile01"
                  />
                </div>
                <button type="submit" className="btn btn-primary w-100">
                  Send Application
                </button>
              </ModalBody>
            </Modal>
          </div>
        </div>
      </div>
    </React.Fragment>
  );
};

export default RightSideContent;
