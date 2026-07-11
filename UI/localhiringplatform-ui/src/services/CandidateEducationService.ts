import type { CandidateEducationCreateModel } from "../types/EducationModels/CandidateEducationCreateModel";
import type { CandidateEducationModel } from "../types/EducationModels/CandidateEducationModel";
import { api } from "../infra/apiClient";
import { API_ENDPOINTS } from "../End_Points/apiEndpoints";
import type { Education } from "../types/EducationModels/EducationModel";

function getBaseUrl() {
    return API_ENDPOINTS.candidate.candidateeducation;
}

export async function getAllEducations(): Promise<Education[]> {

    const url = API_ENDPOINTS.education.root;

    const response =
        await api.get<Education[]>(url);

    return response.data;
}

export async function getCandidateEducations(): Promise<CandidateEducationModel[]> {
    const response =
        await api.get<CandidateEducationModel[]>(getBaseUrl());

    return response.data;
}

export async function addCandidateEducation(
    education: CandidateEducationCreateModel) {

    await api.post(getBaseUrl(), education);
}

export async function updateCandidateEducation(
    education: CandidateEducationCreateModel) {

    await api.put(getBaseUrl(), education);
}

export async function deleteCandidateEducation(
    candidateEducationId: string) {

    await api.delete(
        `${getBaseUrl()}/${candidateEducationId}`);
}