import { useEffect, useState } from "react";
import { getMyJobs } from "../../services/JobService";
import type { Job } from "../../types/Job";
import { useNavigate } from "react-router-dom";
export default function EmployerActiveJobsPage() {

    const [jobs, setJobs] = useState<Job[]>([]);

    const navigate = useNavigate();

    useEffect(() => {

        async function loadJobs() {

            try {

                const data = await getMyJobs();

                const activeJobs = data.filter((job: Job) => job.isActive);

                setJobs(activeJobs);

            } catch (error) {

                console.error(error);
            }
        }
        loadJobs();
    }, []);
    return (
        <div className="page-container">

            <div className="dashboard-header">

                <h1 className="dashboard-title">
                    Active Jobs
                </h1>

                <p className="dashboard-subtitle">
                    Jobs currently active and visible to candidates.
                </p>

            </div>

            {jobs.length === 0 ? (

                <div className="card">
                    No active jobs found.
                </div>

            ) : (

                <div className="jobs-grid">

                    {jobs.map((job) => (

                        <div
                            key={job.entityId}
                            className="card"
                        >
                            <h3>{job.title}</h3>

                            <p>
                                {job.description}
                            </p>

                            <p>
                                {job.city}
                            </p>
                            <div
                                style={{
                                    display: "flex",
                                    gap: "10px",
                                    marginTop: "10px"
                                }}
                            >
                                <button
                                    onClick={() =>
                                        navigate(
                                            `/jobs/edit/${job.entityId}`)
                                    }
                                >
                                    Edit
                                </button>

                                <button
                                    onClick={() =>
                                        navigate(
                                            `/jobapplicants/${job.entityId}`)
                                    }
                                >
                                    View Applicants
                                </button>
                            </div>
                        </div>
                    ))}

                </div>
            )}
        </div>
    );
}