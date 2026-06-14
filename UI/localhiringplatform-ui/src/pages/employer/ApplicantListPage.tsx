import { useEffect } from "react";

import { useState } from "react";

import { getApplicantsByEmployer } from "../../services/JobApplicationService";

import type { Applicant } from "../../types/Applicant";

import { updateApplicationStatus }from "../../services/JobApplicationService";
import { API_BASE_URL } from "../../config/api";

export default function ApplicantListPage() {

    const [applicants, setApplicants] = useState<Applicant[]>([]);

    const statuses = [
        "Applied",
        "Shortlisted",
        "Interview Scheduled",
        "Rejected",
        "Hired"
    ];

    async function handleStatusChange(
        jobApplicationId: string,
        status: string) {
        try {
            await updateApplicationStatus({
                jobApplicationId,
                status
            });

            setApplicants(
                applicants.map(a =>
                    a.jobApplicationId === jobApplicationId
                        ? {
                            ...a,
                            status
                        }
                        : a
                )
            );
        }
        catch (error) {
            console.error(error);
        }
    }

    useEffect(() => {
        async function loadApplicants() {
            const result = await getApplicantsByEmployer();

            setApplicants(result);
        }

        loadApplicants();
    }, []);

    return (
        <div>

            <h2>
                Applicants
            </h2>

            {
                applicants.map(
                    applicant => (

                        <div key={
                            applicant.jobApplicationId
                            }
                            className="card"
                        >
                            <h3>
                                {
                                    applicant.candidateName
                                }
                            </h3>

                            <p>
                                Application Id:
                                {" "}
                                {
                                    applicant.jobApplicationId
                                }
                            </p>

                            <p>
                                Job Title:
                                {" "}
                                {
                                    applicant.jobTitle
                                }
                            </p>

                            <p>
                                Email:
                                {" "}
                                {
                                    applicant.email
                                }
                            </p>

                            <p>
                                Mobile:
                                {" "}
                                {
                                    applicant.mobileNumber
                                }
                            </p>

                            <p>
                                Status:
                                {" "}
                                {
                                    <select
                                        value={applicant.status}
                                        onChange={(e) =>
                                            handleStatusChange(applicant.jobApplicationId,
                                                e.target.value)}
                                    >
                                        {statuses.map(status => (
                                            <option key={status} value={status}>
                                                {status}
                                            </option>
                                        ))}
                                    </select>
                                }
                            </p>

                            <p>
                                Applied On:
                                {" "}
                                {
                                    new Date(
                                        applicant.appliedOn
                                    )
                                        .toLocaleDateString()
                                }
                            </p>

                            <p>
                                Resume:
                                {" "}

                                <a
                                    href={`${API_BASE_URL}${applicant.resumeFilePath}`}
                                    target="_blank"
                                    rel="noreferrer"
                                >
                                    {applicant.resumeFileName}
                                </a>

                                
                            </p>

                            

                        </div>
                    ))
            }

        </div>
    );
}