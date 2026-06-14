import { useState } from "react";
import type { CandidateSearchResult } from "../../types/CandidateSearchResult";
import { searchCandidates } from "../../services/CandidateSearchService";
import { API_BASE_URL } from "../../config/api";

export default function EmployerCandidateSearchPage() {
    const [name, setName] =
        useState("");

    const [city, setCity] =
        useState("");

    const [results, setResults] = useState<CandidateSearchResult[]>([]);

    async function handleSearch() {
        const result =
            await searchCandidates({
                name,
                city
            });

        setResults(result);
    }
    return (

        <div className="page-container">

            <div className="form-card form-card-large">

                <h2 className="form-title">
                    Candidate Search
                </h2>

                <div className="form-group">

                    <label className="form-label">
                        Name
                    </label>

                    <input
                        className="form-control"
                        value={name}
                        onChange={(e) =>
                            setName(
                                e.target.value)
                        }
                    />

                </div>

                <div className="form-group">

                    <label className="form-label">
                        City
                    </label>

                    <input
                        className="form-control"
                        value={city}
                        onChange={(e) =>
                            setCity(
                                e.target.value)
                        }
                    />

                </div>

                <button
                    className="primary-button"
                    onClick={handleSearch}
                >
                    Search
                </button>

            </div>

            <div className="card">

                <div className="card-header">

                    <h3 className="card-title">
                        Candidates
                    </h3>

                </div>

                <div className="card-body">

                    {
                        results.map(
                            candidate => (

                                <div
                                    key={
                                        candidate.candidateProfileId
                                    }
                                    className="card"
                                >

                                    <h3>
                                        {
                                            candidate.fullName
                                        }
                                    </h3>

                                    <p>
                                        Email:
                                        {" "}
                                        {
                                            candidate.email
                                        }
                                    </p>

                                    <p>
                                        Mobile:
                                        {" "}
                                        {
                                            candidate.mobileNumber
                                        }
                                    </p>

                                    <p>
                                        City:
                                        {" "}
                                        {
                                            candidate.city
                                        }
                                    </p>

                                    <p>
                                        Experience:
                                        {" "}
                                        {
                                            candidate.totalExperienceYears
                                        }
                                        {" "}
                                        Years
                                    </p>

                                    <p>
                                        Open To Work:
                                        {" "}
                                        {
                                            candidate.isOpenToWork
                                                ? "Yes"
                                                : "No"
                                        }
                                    </p>

                                    <p>
                                        <a  href={`${API_BASE_URL}${candidate.resumeFilePath}`}
                                            target="_blank" rel="noreferrer"
                                        >
                                            Download Resume
                                        </a>
                                    </p>

                                </div>
                            ))
                    }

                </div>

            </div>

        </div>
    );
}