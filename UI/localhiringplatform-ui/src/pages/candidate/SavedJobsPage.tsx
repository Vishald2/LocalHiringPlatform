import { useEffect, useState } from "react";

import {getMySavedJobs, removeSavedJob} from "../../services/SavedJobService";

import type { Job } from "../../types/Job";

export default function SavedJobsPage() {

    const [jobs, setJobs] = useState<Job[]>([]);

    useEffect(() => {

        async function loadJobs() {

            const result = await getMySavedJobs();

            setJobs(result);
        }

        loadJobs();

    }, []);

    async function handleRemove(
        jobId: string) {

            /*REMOVE SAVED JOB FROM DATABASE */
        await removeSavedJob(jobId);

        /*REMOVE SAVED JOB FROM STATE WITHOUT MAKING A ROUND TRIP TO THE DATABASE */
        setJobs(jobs.filter(x => x.entityId !== jobId));
    }

    return (
        <div>

            <h2>
                Saved Jobs
            </h2>

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

                        <button
                            onClick={() =>
                                handleRemove(
                                    job.entityId)
                            }
                        >
                            Remove
                        </button>

                    </div>
                ))
            }

        </div>
    );
}