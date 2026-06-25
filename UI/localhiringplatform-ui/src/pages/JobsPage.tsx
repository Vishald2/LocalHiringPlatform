import { useEffect }  from "react";
import { useState } from "react";
import {getJobs, searchJobs} from "../services/JobService";
import { applyToJob } from "../services/JobApplicationService";
import type { Job } from "../types/Job";
import { getErrorMessage } from "../utils/errorHelper";
import { saveJob } from "../services/SavedJobService";

async function handleApply(
    jobId: string) {
    try {
        await applyToJob({
            jobId
        });

        alert("Application submitted");
    }
    catch (error) {
        alert(getErrorMessage(error));
    }
}
export default function JobList() {

    const [jobs, setJobs] = useState<Job[]>([]);

    const [keyword,
        setKeyword] =
        useState("");

    const [city,
        setCity] =
        useState("");

    useEffect(() => {

        async function loadJobs() {

            console.log("const result = await getJobs();");

            const result = await getJobs();

            setJobs(result);

        }
        loadJobs();
    }, []);

    async function handleSearch() {

        const result =
            await searchJobs({
                keyword,
                city
            });

        setJobs(result);
    }

    async function handleClear() {

        setKeyword("");
        setCity("");

        const result =
            await getJobs();

        setJobs(result);
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

    return (
        <div>

            <h2>
                Available Jobs
            </h2>

            <div
                style={{
                    marginBottom: "20px"
                }}
            >
                <input
                    type="text"
                    placeholder="Keyword"
                    value={keyword}
                    onChange={
                        e =>
                            setKeyword(
                                e.target.value)
                    }
                />

                <input
                    type="text"
                    placeholder="City"
                    value={city}
                    onChange={
                        e =>
                            setCity(
                                e.target.value)
                    }
                    style={{
                        marginLeft: "10px"
                    }}
                />

                <button
                    onClick={handleSearch}
                    style={{
                        marginLeft: "10px"
                    }}
                >
                    Search
                </button>
                <span>&nbsp;&nbsp;</span>
                <button
                    onClick={handleClear}
                >
                    Clear
                </button>

            </div>
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

                            <button
                                onClick={() =>
                                    handleSave(
                                        job.entityId)
                                }
                                style={{
                                    marginLeft: "10px"
                                }}
                            >
                                Save
                            </button>
                        </p>
                    </div>

                ))
            }
        </div>
    );
}