import { api } from "./api";
import { API_ENDPOINTS } from "../End_Points/apiEndpoints";

import type { JobApplicationRequest } from "../types/JobApplicationRequest";
import type { MyApplication } from "../types/MyApplication";
import type { Applicant } from "../types/Applicant";

import type { UpdateApplicationStatusRequest } from "../types/UpdateApplicationStatusRequest";
import { API_BASE_URL } from "../config/api";
import type { AiMatchResult } from "../types/AiMatchResult";

function getBaseUrl() {
    return API_ENDPOINTS.jobApplication.root;
}

export async function applyToJob(
    request: JobApplicationRequest) {

    console.log(request);
    console.log(getBaseUrl());

    await api.post(getBaseUrl(), request);
}


export async function getMyApplications() {
    const response =
        await api.get<MyApplication[]>("/jobapplication/candidate/my");

    return response.data;
}

export const getApplicantsByJobId = async (
    jobId: string
): Promise<Applicant[]> => {

    const response = await api.get<Applicant[]>(
        `${getBaseUrl()}/job/${jobId}`
    );

    return response.data;
};

export async function getApplicantsByEmployer() {
    const response =
        await api.get<Applicant[]>(`${API_ENDPOINTS.jobApplication.root}/employer/my`);

    return response.data;
}

export async function updateApplicationStatus(
    request: UpdateApplicationStatusRequest) {
    await api.put(`${API_ENDPOINTS.jobApplication.root}/status`, request);
}

export async function getAiAnalysis(
    jobId: string,
    candidateProfileId: string
): Promise<AiMatchResult> {

    try {

        const response = await api.get<AiMatchResult>(
                `/GeminiTest/match?jobId=${jobId}&candidateProfileId=${candidateProfileId}`);

        return response.data;

    } catch (error) {

        console.error(error);

        throw error;
    }
}