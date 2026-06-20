import { useEffect, useState } from "react";
import type { EmployerDashboard } from "../../types/EmployerDashboard";
import { getEmployerDashboard, getEmployerProfile } from "../../services/employerDashboardService";
import { Link, useNavigate } from "react-router-dom";
export default function EmployerDashboardPage() {

    const [dashboard, setDashboard] = useState<EmployerDashboard | null>(null);

    const navigate = useNavigate();

    const [emailVerified, setEmailVerified] = useState(true);

    useEffect(() => {

        async function loadProfile() {

            const profile = await getEmployerProfile();

            console.log("Profile");
            console.log(profile);

            setEmailVerified(profile.isEmailVerified);
        }

        loadProfile();

    }, []);

    useEffect(() => {
        async function loadDashboard() {

            try {

                const data = await getEmployerDashboard();

                setDashboard(data);
            }
            catch (error) {

                console.error(error);
            }
        }

        loadDashboard();

    }, []);

    if (!dashboard) {

        return <div>Loading...</div>;
    }

    return (

        <div className="page-container dashboard-page">

            <div className="dashboard-header">

                <h1 className="dashboard-title">
                    Employer Dashboard
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
                    Manage jobs and track applicants.
                </p>
                <p>
                
                </p>
            </div>

            <div className="stats-grid">

                <div className="card stat-card">

                    <div className="stat-number">
                        <Link to="/createjob">
                            {dashboard.totalJobs}
                        </Link>
                    </div>

                    <div className="stat-label">
                        Total Jobs
                    </div>

                </div>

                <div className="card stat-card">

                    <div className="stat-number">
                        <Link to="/employer/activejobs">
                            {dashboard.activeJobs}
                        </Link>
                        
                    </div>

                    <div className="stat-label">
                        Active Jobs
                    </div>

                </div>

                <div className="card stat-card">

                    <div className="stat-number">
                        <Link to="/employer/jobs/applicants">
                            {dashboard.totalApplicants}
                        </Link>
                        
                        
                    </div>

                    <div className="stat-label">
                        Applicants
                    </div>

                </div>

                <div className="card stat-card">

                    <div className="stat-number">
                        {dashboard.newApplications}
                    </div>

                    <div className="stat-label">
                        New Applications
                    </div>

                </div>

                <div
                    className="card stat-card"
                    onClick={() =>
                        navigate(
                            "/employer/candidates")
                    }
                    style={{ cursor: "pointer" }}
                >
                    <div className="stat-number">
                        🔍
                    </div>

                    <div className="stat-label">
                        Search Candidates
                    </div>
                </div>

            </div>

            <div className="dashboard-header">

                <h2 className="dashboard-title">
                    Quick Actions
                </h2>

            </div>

            <div className="stats-grid">

                <div
                    className="card stat-card"
                    onClick={() => navigate("/managejobs")}
                    style={{ cursor: "pointer" }}
                >
                    <div className="stat-number">
                        📋
                    </div>

                    <div className="stat-label">
                        Manage Jobs
                    </div>
                </div>

                <div
                    className="card stat-card"
                    onClick={() => navigate("/createjob")}
                    style={{ cursor: "pointer" }}
                >
                    <div className="stat-number">
                        ➕
                    </div>

                    <div className="stat-label">
                        Post Job
                    </div>
                </div>

                <div
                    className="card stat-card"
                    onClick={() => navigate("/employer/jobs/applicants")}
                    style={{ cursor: "pointer" }}
                >
                    <div className="stat-number">
                        👥
                    </div>

                    <div className="stat-label">
                        Applicants
                    </div>
                </div>

                <div
                    className="card stat-card"
                    onClick={() => navigate("/eprofile")}
                    style={{ cursor: "pointer" }}
                >
                    <div className="stat-number">
                        🏢
                    </div>

                    <div className="stat-label">
                        Company Profile
                    </div>
                </div>

            </div>

        </div>
    );
}