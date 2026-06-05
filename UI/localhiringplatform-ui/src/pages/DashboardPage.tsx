import { useNavigate } from "react-router-dom";
export default function DashboardPage() {

    const navigate = useNavigate();

    function handleLogout() {

        localStorage.clear();

        navigate("/login");
    }

    return (
        <div>
            <h1>Candidate Dashboard</h1>

            <button
                onClick={handleLogout}>
                Logout
            </button>
        </div>
    );
}