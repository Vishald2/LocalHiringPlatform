import { api } from "../infra/apiClient";
import { API_ENDPOINTS } from "../End_Points/apiEndpoints";

import type { AIChatRequest } from "../types/AI/AIChatRequest";
import type { AIChatResponse } from "../types/AI/AIChatResponse";
import { aiHubClient } from "../services/SignalR/StreamingHubClient"

function getBaseUrl() {
    return API_ENDPOINTS.aichat.root;
}

export async function sendMessage(
    request: AIChatRequest) {

    const response =
        await api.post<AIChatResponse>(
            getBaseUrl(),
            request);

    return response.data;
}

export async function sendMessageToStreamingHub(userMessage: string) {

    const connectionID = aiHubClient.connectionId;

    console.log("Sending message to streaming hub. ConnectionID:", connectionID, "Message:", userMessage);

    await api.post("/AIChat/stream", {
        message: userMessage,
        connectionId: connectionID
    });
}