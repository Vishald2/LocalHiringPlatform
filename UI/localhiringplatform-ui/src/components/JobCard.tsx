import type { Job } from "../types/Job";

type JobCardProps = {
    job: Job;
};

export default function JobCard({ job }: JobCardProps) {
    return (
        <div>
            <h3>{job.title}</h3>
            <p>{job.company}</p>
            <p>{job.location}</p>
            <p>{job.salary}</p>
        </div>
    );
}