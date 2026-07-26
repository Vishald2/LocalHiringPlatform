import { useCallback, useEffect, useMemo, useState } from "react";
import { NotificationContext } from "../context/NotificationContext";
import type { NotificationModel } from "../types/SignalR/NotificationModel";
import { SignalRClient } from "../services/SignalR/SignalRClient";

interface Props {
    children: React.ReactNode;
}

export function NotificationProvider({ children }: Props) {

    const [notifications, setNotifications] =
        useState<NotificationModel[]>([]);

    const addNotification = useCallback((
        notification: NotificationModel
    ) => {

        /*previous is the latest committed state, 
        even if multiple updates are queued. */
        setNotifications(previous => [
            ...previous,
            notification
        ]);
    },[]);

    const removeNotification = useCallback((
        notificationId: string
    ) => {

        setNotifications(previous =>
            previous.filter(
                notification =>
                    notification.entityId !== notificationId
            )
        );
    },[]);

    const clearNotifications = useCallback(() => {

        setNotifications([]);
    },[]);

    const signalRClient = useMemo(
        () => new SignalRClient(),
        []
    );

    const start = useCallback(async () => {
        await signalRClient.start();
    },[]);

    const stop = useCallback(async () => {
        await signalRClient.stop();
    },[]);

    useEffect(() => {

        signalRClient.onNotification(notification => {

            console.log("Notification Received");

            addNotification(notification);

        });

        return () => {
            signalRClient.offNotification();
        };

    });

    const value = useMemo(() => ({
        notifications,
        addNotification,
        removeNotification,
        clearNotifications,
        start,
        stop
    }), [
        notifications,
        addNotification,
        removeNotification,
        clearNotifications,
        start,
        stop
    ]);

    return (
                <NotificationContext.Provider value={value}>
                    {children}
                </NotificationContext.Provider>
    );
}