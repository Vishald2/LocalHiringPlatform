import type { CandidateEducationCreateModel } from "../types/EducationModels/CandidateEducationCreateModel";
import type { CandidateEducationModel } from "../types/EducationModels/CandidateEducationModel";
import { api } from "../infra/apiClient";
import { API_ENDPOINTS } from "../End_Points/apiEndpoints";

import type { EducationModel } from "../types/EducationModels/EducationModel";

function getBaseUrl() {
    return API_ENDPOINTS.candidate.candidateeducation;
}

export async function getCandidateEducation(
    candidateEducationEntityId: string)
    : Promise<CandidateEducationCreateModel> {



    const response =
        await api.get<CandidateEducationCreateModel>(
            `${getBaseUrl()}/detail`,
            {
                params:
                {
                    candidateEducationEntityId
                }
            });

    return response.data;
}

export async function getAllEducations(): Promise<EducationModel[]> {

    const url = API_ENDPOINTS.education.root;

    const response =
        await api.get<EducationModel[]>(url);

    return response.data;
}

export async function getCandidateEducations(): Promise<CandidateEducationModel[]> {
    const response =
        await api.get<CandidateEducationModel[]>(getBaseUrl());

    return response.data;
}

export async function addCandidateEducation(
    education: CandidateEducationCreateModel) {

        
    console.log("addCandidateEducation-getBaseUrl()", getBaseUrl());
    console.log(education);

    await api.post(getBaseUrl(), education);
}

export async function updateCandidateEducation(
    education: CandidateEducationCreateModel) {

    console.log("updateCandidateEducation-url", getBaseUrl())
    console.log("education", education);

    await api.put(getBaseUrl(), education);
}

export async function deleteCandidateEducation(
    candidateEducationId: string) {

    await api.delete(
        `${getBaseUrl()}/${candidateEducationId}`);
}