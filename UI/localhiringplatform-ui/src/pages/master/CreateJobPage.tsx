import { useState } from "react";

import { addJob } from "../../services/JobService";
import { getErrorMessage } from "../../utils/errorHelper";

export default function CreateJobPage() {

    const [job, setJob] = useState({
        title: "",
        description: "",
        city: "",
        state: "",
        minSalary: 0,
        maxSalary: 0,
        experienceRequired: 0,
        requiredSkills: ""
    });

    async function handleSubmit(
        e: React.FormEvent) {

        e.preventDefault();

        try {

            await addJob(job);

            alert(
                "Job created successfully");

            setJob({
                title: "",
                description: "",
                city: "",
                state: "",
                minSalary: 0,
                maxSalary: 0,
                experienceRequired: 0,
                requiredSkills: ""
            });
        }
        catch (error) {
            alert(getErrorMessage(error));
            console.log(error);
        }
    }

    return (
        <div className="page-container">

            <div className="form-card form-card-large">

                <h2 className="form-title">
                    Create Job
                </h2>

                <form
                    onSubmit={
                        handleSubmit
                    }>

                    <div className="form-group">

                        <label className="form-label">
                            Title
                        </label>

                        <input
                            className="form-control"
                            value={job.title}
                            onChange={(e) =>
                                setJob({
                                    ...job,
                                    title:
                                        e.target.value
                                })
                            }
                        />

                    </div>

                    <div className="form-group">

                        <label className="form-label">
                            Description
                        </label>

                        <textarea
                            className="form-textarea"
                            value={job.description}
                            onChange={(e) =>
                                setJob({
                                    ...job,
                                    description:
                                        e.target.value
                                })
                            }
                        />

                    </div>

                    <div className="form-group">

                        <label className="form-label">
                            City
                        </label>

                        <input
                            className="form-control"
                            value={job.city}
                            onChange={(e) =>
                                setJob({
                                    ...job,
                                    city:
                                        e.target.value
                                })
                            }
                        />

                    </div>

                    <div className="form-group">

                        <label className="form-label">
                            State
                        </label>

                        <input
                            className="form-control"
                            value={job.state}
                            onChange={(e) =>
                                setJob({
                                    ...job,
                                    state:
                                        e.target.value
                                })
                            }
                        />

                    </div>

                    <div className="form-group">

                        <label className="form-label">
                            Minimum Salary
                        </label>

                        <input
                            className="form-control"
                            type="number"
                            value={job.minSalary}
                            onChange={(e) =>
                                setJob({
                                    ...job,
                                    minSalary:
                                        Number(
                                            e.target.value)
                                })
                            }
                        />

                    </div>

                    <div className="form-group">

                        <label className="form-label">
                            Maximum Salary
                        </label>

                        <input
                            className="form-control"
                            type="number"
                            value={job.maxSalary}
                            onChange={(e) =>
                                setJob({
                                    ...job,
                                    maxSalary:
                                        Number(
                                            e.target.value)
                                })
                            }
                        />

                    </div>

                    <div className="form-group">

                        <label className="form-label">
                            Experience Required (Years)
                        </label>

                        <input
                            className="form-control"
                            type="number"
                            value={
                                job.experienceRequired
                            }
                            onChange={(e) =>
                                setJob({
                                    ...job,
                                    experienceRequired:
                                        Number(
                                            e.target.value)
                                })
                            }
                        />

                    </div>

                    <div className="form-group">

                        <label className="form-label">
                            Required Skills
                        </label>

                        <input
                            className="form-control"
                            placeholder="C#, ASP.NET Core, SQL Server"
                            value={
                                job.requiredSkills
                            }
                            onChange={(e) =>
                                setJob({
                                    ...job,
                                    requiredSkills:
                                        e.target.value
                                })
                            }
                        />

                    </div>

                    <button
                        type="submit"
                        className="primary-button">

                        Create Job

                    </button>

                </form>

            </div>

        </div>
    );
}