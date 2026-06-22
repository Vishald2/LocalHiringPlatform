
import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import {getApplicantsByJobId} from "../../services/JobApplicationService";
import type { Applicant } from "../../types/Applicant";

export default function JobApplicantsPage() {

    const { jobId } = useParams();

    const [applicants,
        setApplicants] =
        useState<Applicant[]>([]);

    const sortedApplicants =
        [...applicants]
            .sort(
                (a, b) =>
                    b.matchPercentage
                    - a.matchPercentage);

    const [loading,
        setLoading] =
        useState(true);

    useEffect(() => {

        async function loadApplicants() {

            try {

                if (!jobId) {
                    return;
                }

                const data = await getApplicantsByJobId(jobId);
                console.log("getApplicantsByJobId");
                console.log(data);
                setApplicants(data);

            } catch (error) {

                console.error(error);

            } finally {

                setLoading(false);
            }
        }

        loadApplicants();

    }, [jobId]);

    if (loading) {

        return (
            <div className="page-container">
                Loading...
            </div>
        );
    }

    return (
        <div className="page-container">

            <div className="dashboard-header">

                <h1 className="dashboard-title">
                    Applicants - {applicants[0]?.jobTitle}
                </h1>

                <p className="dashboard-subtitle">
                    Candidates who applied for this job.
                </p>

            </div>

            {applicants.length === 0 ? (

                <div className="card">
                    No applicants found.
                </div>

            ) : (

                <div className="jobs-grid">

                        {sortedApplicants.map(
                        applicant => (

                            <div
                                key={
                                    applicant.candidateProfileId
                                }
                                className="card"
                            >
                                <h3>
                                    {applicant.candidateName}
                                </h3>

                                <p>
                                    {applicant.email}
                                </p>

                                <p>
                                    {applicant.mobileNumber}
                                </p>

                                <p>
                                    Status:
                                    {" "}
                                    {applicant.status}
                                </p>

                                <p>
                                    Applied:
                                    {" "}
                                    {new Date(
                                        applicant.appliedOn
                                    ).toLocaleDateString()}
                                </p>
                                <p>
                                    Match %
                                    {" "}
                                    {applicant.matchPercentage }
                                </p>

                                <p>

                                    <strong>
                                        Skills:
                                    </strong>

                                    {" "}

                                    {
                                        applicant.skills.length > 0
                                            ? applicant.skills.join(", ")
                                            : "No Skills"
                                    }

                                </p>

                            </div>
                        )
                    )}

                </div>
            )}

        </div>
    );
}