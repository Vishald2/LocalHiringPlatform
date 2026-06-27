import { api } from "../infra/apiClient";
import { API_ENDPOINTS } from "../End_Points/apiEndpoints";
import type { CandidateDashboardResponse } from "../types/CandidateDashboardResponse";

function getBaseUrl() {
    return API_ENDPOINTS.candidate.CandidateDashboard;
}

export const getCandidateDashboard = async (): Promise<CandidateDashboardResponse> => {

    console.log(getBaseUrl());

    const response = await api.get<CandidateDashboardResponse>(getBaseUrl());
    return response.data;
}