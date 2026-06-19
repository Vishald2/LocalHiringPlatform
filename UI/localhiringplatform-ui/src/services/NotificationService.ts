
import { api } from "./api";
import type { Notification } from "../types/Notification";

export async function getMyNotifications() {

    const response =
        await api.get<Notification[]>(
            "/notification/my");

    return response.data;
}