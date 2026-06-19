
import { api } from "./api";
import type { Notification } from "../types/Notification";

export async function getMyNotifications() {

    const response =
        await api.get<Notification[]>(
            "/notification/my");

    return response.data;
}

export async function getUnreadCount() {
    const response =
        await api.get<number>(
            "/notification/unread-count");

    return response.data;
}

export async function markAsRead(notificationId: string) {

    await api.put(`/notification/${notificationId}/read`);
}