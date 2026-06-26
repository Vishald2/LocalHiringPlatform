
/*GET FROM SWAGGER*/
export const API_ENDPOINTS =
{
    candidate:
    {
        profile: "/candidate/profile",
        CandidateDashboard: "CandidateDashboard",
        RecommendedJobs: "recommended-jobs"
    },
    employer:
    {
        dashboard:"EmployerDashboard"
    },
    job:
    {
        root: "/job"
    },
    jobApplication:
    {
        root: "/jobapplication",
        employer: {
            applicants: "/jobapplication/employer/my",
            updatestatus:"/jobapplication/status"
        },
        GetApplicantsByJobId: "/jobapplication/job/:jobId"

    },

    skill:
    {
        root: "/master/skill"
    }
};