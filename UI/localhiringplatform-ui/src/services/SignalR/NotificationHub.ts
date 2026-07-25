import * as signalR from "@microsoft/signalr";
import type { NotificationModel } from "../../types/SignalR/NotificationModel";
import { API_BASE_URL } from "../../config/api";

export class NotificationHub {

    private connection: signalR.HubConnection;
    constructor() {

        this.connection = new signalR.HubConnectionBuilder()
            .withUrl(`${API_BASE_URL}/notificationHub`)
            .withAutomaticReconnect()
            .build();
    }

    public async start(): Promise<void> {

        if (this.connection.state === signalR.HubConnectionState.Disconnected) {

            await this.connection.start();

            console.log("SignalR Connected");
        }
    }

    public async stop(): Promise<void> {

        if (this.connection.state !== signalR.HubConnectionState.Disconnected) {

            await this.connection.stop();

            console.log("SignalR Disconnected");
        }
    }

    public onNotification(
        callback: (notification: NotificationModel) => void
    ): void {

        this.connection.on(
            "ReceiveNotification",
            callback
        );
    }

    public offNotification(): void {

        this.connection.off("ReceiveNotification");
    }
}