import { BrowserRouter, Routes, Route } from "react-router-dom";
import HomePage from "./pages/HomePage";
import LoginPage from "./pages/LoginPage";
import RegisterPage from "./pages/CandidateRegisterPage";
import CandidateDashboardPage from "./pages/candidate/CandidateDashboardPage";
import CandidateProfilePage from "./pages/candidate/CandidateProfilePage";
import MyApplicationsPage from "./pages/candidate/MyApplicationsPage";
import ApplicantsPage from "./pages/employer/ApplicantsPage";
import CompanyProfilePage from "./pages/employer/CompanyProfilePage";
import EmployerDashboardPage from "./pages/employer/EmployerDashboardPage";
import JobsPage from "./pages/JobsPage";
import MainLayout from "./layouts/MainLayout";
import type { CandidateRegisterRequest } from "./types/CandidateRegisterRequest";
import CandidateRegisterPage from "./pages/CandidateRegisterPage";


function App() {
    return (
        <BrowserRouter>
            <Routes>
                <Route element={<MainLayout />}>
                    <Route path="/" element={<HomePage />} />
                    <Route path="/login" element={<LoginPage />} />
                    <Route path="/login" element={<LoginPage />} />
                    <Route path="/register" element={<RegisterPage />} />
                    <Route path="/cdashboard" element={<CandidateDashboardPage />} />
                    <Route path="/cprofile" element={<CandidateProfilePage />} />
                    <Route path="/capps" element={<MyApplicationsPage />} />
                    <Route path="/eapps" element={<ApplicantsPage />} />
                    <Route path="/eprofile" element={<CompanyProfilePage />} />
                    <Route path="/edashboard" element={<EmployerDashboardPage />} />
                    <Route path="/ejobs" element={<JobsPage />} />
                    <Route path="/candidate/register" element={<CandidateRegisterPage />}
                    />
                </Route>
            </Routes>
        </BrowserRouter>
    );
}

export default App;