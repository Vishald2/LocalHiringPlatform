import JobCard from "../components/JobCard";
import type { Job } from "../types/Job";
export default function JobsPage() {

    const jobs: Job[] = [
         {
            id: 1,
            title: "ASP.NET Developer",
            company: "ABC Technologies",
            location: "Gurgaon",
            salary: "₹10 LPA"
        },
        {
            id: 2,
            title: "React Developer",
            company: "XYZ Solutions",
            location: "Noida",
            salary: "₹15 LPA"
        },
        {
            id: 3,
            title: "Angular Developer",
            company: "TechSoft",
            location: "Delhi",
            salary: "₹20 LPA"
        }
    ];

    return (
        <div>

            <h1>Jobs Page</h1>
            {
                jobs.map(
                    job => (
                        <div>
                            <JobCard key={job.id} job={job}></JobCard>
                            <hr />
                        </div>
                    )

                )
            }
        </div>
    );
}
