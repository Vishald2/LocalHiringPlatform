import { useNavigate } from "react-router-dom";
import { useEffect, useState } from "react";
import { getProfile } from "../services/CandidateProfileService";
import { getCandidateDashboard} from "../services/CandidateDashboardService";
import type { CandidateDashboardResponse } from "../types/CandidateDashboardResponse";
export default function DashboardPage() {

    const navigate = useNavigate();
    const [profileCompletion, setProfileCompletion] =
        useState<number>(0);

    const [dashboard, setDashboard] = useState<CandidateDashboardResponse>({
        totalApplications: 0,
        shortlisted: 0,
        interviewScheduled: 0,
        rejected: 0,
        hired: 0
    });

    const [emailVerified, setEmailVerified] = useState(true);

    useEffect(() => {

        async function loadProfile() {

            const profile = await getProfile();

            console.log("Profile");
            console.log(profile);

            setProfileCompletion(profile.profileCompletionPercentage);

            setEmailVerified(profile.emailVerified);
        }

        loadProfile();

    }, []);

    useEffect(() => {

        async function loadDashboard() {

            const result = await getCandidateDashboard();

            console.log("Dashboard data:", result);

            setDashboard(result);
        }

        loadDashboard();

    }, []);

    return (

        <div className="page-container dashboard-page">

            <div className="dashboard-header">

                <h1 className="dashboard-title">
                    Candidate Dashboard
                </h1>
                {
                    !emailVerified && (

                        <div
                            className="card"
                            style={{
                                marginBottom: "20px",
                                border: "1px solid orange"
                            }}
                        >
                            <h3>
                                Email Not Verified
                            </h3>

                            <p>
                                Please verify your email before
                                applying for jobs.
                            </p>

                        </div>
                    )
                }
                <p className="dashboard-subtitle">
                    Welcome back. Complete your profile and start applying for jobs.
                </p>

            </div>

            <div className="stats-grid">

                <div
                    className="card stat-card"
                    onClick={() => navigate("/cprofile")}
                    style={{ cursor: "pointer" }}
                >
                    <div className="stat-number">
                        {profileCompletion}%
                    </div>

                    <div className="stat-label">
                        Profile Completion
                    </div>
                </div>

                <div
                    className="card stat-card"
                    onClick={() =>
                        navigate("/candidate/myapplications")
                    }
                    style={{ cursor: "pointer" }}
                >
                    <div className="stat-number">
                        {dashboard.totalApplications}
                    </div>

                    <div className="stat-label">
                        Applications
                    </div>
                </div>

                <div className="card stat-card">

                    <div className="stat-number">
                        {dashboard.shortlisted}
                    </div>

                    <div className="stat-label">
                        Shortlisted
                    </div>

                </div>

                <div className="card stat-card">

                    <div className="stat-number">
                        {dashboard.interviewScheduled}
                    </div>

                    <div className="stat-label">
                        Interviews
                    </div>

                </div>

                <div className="card stat-card">

                    <div className="stat-number">
                        {dashboard.rejected}
                    </div>

                    <div className="stat-label">
                        Rejected
                    </div>

                </div>

                <div className="card stat-card">

                    <div className="stat-number">
                        {dashboard.hired}
                    </div>

                    <div className="stat-label">
                        Hired
                    </div>

                </div>

            </div>

            <div className="dashboard-content">

                <div className="card">

                    <div className="card-header">

                        <h3 className="card-title">
                            Quick Actions
                        </h3>

                    </div>

                    <div className="card-body">

                        <button
                            className="primary-button"
                            onClick={() => navigate("/cprofile")}
                        >
                            Complete Profile
                        </button>

                    </div>

                </div>

                <div className="card">

                    <div className="card-header">

                        <h3 className="card-title">
                            Account Status
                        </h3>

                    </div>

                    <div className="card-body">

                        <p>
                            Open To Work: Yes
                        </p>

                        <p>
                            Resume Uploaded: Yes
                        </p>

                    </div>

                </div>

            </div>

        </div>
    );
}