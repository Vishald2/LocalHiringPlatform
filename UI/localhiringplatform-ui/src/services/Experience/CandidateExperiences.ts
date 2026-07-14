import axios from "axios";

import { API_ENDPOINTS }
    from "../../End_Points/apiEndpoints";

import type { CandidateExperienceResponseModel } from "../../types/Experience/CandidateExperienceResponseModel";
import type { CandidateExperienceCreateModel } from "../../types/Experience/CandidateExperienceCreateModel";
import { api } from "../../infra/apiClient";

function getBaseUrl() {

    return API_ENDPOINTS.CandidateExperience.root;
}

export async function getCandidateExperiences():
    Promise<CandidateExperienceResponseModel[]> {

    console.log(`${getBaseUrl()}`);

    const response =
        await api.get<CandidateExperienceResponseModel[]>(
            `${getBaseUrl()}`);

    return response.data;
}

export async function getCandidateExperience(
    candidateExperienceEntityId: string):
    Promise<CandidateExperienceCreateModel> {

    const response = await axios.get<CandidateExperienceCreateModel>(
        `${getBaseUrl()}/candidateexperience/detail`,
        {
            params: {
                candidateExperienceEntityId
            }
        });

    return response.data;
}

export async function addCandidateExperience(
    model: CandidateExperienceCreateModel): Promise<void> {

    await axios.post(
        `${getBaseUrl()}/candidateexperience`,
        model);
}

export async function updateCandidateExperience(
    model: CandidateExperienceCreateModel): Promise<void> {

    await axios.put(
        `${getBaseUrl()}/candidateexperience`,
        model);
}

export async function deleteCandidateExperience(
    candidateExperienceEntityId: string): Promise<void> {

    await axios.delete(
        `${getBaseUrl()}/candidateexperience/${candidateExperienceEntityId}`);
}