import type { JobSearchResultModel } from "../../types/AI/JobSearchResultModel";
import { saveJob } from "../../services/SavedJobService";
import { getErrorMessage } from "../../utils/errorHelper";

interface Props {
    jobs: JobSearchResultModel[];
}

async function handleSave(
    jobId: string) {
    try {

        await saveJob(jobId);

        alert("Job saved");
    }
    catch (error) {
        alert(
            getErrorMessage(error));
    }
}

export default function JobSearchResults({ jobs }: Props) {

    if (jobs.length === 0) {
        return (
            <div>No matching jobs found.</div>
        );
    }

    const formatLPA = (salary?: number) => {

        if (!salary)
            return "Not specified";

        const lpa = salary / 100000;

        if (Number.isInteger(lpa))
            return `₹${lpa} LPA`;

        return `₹${lpa.toFixed(1)} LPA`;
    };

    return (

        <div>

            {
                jobs.map((item, index) => (

                    <div
                        key={item.job.entityId}
                        className={`card ${index === 0 ? "card-selected" : ""}`}
                    >

                        <div className="card-header">

                            <h3 className="card-title">
                                {item.job.title}
                            </h3>

                        </div>

                        <div className="job-location">
                            {item.job.city}, {item.job.state}
                        </div>

                        <div className="job-description">
                            {item.job.description}
                        </div>

                        <hr className="job-divider" />

                        <div className="job-details">

                            <div>
                                <strong>Salary</strong>
                                <div>
                                    {formatLPA(item.job.minSalary)}
                                    {" - "}
                                    {formatLPA(item.job.maxSalary)}
                                </div>
                            </div>

                            <div>
                                <strong>Experience</strong>
                                <div>
                                    {item.job.experienceRequired}

                                    {
                                        item.job.maxExperienceRequired > 0 &&
                                        item.job.maxExperienceRequired !== item.job.experienceRequired &&
                                        ` - ${item.job.maxExperienceRequired}`
                                    }

                                    {" Years"}
                                </div>
                            </div>

                            <div>
                                <strong>Skills</strong>

                                <div className="job-skills">

                                    {
                                        item.job.requiredSkills
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
                                <div>
                                    <div className="job-actions">
                                        <button style={{ width: "100px" }} className="secondary-button"
                                            onClick={() =>
                                                handleSave(
                                                    item.job.entityId)
                                            }
                                        >
                                            Save
                                        </button>

                                    </div>
                                </div>

                            </div>

                        </div>

                        <hr className="job-divider" />

                        <div className="job-footer">

                        </div>

                    </div>

                ))
            }

        </div>

    );
}