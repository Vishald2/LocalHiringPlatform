import { useEffect } from "react";

import { useState } from "react";

import { getApplicantsByEmployer } from "../../services/JobApplicationService";

import type {Applicant} from "../../types/Applicant";

export default function ApplicantListPage() {

    const [applicants, setApplicants] = useState<Applicant[]>([]);


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

                        <div
                            key={
                                applicant.candidateProfileId
                            }
                            className="card"
                        >
                            <h3>
                                {
                                    applicant.candidateName
                                }
                            </h3>

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
                                    applicant.status
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

                        </div>
                    ))
            }

        </div>
    );
}