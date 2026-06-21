import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";

import type { Job } from "../../types/Job";
import {getMyJobs, updateJob} from "../../services/JobService";
export default function ManageJobsPage() {
    const navigate =
        useNavigate();

    const [jobs, setJobs] = useState<Job[]>([]);

    useEffect(() => {
        async function loadJobs() {
            const result =
                await getMyJobs();

            setJobs(result);
        }
        loadJobs();
    }, []);



    async function toggleJobStatus(
        job: Job) {

        async function loadJobs() {
            const result =
                await getMyJobs();

            setJobs(result);
        }

        await updateJob({
            entityId:
                job.entityId,

            title:
                job.title,

            description:
                job.description,

            city:
                job.city,

            state:
                job.state,

            minSalary:
                job.minSalary,

            maxSalary:
                job.maxSalary,

            experienceRequired:
                job.experienceRequired,

            requiredSkills:
                job.requiredSkills,

            isActive:
                !job.isActive
        });

        await loadJobs();
    }

    return (

        <div className="page-container">

            <div className="dashboard-header">

                <h1 className="dashboard-title">
                    Manage Jobs
                </h1>

                <p className="dashboard-subtitle">
                    Manage all jobs posted by your company.
                </p>

            </div>

            {
                jobs.map(job => (

                    <div
                        key={job.entityId}
                        className="card"
                        style={{
                            marginBottom: "20px"
                        }}
                    >
                        <h3>
                            {job.title}
                        </h3>

                        <p>
                            {job.description}
                        </p>

                        <p>
                            Location:
                            {" "}
                            {job.city}
                            {", "}
                            {job.state}
                        </p>

                        <p>
                            Status:
                            {" "}
                            <strong>
                                {
                                    job.isActive
                                        ? "Active"
                                        : "Inactive"
                                }
                            </strong>
                        </p>

                        <p>
                            Applicants:
                            {" "}
                            {job.applicantCount}
                        </p>

                        <div
                            style={{
                                display: "flex",
                                gap: "10px",
                                flexWrap: "wrap"
                            }}
                        >
                            <button
                                className="primary-button"
                                style={{
                                    width: "auto"
                                }}
                                onClick={() =>
                                    navigate(
                                        `/jobs/edit/${job.entityId}`)
                                }
                            >
                                Edit
                            </button>

                            <button
                                className="primary-button"
                                style={{
                                    width: "auto"
                                }}
                                onClick={() =>
                                    navigate(`/ejobapplicants/${job.entityId}`)
                                }
                            >
                                View Applicants
                            </button>

                            <button
                                className="primary-button"
                                style={{
                                    width: "auto"
                                }}
                                onClick={() =>
                                    toggleJobStatus(job)
                                }
                            >
                                {
                                    job.isActive
                                        ? "Deactivate"
                                        : "Activate"
                                }
                            </button>

                        </div>

                    </div>

                ))
            }

        </div>
    );
}