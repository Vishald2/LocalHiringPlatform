import { api } from "../../infra/apiClient";

import { API_ENDPOINTS }
    from "../../End_Points/apiEndpoints";

import type { UniversityResponseModel }
    from "../../types/EducationModels/UniversityResponseModel";

function getBaseUrl() {

    return API_ENDPOINTS.University.root;
}

export async function getAllUniversities()
    : Promise<UniversityResponseModel[]> {

    const response =
        await api.get<UniversityResponseModel[]>(
            getBaseUrl());

    return response.data;
}

export async function searchUniversities(
    searchText: string)
    : Promise<UniversityResponseModel[]> {

    const response =
        await api.get<UniversityResponseModel[]>(
            `${getBaseUrl()}/search`,
            {
                params:
                {
                    searchText
                }
            });

    return response.data;
}