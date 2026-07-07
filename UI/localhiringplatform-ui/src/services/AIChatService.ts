import { api } from "../infra/apiClient";
import { API_ENDPOINTS } from "../End_Points/apiEndpoints";

import type { AIChatRequest } from "../types/AI/AIChatRequest";
import type { AIChatResponse } from "../types/AI/AIChatResponse";

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