
import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";

import type { Job } from "../../types/Job";
import { getJobById } from "../../services/JobService";

import { handleApply } from "../../utils/HelperFunctions";

export default function JobDetailsPage() {

    const { jobId } = useParams();

    const navigate = useNavigate();

    const [job, setJob] = useState<Job | null>(null);

    const [loading, setLoading] = useState(true);

    useEffect(() => {

        const loadJob = async () => {

            if (!jobId)
                return;

            try {

                const response = await getJobById(jobId);

                setJob(response);

            }
            finally {

                setLoading(false);

            }

        };

        loadJob();

    }, []);



    const formatLPA = (salary?: number) => {

        if (!salary)
            return "Not specified";

        const lpa = salary / 100000;

        return Number.isInteger(lpa)
            ? `₹${lpa} LPA`
            : `₹${lpa.toFixed(1)} LPA`;
    };

    if (loading)
        return <div className="page-container">Loading...</div>;

    if (!job)
        return <div className="page-container">Job not found.</div>;

    return (

        <div className="page-container">

            <div className="card">

                <div className="card-header">

                    <h2 className="card-title">

                        {job.title}

                    </h2>

                </div>

                <div className="job-location">

                    {job.city}, {job.state}

                </div>

                <div className="job-description">

                    {job.description}

                </div>

                <hr className="job-divider" />

                <div className="job-details">

                    <div>

                        <strong>Salary</strong>

                        <div>

                            {formatLPA(job.minSalary)}
                            {" - "}
                            {formatLPA(job.maxSalary)}

                        </div>

                    </div>

                    <div>

                        <strong>Experience</strong>

                        <div>

                            {job.experienceRequired}

                            {
                                job.maxExperienceRequired > 0 &&
                                job.maxExperienceRequired !== job.experienceRequired &&
                                ` - ${job.maxExperienceRequired}`
                            }

                            {" Years"}

                        </div>

                    </div>

                </div>

                <hr className="job-divider" />

                <div>

                    <strong>Required Skills</strong>

                    <div style={{ marginTop: "10px" }}>

                        {
                            job.requiredSkills
                                ?.split(",")
                                .map(skill => skill.trim())
                                .filter(skill => skill.length > 0)
                                .map((skill, index) => (

                                    <span
                                        key={index}
                                        className="skill-chip"
                                    >
                                        {skill}
                                    </span>

                                ))
                        }

                    </div>

                </div>

                <hr className="job-divider" />

                <div className="job-actions">

                    <button  className="primary-button"
                        style={{ width: "120px" }}
                        onClick={() => navigate(-1)}
                    >
                        Back
                    </button>

                    <button className="primary-button"
                        style={{ width: "120px" }}
                        onClick={() =>
                            handleApply(
                                job.entityId)
                        }
                    >
                        Apply
                    </button>

                </div>

            </div>

        </div>

    );
}