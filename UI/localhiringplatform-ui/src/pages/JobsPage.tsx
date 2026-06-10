import { useEffect }
    from "react";

import { useState }
    from "react";

import { getJobs }
    from "../services/JobService";

import { applyToJob }
    from "../services/ApplyToJobService";

import type { Job }
    from "../types/Job";

async function handleApply(
    jobId: string) {
    try {
        await applyToJob({
            jobId
        });

        alert(
            "Application submitted");
    }
    catch {
        alert(
            "Already applied");
    }
}
export default function JobList() {
    const [jobs, setJobs] =  useState<Job[]>([]);

    useEffect(() => {

        async function loadJobs() {
            const result =
                await getJobs();

            setJobs(result);

        }
        loadJobs();
    }, []);

    return (
        <div>

            <h2>
                Available Jobs
            </h2>

            {
                jobs.map(job => (

                    <div
                        key={
                            job.entityId
                        }
                        className="card">

                        <h3>
                            {job.title}
                        </h3>

                        <p>
                            {job.city},
                            {job.state}
                        </p>

                        <p>
                            Experience:
                            {" "}
                            {job.experienceRequired}
                            {" "}
                            Years
                        </p>

                        <p>
                            Salary:
                            {" "}
                            {job.minSalary}
                            {" - "}
                            {job.maxSalary}
                        </p>

                        <p>
                        <button
                            onClick={() =>
                                handleApply(
                                    job.entityId)
                            }
                        >
                            Apply
                        </button>
                        </p>
                    </div>

                ))
            }

        </div>
    );
}