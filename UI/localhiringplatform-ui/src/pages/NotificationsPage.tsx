
import { useEffect, useState } from "react";

import { getMyNotifications }
    from "../services/NotificationService";

import type { Notification }
    from "../types/Notification";

import {markAsRead} from "../services/NotificationService";

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

    async function
        handleMarkAsRead(
            notificationId: string) {
        await markAsRead(
            notificationId);

        setNotifications(
            notifications.map(
                x =>
                    x.entityId === notificationId
                        ? {
                            ...x,
                            isRead: true
                        }
                        : x));
    }

    return (

        <div className="page-container">

            <div className="dashboard-header">

                <h4 className="dashboard-title">
                    Notifications
                </h4>

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
                                        marginBottom: "15px",
                                        backgroundColor:
                                            notification.isRead
                                                ? "white"
                                                : "#eef6ff"
                                    }}
                                >

                                    <div className="card-body">
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
                                        {
                                            !notification.isRead &&
                                            (
                                                <div
                                                    style={{
                                                        marginTop: "10px"
                                                    }}
                                                >
                                                    <button
                                                        className="primary-button"
                                                        onClick={() =>
                                                            handleMarkAsRead(
                                                                notification.entityId)}
                                                    >
                                                        Mark As Read
                                                    </button>
                                                </div>
                                            )
                                        }

                                    </div>

                                </div>
                            ))
                    )
            }


        </div>
    );
}