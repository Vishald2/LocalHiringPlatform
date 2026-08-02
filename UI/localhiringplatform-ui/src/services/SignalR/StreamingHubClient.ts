import * as signalR from "@microsoft/signalr";
import { API_BASE_URL } from "../../config/api";
import type { AIStreamMessage } from "../../types/AI/AIStreamMessage";
import { HubConnectionState } from "@microsoft/signalr";

export class StreamingHubClient {

    private connection: signalR.HubConnection;

    constructor() {

        this.connection = new signalR.HubConnectionBuilder()
            .withUrl(`${API_BASE_URL}/aiHub`, {
                accessTokenFactory: () =>
                    localStorage.getItem("token") ?? ""
            })
            .withAutomaticReconnect()
            .build();
    }

    public get connectionId(): string | null {
        return this.connection.connectionId;
    }

    public async start(): Promise<void> {

        if (this.connection.state === signalR.HubConnectionState.Disconnected) {

            console.log("Streaming. API_BASE_URL=", API_BASE_URL);

            await this.connection.start();

            console.log("AI Hub Connected");
        }
    }

    public async stop(): Promise<void> {

        if (this.connection.state !== signalR.HubConnectionState.Disconnected) {

            await this.connection.stop();
        }
    }

    public async ensureConnected(): Promise<void> {

        if (this.connection.state === HubConnectionState.Disconnected) {

            await this.start();
        }

        if (!this.connection.connectionId) {

            throw new Error("Streaming hub is not connected.");
        }
    }

    public onMessage(
        callback: (message: AIStreamMessage) => void) {

        console.log(callback);

        this.connection.on(
            "ReceiveAIMessage",
            callback);
    }

    public offMessage(
        callback: (message: AIStreamMessage) => void) {
        this.connection.off("ReceiveAIMessage", callback);
    }
}

export const aiHubClient = new StreamingHubClient();