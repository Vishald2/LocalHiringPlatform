import { BrowserRouter, Routes, Route } from "react-router-dom";
import LoginPage from "./pages/LoginPage";
import CandidateDashboardPage from "./pages/DashboardPage";
import CandidateProfilePage from "./pages/candidate/CandidateProfilePage";
import MyApplicationsPage from "./pages/candidate/MyApplicationsPage";
import CompanyProfilePage from "./pages/employer/CompanyProfilePage";
import EmployerDashboardPage from "./pages/employer/EmployerDashboardPage";
import MainLayout from "./layouts/MainLayout";
import ProtectedRoute from "./components/ProtectedRoute";
import DashboardPage from "./pages/DashboardPage";
import ManageSkills from "./pages/master/ManageSkills";
import CreateJobPage from "./pages/master/CreateJobPage";
import ApplicantListPage from "./pages/employer/ApplicantListPage";
import EmployerActiveJobsPage from "./pages/employer/EmployerActiveJobsPage";
import EditJobPage from "./pages/master/EditJobPage";
import EmployerCandidateSearchPage from "./pages/candidate/EmployerCandidateSearchPage";
import ManageJobsPage from "./pages/employer/ManageJobsPage";
import VerifyEmailPage from "./pages/VerifyEmailPage";
import NotificationsPage from "./pages/NotificationsPage";
import SavedJobsPage from "./pages/candidate/SavedJobsPage";
import JobApplicantsPage from "./pages/employer/JobApplicantsPage";
import ChangePasswordPage from "./pages/candidate/ChangePassword";
import CandidateRegisterPage from "./pages/candidate/CandidateRegisterPage";
import AvailableJobs from "./pages/candidate/AvailableJobs";
import JobDetailsPage from "./pages/Jobs/ViewJobDetails";

import ProfilePage from "./pages/ProfilePage";


import LandingPage from "./pages/Landing/LandingPage";

import AIChatPage from "./pages/AI/AIChatPage";

function App() {
    console.log("App.tsx");
    console.log(import.meta.env.VITE_TEST);
    console.log(import.meta.env.VITE_API_BASE_URL);
    return (
        <BrowserRouter>
            <Routes>
                <Route element={<MainLayout />}>
                    <Route path="/login" element={<LoginPage />} />
                    <Route path="/register" element={<CandidateRegisterPage />} />
                    <Route path="/cdashboard" element={<CandidateDashboardPage />} />
                    <Route
                        path="/cprofile"
                        element={
                            <ProtectedRoute>
                                <CandidateProfilePage />
                            </ProtectedRoute>
                        }
                    />
                    <Route
                        path="/cprofilenew"
                        element={
                            <ProtectedRoute>
                                <ProfilePage />
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
                    <Route path="/availablejobs"
                        element={
                            <AvailableJobs />
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
                    <Route path="/companyprofile" element={<CompanyProfilePage />} />
                    <Route path="/edashboard" element={<EmployerDashboardPage />} />
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
                    <Route
                        path="/managejobs"
                        element={<ManageJobsPage />}
                    />
                    <Route
                        path="/verify-email"
                        element={
                            <VerifyEmailPage />
                        }
                    />
                    <Route path="/notifications"
                        element={<NotificationsPage />}
                    />

                    <Route
                        path="/savedjobs"
                        element={
                            <SavedJobsPage />
                        }
                    />

                    <Route
                        path="/jobapplicants/:jobId"
                        element={
                            <JobApplicantsPage />
                        }
                    />
                    <Route
                        path="/changepassword"
                        element={<ChangePasswordPage />}
                    />

                    <Route
                        path="/"
                        element={<LandingPage />}
                    />
                    <Route
                        path="/aichat"
                        element={<AIChatPage />}
                    />

                    <Route
                        path="/jobdetails/:jobId"
                        element={<JobDetailsPage />}
                    />
                </Route>
              
            </Routes>
        </BrowserRouter>
    );
}

export default App;