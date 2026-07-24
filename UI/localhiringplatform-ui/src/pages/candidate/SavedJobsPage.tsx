import { useEffect, useState } from "react";

import {getMySavedJobs, removeSavedJob} from "../../services/SavedJobService";

import type { Job } from "../../types/Job";
import { useNavigate } from "react-router-dom";

export default function SavedJobsPage() {

    const navigate = useNavigate();

    const [jobs, setJobs] = useState<Job[]>([]);

    useEffect(() => {

        async function loadJobs() {

            const result = await getMySavedJobs();

            setJobs(result);
        }

        loadJobs();

    }, []);

    const handleViewDetails = (jobId: string) => {

        navigate(`/jobdetails/${jobId}`);

    };

    async function handleRemove(
        jobId: string) {

            /*REMOVE SAVED JOB FROM DATABASE */
        await removeSavedJob(jobId);

        /*REMOVE SAVED JOB FROM STATE WITHOUT MAKING A ROUND TRIP TO THE DATABASE */
        setJobs(jobs.filter(x => x.entityId !== jobId));
    }

    return (
        <div className="page-container">

            <h4>
                Saved Jobs
            </h4>

            {
                jobs.map(job => (

                    <div
                        key={
                            job.entityId
                        }
                        className="card"
                    >

                        <h3>
                            {job.title}
                        </h3>

                        <p>
                            {job.city},
                            {" "}
                            {job.state}
                        </p>

                        <button className="secondary-button"
                            onClick={() =>
                                handleRemove(
                                    job.entityId)
                            }
                            style={{
                                 width: "120px"
                            }}
                        >
                            Remove
                        </button>

                        <button className="secondary-button"
                            onClick={() =>
                                handleViewDetails(
                                    job.entityId)
                            }
                            style={{
                                width: "120px"
                            }}
                        >
                            View Details
                        </button>

                    </div>
                ))
            }

        </div>
    );
}