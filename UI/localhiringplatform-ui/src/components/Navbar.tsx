import { Link, useNavigate } from "react-router-dom";
import { getUnreadCount } from "../services/NotificationService";
import { useState, useEffect } from "react";

export default function Navbar() {

    const navigate = useNavigate();

    const token =
        localStorage.getItem("token");

    const role =
        localStorage.getItem("role");

    const [unreadCount, setUnreadCount] = useState(0);

    useEffect(() => {

        async function
            loadUnreadCount() {
            try {
                const count =
                    await getUnreadCount();

                setUnreadCount(
                    count);
            }
            catch(e) {
                console.error("Error fetching unread count:", e);
            }
        }

        if (token) {
            loadUnreadCount();
        }

    }, [token]);

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

                {token && role === "Candidate" && (
                    <>
                        <Link
                            className="navbar-link"
                            to="/dashboard">
                            Dashboard
                        </Link>

                        <Link
                            className="navbar-link"
                            to="/jobs">
                            Jobs
                        </Link>

                        <Link
                            className="navbar-link"
                            to="/candidate/myapplications">
                            My Applications
                        </Link>

                        <Link
                            className="navbar-link"
                            to="/cprofile">
                            Profile
                        </Link>

                        <Link
                            className="navbar-link"
                            to="/notifications">
                            Notifications
                            {
                                unreadCount > 0 &&
                                ` (${unreadCount})`
                            }
                        </Link>

                        <button
                            className="navbar-button navbar-logout"
                            onClick={handleLogout}>
                            Logout
                        </button>
                    </>
                )}

                {token && role === "Employer" && (
                    <>
                        <Link
                            className="navbar-link"
                            to="/mskill">
                            Skills
                        </Link>
                        <Link
                            className="navbar-link"
                            to="/edashboard">
                            Dashboard
                        </Link>

                        <Link
                            className="navbar-link"
                            to="/companyprofile">
                            Company Profile
                        </Link>

                        <Link
                            className="navbar-link"
                            to="/createjob">
                            Create Job
                        </Link>

                        <Link
                            className="navbar-link"
                            to="/managejobs">
                            Manage Jobs
                        </Link>

                        <Link
                            className="navbar-link"
                            to="/employer/candidates">
                            Search Candidates
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