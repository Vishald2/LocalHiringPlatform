import type { CandidateProfile } from "../types/CandidateProfile";
import { api } from "./api";
import { API_ENDPOINTS } from "../End_Points/apiEndpoints";

function getBaseUrl() {
    return API_ENDPOINTS.candidate.profile;
}

export async function getProfile() {
    const response = await api.get(getBaseUrl());

    return response.data;
}

export async function updateProfile(
    profile: CandidateProfile) {

    await api.put(getBaseUrl(), profile);
}