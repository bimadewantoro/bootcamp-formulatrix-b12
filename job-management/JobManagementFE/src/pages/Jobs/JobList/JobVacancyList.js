import React, { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import { Col, Input, Label, Row, Modal, ModalBody } from "reactstrap";
import axios from "axios";

const JobVacancyList = () => {
  const [modal, setModal] = useState(false);
  const openModal = () => setModal(!modal);

  const [jobVacancyList, setJobVacancyList] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchJobs = async () => {
      try {
        const controller = new AbortController();
        const timeoutId = setTimeout(() => controller.abort(), 10000);

        const response = await axios.get(
          "http://localhost:5247/api/jobs?activeOnly=true",
          {
            signal: controller.signal,
            headers: {
              Accept: "application/json",
              "Content-Type": "application/json",
            },
          }
        );

        clearTimeout(timeoutId);

        if (
          response.data &&
          Array.isArray(response.data) &&
          response.data.length > 0
        ) {
          const transformedJobs = transformApiData(response.data);
          setJobVacancyList(transformedJobs);
        } else {
          console.warn("API returned no job data");
        }
      } catch (error) {
        let errorMessage = "Failed to fetch job listings";

        if (axios.isAxiosError(error)) {
          if (error.code === "ERR_NETWORK") {
            errorMessage =
              "Network error - please check if the API server is running";
          } else if (error.response) {
            errorMessage = `API error: ${error.response.status} ${error.response.statusText}`;
          } else if (error.request) {
            errorMessage = "No response received from the server";
          }
        } else if (error.name === "AbortError") {
          errorMessage = "Request timeout - API took too long to respond";
        }

        console.error("Error fetching jobs:", error);
        setError(errorMessage);
      } finally {
        setLoading(false);
      }
    };

    fetchJobs();
  }, []);

  const transformApiData = (apiJobs) => {
    return apiJobs.map((job, index) => {
      const jobDate = new Date(job.createdAt);
      const now = new Date();
      const diffTime = Math.abs(now - jobDate);
      const diffDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24));

      let jobPostTime;
      if (diffDays < 1) {
        const diffHours = Math.ceil(diffTime / (1000 * 60 * 60));
        if (diffHours < 1) {
          const diffMinutes = Math.ceil(diffTime / (1000 * 60));
          jobPostTime = `${diffMinutes} min ago`;
        } else {
          jobPostTime = `${diffHours} hours ago`;
        }
      } else if (diffDays < 30) {
        jobPostTime = `${diffDays} days ago`;
      } else {
        const diffMonths = Math.ceil(diffDays / 30);
        jobPostTime = `${diffMonths} month${diffMonths > 1 ? "s" : ""} ago`;
      }

      const jobType = job.jobType?.toLowerCase() || "";
      const fullTime = jobType.includes("full");
      const partTime = jobType.includes("part");
      const freeLance = jobType.includes("free");
      const internship = jobType.includes("intern");

      const badges = [];

      if (!job.isActive) {
        badges.push({
          id: 1,
          badgeclassName: "bg-warning-subtle text-warning",
          badgeName: "Closed",
        });
      }

      return {
        id: job.id,
        jobTitle: job.title,
        departmentName: `${job.department} Department`,
        location: job.location,
        jobPostTime: jobPostTime,
        fullTime: fullTime,
        partTime: partTime,
        freeLance: freeLance,
        internship: internship,
        timing: job.jobType,
        addclassNameBookmark: 0,
        badges: badges,
        experience: "1 - 3 years",
      };
    });
  };

  return (
    <React.Fragment>
      <div>
        {loading ? (
          <div className="text-center py-5">
            <div className="spinner-border text-primary" role="status">
              <span className="visually-hidden">Loading...</span>
            </div>
          </div>
        ) : error ? (
          <div className="alert alert-danger" role="alert">
            {error}
          </div>
        ) : (
          jobVacancyList.map((jobVacancyListDetails, key) => (
            <div
              key={key}
              className={
                jobVacancyListDetails.addclassNameBookmark === true
                  ? "job-box bookmark-post card mt-4"
                  : "job-box card mt-5"
              }
            >
              <div className="bookmark-label text-center">
                <Link to="#" className="align-middle text-white">
                  <i className="mdi mdi-star"></i>
                </Link>
              </div>
              <div className="p-5">
                <Row className="align-items-center">
                  {/* Removed the company image column as requested */}

                  <Col md={4}>
                    <div className="mb-2 mb-md-0">
                      <h5 className="fs-18 mb-0">
                        <Link to="/jobdetails" className="text-dark">
                          {jobVacancyListDetails.jobTitle}
                        </Link>
                      </h5>
                      <p className="text-muted fs-14 mb-0">
                        {jobVacancyListDetails.departmentName}
                      </p>
                    </div>
                  </Col>

                  <Col md={3}>
                    <div className="d-flex mb-2">
                      <div className="flex-shrink-0">
                        <i className="mdi mdi-map-marker text-primary me-1"></i>
                      </div>
                      <p className="text-muted mb-0">
                        {jobVacancyListDetails.location}
                      </p>
                    </div>
                  </Col>

                  <Col md={2}>
                    <div className="d-flex mb-0">
                      <div className="flex-shrink-0">
                        <i className="uil uil-clock-three text-primary me-1"></i>
                      </div>
                      <p className="text-muted mb-0">
                        {" "}
                        {jobVacancyListDetails.jobPostTime}
                      </p>
                    </div>
                  </Col>

                  <Col md={3}>
                    <div>
                      <span
                        className={
                          jobVacancyListDetails.fullTime === true
                            ? "badge bg-success-subtle text-success fs-13 mt-1 mx-1"
                            : jobVacancyListDetails.partTime === true
                            ? "badge bg-danger-subtle text-danger fs-13 mt-1 mx-1"
                            : jobVacancyListDetails.freeLance === true
                            ? "badge bg-primary-subtle text-primary fs-13 mt-1 mx-1"
                            : jobVacancyListDetails.internship === true
                            ? "badge bg-info-subtle text-info mt-1"
                            : ""
                        }
                      >
                        {jobVacancyListDetails.timing}
                      </span>
                      {(jobVacancyListDetails.badges || []).map(
                        (badgeInner, key) => (
                          <span
                            className={`badge ${badgeInner.badgeclassName} fs-13 mt-1`}
                            key={key}
                          >
                            {badgeInner.badgeName}
                          </span>
                        )
                      )}
                    </div>
                  </Col>
                </Row>
              </div>
              <div className="p-3 bg-light">
                <Row className="justify-content-between">
                  <Col md={4}>
                    <div>
                      <p className="text-muted mb-0">
                        <span className="text-dark">Experience :</span>
                        {jobVacancyListDetails.experience}
                      </p>
                    </div>
                  </Col>
                  <Col lg={2} md={3}>
                    <div>
                      <Link
                        to="#applyNow"
                        onClick={openModal}
                        className="primary-link"
                      >
                        Apply Now{" "}
                        <i className="mdi mdi-chevron-double-right"></i>
                      </Link>
                    </div>
                  </Col>
                </Row>
              </div>
            </div>
          ))
        )}

        {/* Apply Now Modal */}
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

export default JobVacancyList;
