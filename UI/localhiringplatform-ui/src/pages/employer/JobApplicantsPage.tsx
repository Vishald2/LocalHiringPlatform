import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import {getApplicantsByJobId, getAiAnalysis} from "../../services/JobApplicationService";
import type { Applicant } from "../../types/Applicant";
import type { AiMatchResult } from "../../types/AiMatchResult";

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

    const [loading, setLoading] = useState(true);

    const [aiResults,
        setAiResults] =
        useState<
            Record<
                string,
                AiMatchResult
            >
            >({});

    const [visibleAiResults,
        setVisibleAiResults] =
        useState<Record<string, boolean>>({});

    const [loadingAi,
        setLoadingAi] =
        useState<Record<string, boolean>>({});

    useEffect(() => {

        async function loadApplicants() {

            try {

                if (!jobId) {
                    return;
                }

                const data = await getApplicantsByJobId(jobId);
                setApplicants(data);

            } catch (error) {

                console.error(error);

            } finally {

                setLoading(false);
            }
        }

        loadApplicants();

    }, [jobId]);

    async function handleAiAnalysis(
        jobId: string,
        candidateProfileId: string) {

        const existingResult =
            aiResults[candidateProfileId];

        if (existingResult) {

            setVisibleAiResults(
                prev => ({
                    ...prev,
                    [candidateProfileId]:
                        !prev[candidateProfileId]
                }));

            return;
        }

        try {

            // Set loading state for this candidate
            setLoadingAi(
                prev => ({
                    ...prev,
                    [candidateProfileId]:
                        true
                }));

            const result =
                await getAiAnalysis(
                    jobId,
                    candidateProfileId);

            setLoadingAi(
                prev => ({
                    ...prev,
                    [candidateProfileId]:
                        false
                }));
            setAiResults(
                prev => ({
                    ...prev,
                    [candidateProfileId]:
                        result
                }));

            setVisibleAiResults(
                prev => ({
                    ...prev,
                    [candidateProfileId]:
                        true
                }));

        } catch (error) {

            console.error(error);
        }
        finally {
            setLoadingAi(
                prev => ({
                    ...prev,
                    [candidateProfileId]:
                        false
                }));
        }
    }

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
            {
                sortedApplicants.map(
                    applicant => {

                        return (

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
                                    {
                                        new Date(
                                            applicant.appliedOn
                                        ).toLocaleDateString()
                                    }
                                </p>

                                <p>
                                    Match %
                                    {" "}
                                    {applicant.matchPercentage}
                                </p>

                                {
                                    <button
                                        onClick={() =>
                                            handleAiAnalysis(
                                                jobId,
                                                applicant.candidateProfileId)
                                        }
                                        disabled={
                                            loadingAi[
                                            applicant.candidateProfileId
                                            ]
                                        }
                                    >
                                        {
                                            loadingAi[
                                                applicant.candidateProfileId
                                            ]
                                                ? "Analyzing..."
                                                : "AI Analysis"
                                        }
                                    </button>
                                }

                                {
                                    visibleAiResults[
                                    applicant.candidateProfileId
                                    ] &&
                                    aiResults[
                                    applicant.candidateProfileId
                                    ] && ( (
                                        <div className="card">

                                            <p>
                                                <strong>
                                                    AI Score:
                                                </strong>
                                                
                                                {" "}
                                                {
                                                    aiResults[
                                                        applicant.candidateProfileId
                                                    ].score
                                                }
                                                %
                                            </p>

                                            <p>
                                                Recommendation:
                                                {" "}
                                                {
                                                    aiResults[
                                                        applicant.candidateProfileId
                                                    ].recommendation
                                                }
                                            </p>

                                            <p>
                                                <strong>
                                                    Strengths:
                                                </strong>
                                            </p>

                                            <ul>
                                                {
                                                    aiResults[
                                                        applicant.candidateProfileId
                                                    ].strengths.map(
                                                        (strength, index) => (
                                                            <li key={index}>
                                                                {strength}
                                                            </li>
                                                        ))
                                                }
                                            </ul>

                                            <p>
                                                <strong>
                                                    Gaps:
                                                </strong>
                                            </p>

                                            <ul>
                                                {
                                                    aiResults[
                                                        applicant.candidateProfileId
                                                    ].gaps.map(
                                                        (gap, index) => (
                                                            <li key={index}>
                                                                {gap}
                                                            </li>
                                                        ))
                                                }
                                            </ul>

                                        </div>
                                    )
                                )}

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
                        );
                    }
                )
            }

        </div>
    );
}