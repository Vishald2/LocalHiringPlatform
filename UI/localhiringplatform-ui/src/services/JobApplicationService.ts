import { api } from "../infra/apiClient";
import { API_ENDPOINTS } from "../End_Points/apiEndpoints";

import type { JobApplicationRequest } from "../types/JobApplicationRequest";
import type { MyApplication } from "../types/MyApplication";
import type { Applicant } from "../types/Applicant";

import type { UpdateApplicationStatusRequest } from "../types/UpdateApplicationStatusRequest";
import type { AiMatchResult } from "../types/AiMatchResult";

export async function applyToJob(
    request: JobApplicationRequest) {

    const endpoint = API_ENDPOINTS.jobApplication.root;

    await api.post(endpoint, request);
}

export async function getMyApplications() {

    const endpoint =
        API_ENDPOINTS.jobApplication.root + "/candidate/my";

    const response =
        await api.get<MyApplication[]>(endpoint);

    return response.data;
}

export const getApplicantsByJobId = async (
    jobId: string
): Promise<Applicant[]> => {

    const endpoint = API_ENDPOINTS.jobApplication.GetApplicantsByJobId.replace(':jobId', jobId);

    const response = await api.get<Applicant[]>(endpoint);

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
    candidateProfileId: string,
    reanalyse: boolean = false
): Promise<AiMatchResult> {

    try {

        const response = await api.get<AiMatchResult>(
            `/GeminiTest/match?jobId=${jobId}
                &candidateProfileId=${candidateProfileId}
                &reanalyse=${reanalyse}`);

        return response.data;

    } catch (error) {

        console.error(error);

        throw error;
    }
}