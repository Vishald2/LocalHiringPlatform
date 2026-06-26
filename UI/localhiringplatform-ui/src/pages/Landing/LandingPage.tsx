import "./LandingPage.css";
import { useNavigate } from "react-router-dom";

export default function LandingPage() {



    const navigate = useNavigate();

    return (

        <div className="landing-page">

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

                        ↓ Scroll to Discover

                    </div>

                </div>

            </section>

            <section className="features-section">

                <div className="section-container">
                    <h2 className="section-title">

                        Why Choose LocalHire AI?

                    </h2>

                    <div className="section-tag">

                        FEATURES

                    </div>

                    <p className="section-description">

                        Modern AI-powered capabilities designed to help
                        employers hire smarter and professionals discover
                        better career opportunities.

                    </p>

                    <div className="feature-grid">

                        <div className="feature-card feature-card-large">

                            <div className="feature-icon">

                                🧠

                            </div>

                            <h3>

                                AI Resume Analysis

                            </h3>

                            <p>

                                Reduce manual resume screening with
                                AI-powered candidate evaluation.

                            </p>

                        </div>

                        <div className="feature-card feature-card-large">

                            <div className="feature-icon">

                                🎯

                            </div>

                            <h3>

                                Smart Candidate Matching

                            </h3>

                            <p>

                                Identify relevant candidates based
                                on required skills.

                            </p>

                        </div>

                        <div className="feature-card feature-card-large">

                            <div className="feature-icon">

                                💼

                            </div>

                            <h3>

                                Job Recommendations

                            </h3>

                            <p>

                                Help candidates discover better
                                career opportunities.

                            </p>

                        </div>

                        <div className="feature-card">

                            <div className="feature-icon">

                                📄

                            </div>

                            <h3>

                                PDF & DOCX Support

                            </h3>

                        </div>

                        <div className="feature-card">

                            <div className="feature-icon">

                                📊

                            </div>

                            <h3>

                                AI Strengths & Skill Gaps

                            </h3>

                        </div>

                        <div className="feature-card">

                            <div className="feature-icon">

                                🔒

                            </div>

                            <h3>

                                Secure Authentication

                            </h3>

                        </div>

                    </div>

                </div>

            </section>

            <section className="candidate-section">

                <h2>
                    Built for Candidates
                </h2>

            </section>

            <section className="employer-section">

                <h2>
                    Built for Employers
                </h2>

            </section>

            <section className="technology-section">

                <h2>
                    Technology Stack
                </h2>

            </section>

            <section className="about-section">

                <h2>
                    About This Project
                </h2>

            </section>

            <footer>

                © 2026 LocalHire AI

            </footer>

        </div>

    );

}