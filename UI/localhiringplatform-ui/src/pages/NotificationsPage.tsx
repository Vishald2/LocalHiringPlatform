
import { useEffect, useState } from "react";

import { getMyNotifications }
    from "../services/NotificationService";

import type { Notification }
    from "../types/Notification";

export default function NotificationsPage() {

    const [notifications,
        setNotifications] =
        useState<Notification[]>([]);

    useEffect(() => {

        async function loadNotifications() {

            const result =
                await getMyNotifications();

            setNotifications(result);
        }

        loadNotifications();

    }, []);

    return (

        <div className="page-container">

            <div className="dashboard-header">

                <h1 className="dashboard-title">
                    Notifications
                </h1>

                <p className="dashboard-subtitle">
                    View your latest updates.
                </p>

            </div>

            {
                notifications.length === 0
                    ? (
                        <div className="card">

                            <div className="card-body">

                                <p>
                                    No notifications found.
                                </p>

                            </div>

                        </div>
                    )
                    : (
                        notifications.map(
                            notification => (

                                <div
                                    key={
                                        notification.entityId
                                    }
                                    className="card"
                                    style={{
                                        marginBottom:
                                            "15px"
                                    }}
                                >

                                    <div
                                        className="card-body"
                                    >

                                        <h3>
                                            {
                                                notification.title
                                            }
                                        </h3>

                                        <p>
                                            {
                                                notification.message
                                            }
                                        </p>

                                        <small>
                                            {
                                                new Date(
                                                    notification.createdOn
                                                ).toLocaleString()
                                            }
                                        </small>

                                    </div>

                                </div>
                            ))
                    )
            }

        </div>
    );
}