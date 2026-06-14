
/*GET FROM SWAGGER*/
export const API_ENDPOINTS =
{
    candidate:
    {
        profile: "/candidate/profile",
        CandidateDashboard: "CandidateDashboard"
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
            }
    },

    skill:
    {
        root: "/master/skill"
    }
};