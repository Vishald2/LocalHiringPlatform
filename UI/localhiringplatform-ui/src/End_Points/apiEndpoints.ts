
/*GET FROM SWAGGER*/
export const API_ENDPOINTS =
{
    candidate:
    {
        profile: "/candidate/profile"
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
                applicants: "/jobapplication/employer/my"
            }
    },

    skill:
    {
        root: "/master/skill"
    }
};