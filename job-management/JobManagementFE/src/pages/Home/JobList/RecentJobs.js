import React, { useState, useEffect } from "react";
import { Col, Row, Modal, ModalBody, Input, Label } from "reactstrap";
import { Link } from "react-router-dom";
import axios from "axios";

const RecentJobs = () => {
  //Apply Now Model
  const [modal, setModal] = useState(false);
  const openModal = () => setModal(!modal);

  // State for API data
  const [jobs, setJobs] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchJobs = async () => {
      try {
        // Add timeout to prevent hanging requests
        const controller = new AbortController();
        const timeoutId = setTimeout(() => controller.abort(), 10000);

        const response = await axios.get(
          "http://localhost:5247/api/jobs?activeOnly=true&getLatestJob=true",
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
          setJobs(transformedJobs);
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

  // Transform API data to match component format
  const transformApiData = (apiJobs) => {
    return apiJobs.map((job, index) => {
      // Determine job type
      const jobTypeMap = {
        "Full-time": { fullTime: true, partTime: false, freelancer: false },
        "Part-Time": { fullTime: false, partTime: true, freelancer: false },
        Internship: { fullTime: false, partTime: false, freelancer: true },
        Freelance: { fullTime: false, partTime: false, freelancer: true },
      };

      const jobTypeInfo = jobTypeMap[job.jobType] || {
        fullTime: true,
        partTime: false,
        freelancer: false,
      };

      const formatToMillions = (value) => {
        return new Intl.NumberFormat("id-ID").format(value / 1000000);
      };

      const minSalary = formatToMillions(job.minSalary);
      const maxSalary = formatToMillions(job.maxSalary);
      const formattedSalary = `${minSalary}-${maxSalary} juta/bulan`;

      return {
        id: job.id,
        jobTitle: job.title,
        departmentName: `${job.department} Department`,
        location: job.location,
        salary: formattedSalary,
        fullTime: jobTypeInfo.fullTime,
        partTime: jobTypeInfo.partTime,
        freelancer: jobTypeInfo.freelancer,
        timing: job.jobType,
        catogary: "Recent Jobs",
        addclassNameBookmark: 0,
        experience: "1 - 2 years",
        description: job.description
          ? job.description.substring(0, 50) + "..."
          : null,
      };
    });
  };

  return (
    <React.Fragment>
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
        <>
          {jobs.map((recentJobDetails, key) => (
            <div
              key={key}
              className={
                recentJobDetails.addclassNameBookmark === false
                  ? "job-box bookmark-post card mt-4"
                  : "job-box card mt-4"
              }
            >
              <div className="bookmark-label text-center">
                <Link to="#" className="text-white align-middle">
                  <i className="mdi mdi-star"></i>
                </Link>
              </div>
              <div className="p-5">
                <Row className="align-items-center">
                  <Col md={3}>
                    <div className="mb-2 mb-md-0">
                      <h5 className="fs-18 mb-1">
                        <Link to="/jobdetails" className="text-dark">
                          {recentJobDetails.jobTitle}
                        </Link>
                      </h5>
                      <p className="text-muted fs-14 mb-0">
                        {recentJobDetails.departmentName}
                      </p>
                    </div>
                  </Col>

                  <Col md={3}>
                    <div className="d-flex mb-2">
                      <div className="flex-shrink-0">
                        <i className="mdi mdi-map-marker text-primary me-1"></i>
                      </div>
                      <p className="text-muted mb-0">
                        {recentJobDetails.location}
                      </p>
                    </div>
                  </Col>

                  <Col md={3}>
                    <div>
                      <p className="text-muted mb-2">
                        <span className="text-primary">IDR </span>
                        {recentJobDetails.salary}
                      </p>
                    </div>
                  </Col>

                  <Col md={3}>
                    <div>
                      <span
                        className={
                          recentJobDetails.fullTime === true
                            ? "badge bg-success-subtle text-success fs-13 mt-1 mx-1"
                            : recentJobDetails.partTime === true
                            ? "badge bg-danger-subtle text-danger fs-13 mt-1 mx-1"
                            : recentJobDetails.freelancer === true
                            ? "badge bg-primary-subtle text-primary fs-13 mt-1 mx-1"
                            : ""
                        }
                      >
                        {recentJobDetails.timing}
                      </span>

                      {(recentJobDetails.badges || []).map(
                        (badgeInner, key) => (
                          <span
                            className={
                              "badge " +
                              badgeInner.badgeclassName +
                              " fs-13 mt-1"
                            }
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
                <Row>
                  <Col md={4}>
                    <div>
                      <p className="text-muted mb-0">
                        <span className="text-dark">Experience :</span>{" "}
                        {recentJobDetails.experience}
                      </p>
                    </div>
                  </Col>

                  <Col lg={6} md={5}>
                    <div>
                      <p className="text-muted mb-0">
                        <span className="text-dark">
                          {recentJobDetails.description === null
                            ? ""
                            : "Description :"}
                        </span>
                        {recentJobDetails.description}{" "}
                      </p>
                    </div>
                  </Col>

                  <Col lg={2} md={3}>
                    <div className="text-start text-md-end">
                      <Link to="#" onClick={openModal} className="primary-link">
                        Apply Now{" "}
                        <i className="mdi mdi-chevron-double-right"></i>
                      </Link>
                    </div>
                  </Col>
                </Row>
              </div>
            </div>
          ))}
        </>
      )}

      <div className="text-center mt-4 pt-2">
        <Link to="/joblist" className="btn btn-primary">
          View More <i className="uil uil-arrow-right"></i>
        </Link>
      </div>

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
    </React.Fragment>
  );
};

export default RecentJobs;
