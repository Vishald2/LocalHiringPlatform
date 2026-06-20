import { api } from "./api";
import type { EmployerDashboard } from "../types/EmployerDashboard";
import { API_ENDPOINTS } from "../End_Points/apiEndpoints";
import type { EmployerProfile } from "../types/EmployeeProfile";

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

export const getEmployerProfile =
    async (): Promise<EmployerProfile> => {

        const response =
            await api.get<EmployerProfile>(`EmployerDashboard/profile`);

        return response.data;
    };