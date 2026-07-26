import * as signalR from "@microsoft/signalr";
import type { NotificationModel } from "../../types/SignalR/NotificationModel";
import { API_BASE_URL } from "../../config/api";


export class SignalRClient {

    // SignalRClient.ts

   

    private connection: signalR.HubConnection;
    constructor() {

        this.connection = new signalR.HubConnectionBuilder()
            .withUrl(`${API_BASE_URL}/notificationHub`, {
                accessTokenFactory: () => {

                    const token = localStorage.getItem("token");

                    console.log("SignalR Token:", token);

                    return token ?? "";
                }
            })
            .withAutomaticReconnect()
            .build();

        console.log("Connection Created. Connectionid-", this.connection.connectionId);

        // this.connection.on("ReceiveNotification", (...args) => {

        //     console.log("RAW EVENT. Connectionid-", this.connection.connectionId);
        //     console.log("RAW EVENT", args);

        // });
    }

    public async start(): Promise<void> {

        console.log("SignalR Connectint in start(). ConnectionId-{}", this.connection.connectionId);
        if (this.connection.state === signalR.HubConnectionState.Disconnected) {

            await this.connection.start();
            
            console.log("SignalR Connected. ConnectionId-{}", this.connection.connectionId);
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

        console.log("onNotification. Registering ReceiveNotification", callback);

        this.connection.on(
            "ReceiveNotification",
            callback
        );

        // this.connection.on("ReceiveNotification", (...args) => {

        //     console.log("RAW EVENT. Connectionid-", this.connection.connectionId);
        //     console.log("RAW EVENT", args);

        // });
    }

    public offNotification(): void {

        this.connection.off("ReceiveNotification");
    }
}

export const signalRClient = new SignalRClient();