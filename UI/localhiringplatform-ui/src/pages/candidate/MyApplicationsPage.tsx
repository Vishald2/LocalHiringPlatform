import { useEffect } from "react";
import { useState } from "react";
import { getMyApplications } from "../../services/JobApplicationService";
import type { MyApplication } from "../../types/MyApplication";

export default function MyApplicationsPage() {
    const [applications,
        setApplications]
        = useState<MyApplication[]>([]);



    useEffect(() => {
        async function loadApplications() {

            const result =
                await getMyApplications();

            setApplications(result);
        }
        loadApplications();
    }, []);

    return (
        <div className="page-container">

            <h4>
                My Applications
            </h4>

            {
                applications.map(
                    application => (

                        <div
                            key={
                                application.jobId
                            }
                            className="card"
                        >

                            <h3>
                                {
                                    application.jobTitle
                                }
                            </h3>

                            <p>
                                Status:
                                {" "}
                                {
                                    application.status
                                }
                            </p>

                            <p>
                                Applied On:
                                {" "}
                                {
                                    new Date(
                                        application.appliedOn
                                    )
                                        .toLocaleDateString()
                                }
                            </p>

                        </div>
                    ))
            }

        </div>
    );
}