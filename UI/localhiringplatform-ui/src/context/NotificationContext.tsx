import { createContext } from "react";
import type { NotificationModel } from "../types/SignalR/NotificationModel";

export interface NotificationContextType {

    notifications: NotificationModel[];

    addNotification(
        notification: NotificationModel
    ): void;

    removeNotification(
        notificationId: string
    ): void;

    clearNotifications(): void;
}

export const NotificationContext =
    createContext<NotificationContextType>({
        notifications: [],
        addNotification: () => { },
        removeNotification: () => { },
        clearNotifications: () => { }
    });
