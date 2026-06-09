import { useEffect }
    from "react";

import { useState }
    from "react";

import { getJobs }
    from "../services/JobService";

import type { Job }
    from "../types/Job";

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

                    </div>

                ))
            }

        </div>
    );
}