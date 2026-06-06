import { useNavigate } from "react-router-dom";

export default function DashboardPage() {

    const navigate = useNavigate();

    return (

        <div className="page-container dashboard-page">

            <div className="dashboard-header">

                <h1 className="dashboard-title">
                    Candidate Dashboard
                </h1>

                <p className="dashboard-subtitle">
                    Welcome back. Complete your profile and start applying for jobs.
                </p>

            </div>

            <div className="stats-grid">

                <div className="card stat-card">

                    <div className="stat-number">
                        20%
                    </div>

                    <div className="stat-label">
                        Profile Completion
                    </div>

                </div>

                <div className="card stat-card">

                    <div className="stat-number">
                        0
                    </div>

                    <div className="stat-label">
                        Applications
                    </div>

                </div>

                <div className="card stat-card">

                    <div className="stat-number">
                        0
                    </div>

                    <div className="stat-label">
                        Saved Jobs
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
                            Resume Uploaded: No
                        </p>

                    </div>

                </div>

            </div>

        </div>
    );
}