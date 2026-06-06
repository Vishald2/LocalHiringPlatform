import { Link } from "react-router-dom";

export default function HomePage() {

    return (

        <div className="page-container">

            <section className="hero-section">

                <h1 className="hero-title">
                    Find Local Jobs Faster
                </h1>

                <p className="hero-subtitle">
                    Connect job seekers and employers in your city.
                    Find opportunities near you and hire talent quickly.
                </p>

                <div className="hero-actions">

                    <Link
                        to="/register"
                        className="hero-primary-btn">
                        Register Now
                    </Link>

                    <Link
                        to="/login"
                        className="hero-secondary-btn">
                        Login
                    </Link>

                </div>

            </section>

            <section className="features-grid">

                <div className="card">

                    <h3 className="card-title">
                        Local Jobs
                    </h3>

                    <p>
                        Discover jobs near your location.
                    </p>

                </div>

                <div className="card">

                    <h3 className="card-title">
                        Easy Applications
                    </h3>

                    <p>
                        Apply to jobs with a single click.
                    </p>

                </div>

                <div className="card">

                    <h3 className="card-title">
                        Verified Employers
                    </h3>

                    <p>
                        Connect with genuine local companies.
                    </p>

                </div>

            </section>

        </div>
    );
}