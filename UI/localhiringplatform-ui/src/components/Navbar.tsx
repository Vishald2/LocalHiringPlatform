import { Link, useNavigate } from "react-router-dom";
import { getUnreadCount } from "../services/NotificationService";
import { useState, useEffect, useRef } from "react";
import { useContext } from "react";
import { NotificationContext } from "../context/NotificationContext";


export default function Navbar() {

    const [showNotifications,
        setShowNotifications] =
        useState(false);

    const { stop, clearNotifications } = useContext(NotificationContext);

    const navigate = useNavigate();

    const token =
        localStorage.getItem("token");

    const role =
        localStorage.getItem("role");

    console.log("Token:", token, "Role:", role);

    const [unreadCount, setUnreadCount] = useState(0);

    const {
        notifications
    } = useContext(NotificationContext);

    const notificationRef = useRef<HTMLDivElement>(null);

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

        const handleClickOutside = (event: MouseEvent) => {

            if (
                notificationRef.current &&
                !notificationRef.current.contains(event.target as Node)
            ) {
                setShowNotifications(false);
            }
        };

        document.addEventListener("mousedown", handleClickOutside);

        return () => {
            document.removeEventListener("mousedown", handleClickOutside);
        };

    }, [token]);

    function handleLogout() {

        localStorage.removeItem("token");
        localStorage.removeItem("role");

        stop();

        clearNotifications();



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

                <Link
                    className="navbar-link"
                    to="/aichat">
                    AI Chat
                </Link>

                {token && role === "Admin" && (

                    <>
                        <Link
                            className="navbar-link"
                            to="/mskill">
                            Skills
                        </Link>

                        <button
                            className="navbar-button navbar-logout"
                            onClick={handleLogout}>
                            Logout
                        </button>
                    </>
                
                )}

                {token && role === "Candidate" && (
                    <>
                        <Link
                            className="navbar-link"
                            to="/savedjobs">
                            Saved Jobs
                        </Link>
                       
                        <Link
                            className="navbar-link"
                            to="/dashboard">
                            Dashboard
                        </Link>

                        <Link
                            className="navbar-link"
                            to="/availablejobs">
                            Jobs
                        </Link>

                        <Link
                            className="navbar-link"
                            to="/candidate/myapplications">
                            My Applications
                        </Link>

                        <Link className="navbar-link"
                            to="/changepassword">
                            Change Password
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
                        <Link
                            className="navbar-link"
                            to="/cprofilenew">
                            Profile
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
                            to="/changepassword">
                           Change Password 
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
                        <div
                            className="notification-container"
                            ref={notificationRef}
                        >

                            <div
                                className="notification-icon"
                                onClick={() =>
                                    setShowNotifications(!showNotifications)
                                }
                            >
                                🔔

                                {
                                    notifications.length > 0 &&
                                    <span className="notification-badge">
                                        {notifications.length}
                                    </span>
                                }
                            </div>

                            {
                                showNotifications &&

                                <div className="notification-panel">

                                    {
                                        notifications.map((notification, index) => (

                                            <div
                                                key={index}
                                                className="notification-item"
                                            >
                                                {notification.message}
                                            </div>

                                        ))
                                    }

                                </div>
                            }

                        </div>
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
                            to="/availablejobs">
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