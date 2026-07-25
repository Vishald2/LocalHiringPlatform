import { useMemo, useState } from "react";
import { NotificationContext } from "../context/NotificationContext";
import type { NotificationModel } from "../types/SignalR/NotificationModel";

interface Props {
    children: React.ReactNode;
}

export function NotificationProvider({ children }: Props) {

    const [notifications, setNotifications] =
        useState<NotificationModel[]>([]);

    const addNotification = (
        notification: NotificationModel
    ) => {

        /*previous is the latest committed state, 
        even if multiple updates are queued. */
        setNotifications(previous => [
            ...previous,
            notification
        ]);
    };

    const removeNotification = (
        notificationId: string
    ) => {

        setNotifications(previous =>
            previous.filter(
                notification =>
                    notification.entityId !== notificationId
            )
        );
    };

    const clearNotifications = () => {

        setNotifications([]);
    };

    const value = useMemo(() => ({
        notifications,
        addNotification,
        removeNotification,
        clearNotifications
    }), [
        notifications
    ]);

    return (
                <NotificationContext.Provider value={value}>
                    {children}
                </NotificationContext.Provider>
            );
}