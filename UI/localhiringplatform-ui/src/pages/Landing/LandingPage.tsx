import "./LandingPage.css";
import { useNavigate } from "react-router-dom";

export default function LandingPage() {



    const navigate = useNavigate();

    return (

        <div className="landing-page">

            {/* Hero */}

            <section className="hero-section">

                <div className="hero-content">

                    <h1 className="hero-title">

                        LocalHire
                        <span className="hero-title-ai">
                            AI
                        </span>

                    </h1>

                    <h2 className="hero-subtitle">

                        Intelligent Hiring.
                        <br />
                        Smarter Careers.

                    </h2>

                    <p className="hero-description">

                        Helping employers identify the right candidates
                        through AI-powered resume analysis while enabling
                        professionals to discover better career opportunities.

                    </p>

                    <div className="hero-buttons">

                        <button
                            className="primary-button"
                            onClick={() =>
                                navigate("/register")
                            }
                        >
                            Get Started
                        </button>

                        <button
                            className="secondary-button"
                            onClick={() =>
                                navigate("/login")
                            }
                        >
                            Login
                        </button>

                    </div>

                    <div className="scroll-indicator">

                        ↓

                    </div>

                </div>

            </section>

            {/* Features */}

            <section className="features-section">

                <div className="section-container">

                    <div className="section-tag">

                        FEATURES

                    </div>

                    <h2 className="section-title">

                        Why Choose LocalHire AI?

                    </h2>

                    <p className="section-description">

                        Modern AI-powered capabilities designed to help
                        employers hire smarter and professionals discover
                        better career opportunities.

                    </p>

                    <div className="feature-grid">

                        <div className="feature-card">

                            <div className="feature-icon">
                                🧠
                            </div>

                            <h3>
                                AI Resume Analysis
                            </h3>

                            <p>
                                Reduce manual resume screening using
                                AI-powered candidate evaluation.
                            </p>

                        </div>

                        <div className="feature-card">

                            <div className="feature-icon">
                                🎯
                            </div>

                            <h3>
                                Smart Candidate Matching
                            </h3>

                            <p>
                                Identify relevant candidates
                                based on required skills.
                            </p>

                        </div>

                        <div className="feature-card">

                            <div className="feature-icon">
                                💼
                            </div>

                            <h3>
                                Job Recommendations
                            </h3>

                            <p>
                                Recommend relevant jobs
                                based on candidate profiles.
                            </p>

                        </div>

                        <div className="feature-card">

                            <div className="feature-icon">
                                📄
                            </div>

                            <h3>
                                PDF & DOCX Support
                            </h3>

                            <p>
                                Upload resumes in multiple
                                document formats.
                            </p>

                        </div>

                        <div className="feature-card">

                            <div className="feature-icon">
                                📊
                            </div>

                            <h3>
                                AI Strengths & Skill Gaps
                            </h3>

                            <p>
                                Understand strengths and
                                identify improvement areas.
                            </p>

                        </div>

                        <div className="feature-card">

                            <div className="feature-icon">
                                🔒
                            </div>

                            <h3>
                                Secure Authentication
                            </h3>

                            <p>
                                Role-based access with
                                secure JWT authentication.
                            </p>

                        </div>

                    </div>

                </div>

            </section>

            {/* How It Works */}

            <section className="how-it-works-section">

                <div className="section-container">

                    <div className="section-tag">

                        HOW IT WORKS

                    </div>

                    <h2 className="section-title">

                        Recruitment Made Simple

                    </h2>

                    <p className="section-description">

                        Three simple steps from profile creation
                        to AI-assisted hiring.

                    </p>

                    <div className="feature-grid">

                        <div className="feature-card">

                            <div className="feature-icon">
                                1️⃣
                            </div>

                            <h3>
                                Create Your Profile
                            </h3>

                            <p>

                                Register your account,
                                complete your profile and
                                upload your resume.

                            </p>

                        </div>

                        <div className="feature-card">

                            <div className="feature-icon">
                                2️⃣
                            </div>

                            <h3>
                                AI Analysis
                            </h3>

                            <p>

                                AI analyzes resumes,
                                evaluates candidate profiles
                                and recommends suitable jobs.

                            </p>

                        </div>

                        <div className="feature-card">

                            <div className="feature-icon">
                                3️⃣
                            </div>

                            <h3>
                                Hire Smarter
                            </h3>

                            <p>

                                Employers review AI insights
                                while candidates discover
                                better opportunities.

                            </p>

                        </div>

                    </div>

                </div>

            </section>

            {/* Candidate */}

            <section className="candidate-section">

                <div className="section-container">

                    <div className="section-tag">

                        FOR CANDIDATES

                    </div>

                    <h2 className="section-title">

                        Find Better Career Opportunities

                    </h2>

                    <p className="section-description">

                        Everything candidates need to build
                        a strong profile and get discovered.

                    </p>

                    <div className="feature-grid">

                        <div className="feature-card">
                            ✅ Professional Profile
                        </div>

                        <div className="feature-card">
                            ✅ Resume Upload
                        </div>

                        <div className="feature-card">
                            ✅ Save Jobs
                        </div>

                        <div className="feature-card">
                            ✅ One-click Applications
                        </div>

                        <div className="feature-card">
                            ✅ AI Job Recommendations
                        </div>

                        <div className="feature-card">
                            ✅ Track Applications
                        </div>

                    </div>

                </div>

            </section>

            {/* Employer */}

            <section className="employer-section">

                <div className="section-container">

                    <div className="section-tag">

                        FOR EMPLOYERS

                    </div>

                    <h2 className="section-title">

                        Hire with Confidence

                    </h2>

                    <p className="section-description">

                        AI-powered tools that simplify
                        recruitment and improve hiring decisions.

                    </p>

                    <div className="feature-grid">

                        <div className="feature-card">
                            ✅ Post Jobs
                        </div>

                        <div className="feature-card">
                            ✅ Manage Applicants
                        </div>

                        <div className="feature-card">
                            ✅ AI Resume Analysis
                        </div>

                        <div className="feature-card">
                            ✅ Match Score
                        </div>

                        <div className="feature-card">
                            ✅ Strengths & Skill Gaps
                        </div>

                        <div className="feature-card">
                            ✅ Hiring Recommendations
                        </div>

                    </div>

                </div>

            </section>

            {/* Technology */}

            <section className="technology-section">

                <div className="section-container">

                    <div className="section-tag">

                        TECHNOLOGY

                    </div>

                    <h2 className="section-title">

                        Built with Modern Technologies

                    </h2>

                    <p className="section-description">

                        ASP.NET Core 8, React, TypeScript,
                        SQL Server, Microsoft Azure and
                        Google Gemini AI.

                    </p>

                </div>

            </section>

            {/* About */}

            <section className="about-section">

                <div className="section-container">

                    <div className="section-tag">

                        ABOUT

                    </div>

                    <h2 className="section-title">

                        About LocalHire AI

                    </h2>

                    <p className="section-description">

                        LocalHire AI is a full-stack recruitment platform
                        built to demonstrate modern software engineering
                        using ASP.NET Core 8, React, TypeScript, SQL Server,
                        Azure and Google Gemini AI. It showcases AI-assisted
                        recruitment through resume analysis, intelligent
                        candidate matching and personalized job recommendations.

                    </p>

                </div>

            </section>

        </div>

    );

}