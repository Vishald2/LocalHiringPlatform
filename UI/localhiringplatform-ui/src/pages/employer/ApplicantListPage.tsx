import { useEffect }
    from "react";

import { useState }
    from "react";

import { useParams }
    from "react-router-dom";

import {
    getApplicants
}
    from "../../services/ApplyToJobService";

import type {
    Applicant
}
    from "../../types/Applicant";

export default function ApplicantListPage() {
    const { jobId } =
        useParams();

    const [applicants,
        setApplicants]
        = useState<
            Applicant[]
        >([]);


    useEffect(() => {
        async function loadApplicants() {
            if (!jobId) {
                return;
            }

            const result =
                await getApplicants(
                    jobId);

            setApplicants(
                result);
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