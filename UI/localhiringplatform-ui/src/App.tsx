import { BrowserRouter, Routes, Route } from "react-router-dom";
import HomePage from "./pages/HomePage";
import LoginPage from "./pages/LoginPage";
import RegisterPage from "./pages/CandidateRegisterPage";
import CandidateDashboardPage from "./pages/DashboardPage";
import CandidateProfilePage from "./pages/candidate/CandidateProfilePage";
import MyApplicationsPage from "./pages/candidate/MyApplicationsPage";
import CompanyProfilePage from "./pages/employer/CompanyProfilePage";
import EmployerDashboardPage from "./pages/employer/EmployerDashboardPage";
import JobList from "./pages/JobsPage";
import MainLayout from "./layouts/MainLayout";
import CandidateRegisterPage from "./pages/CandidateRegisterPage";
import ProtectedRoute from "./components/ProtectedRoute";
import DashboardPage from "./pages/DashboardPage";
import ManageSkills from "./pages/master/ManageSkills";
import CreateJobPage from "./pages/master/CreateJobPage";
import ApplicantListPage from "./pages/employer/ApplicantListPage";
import EmployerActiveJobsPage from "./pages/employer/EmployerActiveJobsPage";
import EditJobPage from "./pages/master/EditJobPage";
import EmployerCandidateSearchPage from "./pages/candidate/EmployerCandidateSearchPage";
function App() {
    return (
        <BrowserRouter>
            <Routes>
                <Route element={<MainLayout />}>
                    <Route path="/" element={<HomePage />} />
                    <Route path="/login" element={<LoginPage />} />
                    <Route path="/register" element={<RegisterPage />} />
                    <Route path="/cdashboard" element={<CandidateDashboardPage />} />
                    <Route
                        path="/cprofile"
                        element={
                            <ProtectedRoute>
                                <CandidateProfilePage />
                            </ProtectedRoute>
                        }
                    />
                    <Route path="/createjob"
                        element={
                            <ProtectedRoute>
                                <CreateJobPage />
                            </ProtectedRoute>
                        }
                    />
                    <Route path="/jobs"
                        element={
                            <JobList />
                        }
                    />
                    <Route
                        path="/mskill"
                        element={
                            <ProtectedRoute>
                                <ManageSkills />
                            </ProtectedRoute>
                        }
                    />
                    <Route path="/capps" element={<MyApplicationsPage />} />
                    <Route path="/eprofile" element={<CompanyProfilePage />} />
                    <Route path="/edashboard" element={<EmployerDashboardPage />} />
                    <Route path="/candidate/register" element={<CandidateRegisterPage />}/>
                    <Route
                        path="/dashboard"
                        element={
                            <ProtectedRoute>
                                <DashboardPage />
                            </ProtectedRoute>
                        }
                    />
                    <Route
                        path="/candidate/myapplications"
                        element={
                            <MyApplicationsPage />
                        }
                    />

                    <Route
                        path="/employer/activejobs"
                        element={
                            <EmployerActiveJobsPage />
                        }
                    />

                    <Route
                        path="/employer/jobs/applicants"
                        element={
                            <ApplicantListPage />
                        }
                    />
                    <Route
                        path="/jobs/edit/:id"
                        element={<EditJobPage />}
                    />
                    <Route
                        path="/employer/candidates"
                        element={
                            <EmployerCandidateSearchPage />
                        }
                    />
                </Route>
              
            </Routes>
        </BrowserRouter>
    );
}

export default App;