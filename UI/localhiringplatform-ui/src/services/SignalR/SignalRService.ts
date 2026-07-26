import {HubConnection, HubConnectionBuilder} from "@microsoft/signalr";
import { API_BASE_URL } from "../../config/api";
import type { NotificationModel } from "../../types/SignalR/NotificationModel";

let connection: HubConnection | null = null;

export async function getConnection(): Promise<HubConnection> {

    if (connection) {
        console.log("SignalR Already Connected");
        return connection;
    }

    connection = new HubConnectionBuilder()
    .withUrl(`${API_BASE_URL}/notificationHub`, {
        accessTokenFactory: () => {
            return localStorage.getItem("token") ?? "";
        }
    })
    .withAutomaticReconnect()
    .build();

    await connection.start();

    connection.on("Pong", (message: string) => {

        console.log("Received Pong from server:", message);

    });

    connection.on("JobApplied", (message: NotificationModel) => {

        console.log("JobApplied:", message);

    });

    console.log("SignalR Connected");

    return connection;
}

export async function pingServer() {
    const connection = await getConnection();

    await connection.invoke("Ping");
}

