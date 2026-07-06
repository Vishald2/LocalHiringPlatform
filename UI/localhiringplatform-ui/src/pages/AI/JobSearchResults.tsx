import type { JobSearchResultModel } from "../../types/AI/JobSearchResultModel";


interface Props {
    jobs: JobSearchResultModel[];
}

export default function JobSearchResults({ jobs }: Props) {

    console.log("jobs");
    console.log(jobs);

    if (jobs.length === 0) {
        return (
            <div>No matching jobs found.</div>
        );
    }

    return (

        <div>

            {
                jobs.map((item) => (
                    
                    <div
                        key={item.job.entityId}
                        className="card"
                        style={{ marginBottom: "15px" }}
                    >
                        <h4>{item.job.title}</h4>

                        <div>
                            <strong>Location:</strong> {item.job.city}, {item.job.state}
                        </div>

                        <div>
                            <strong>Salary:</strong> ₹{item.job.minSalary} - ₹{item.job.maxSalary}
                        </div>

                        <div>
                            <strong>Experience:</strong> {item.job.experienceRequired} Years
                        </div>

                        <div>
                            <strong>Skills:</strong> {item.job.requiredSkills}
                        </div>
                    </div>

                ))
            }

        </div>
    );
}