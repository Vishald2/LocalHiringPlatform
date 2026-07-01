import type { CandidateProfile } from "../types/CandidateProfile";
import { api } from "../infra/apiClient";
import { API_ENDPOINTS } from "../End_Points/apiEndpoints";

function getBaseUrl() {
    return API_ENDPOINTS.candidate.profile;
}

export async function getProfile(): Promise<CandidateProfile> {
    const response =
        await api.get<CandidateProfile>(getBaseUrl());

    console.log(response);

    return response.data;
}

export async function updateProfile(
    profile: CandidateProfile) {

    await api.put(getBaseUrl(), profile);
}

export async function uploadResume(
    file: File) {
    const formData =
        new FormData();

    formData.append(
        "resume",
        file);

    await api.post(
        `${API_ENDPOINTS.candidate.profile}/resume`,
        formData,
        {
            headers:
            {
                "Content-Type": "multipart/form-data"
            }
        });
}