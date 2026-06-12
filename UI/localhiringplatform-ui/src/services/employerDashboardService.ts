import { api } from "./api";
import type { EmployerDashboard } from "../types/EmployerDashboard";
import { API_ENDPOINTS } from "../End_Points/apiEndpoints";

function getBaseUrl() {
    return API_ENDPOINTS.employer.dashboard;
}

export const getEmployerDashboard =
    async (): Promise<EmployerDashboard> => {

        const response =
            await api.get<EmployerDashboard>(
                getBaseUrl());

        return response.data;
    };