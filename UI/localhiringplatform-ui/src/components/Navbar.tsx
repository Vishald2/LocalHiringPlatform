import { Link, useNavigate } from "react-router-dom";

export default function Navbar() {

    const navigate = useNavigate();

    const token =
        localStorage.getItem("token");

    function handleLogout() {

        localStorage.removeItem("token");
        localStorage.removeItem("role");

        navigate("/login");
    }

    return (

        <nav className="navbar">

            <div className="navbar-brand">

                <Link to="/">
                    Local Hiring Platform
                </Link>

            </div>

            <div className="navbar-menu">

                <Link
                    className="navbar-link"
                    to="/">
                    Home
                </Link>

                {token && (
                    <>
                        <Link
                            className="navbar-link"
                            to="/dashboard">
                            Dashboard
                        </Link>

                        <Link
                            className="navbar-link"
                            to="/cprofile">
                            Profile
                        </Link>

                        <Link
                            className="navbar-link"
                            to="/jobs">
                            Jobs
                        </Link>

                        <button
                            className="navbar-button navbar-logout"
                            onClick={handleLogout}>
                            Logout
                        </button>
                    </>
                )}

                {!token && (
                    <>
                        <Link
                            className="navbar-link"
                            to="/jobs">
                            Jobs
                        </Link>

                        <Link
                            className="navbar-link"
                            to="/login">
                            Login
                        </Link>

                        <Link
                            className="navbar-link"
                            to="/register">
                            Register
                        </Link>
                    </>
                )}

            </div>

        </nav>
    );
}