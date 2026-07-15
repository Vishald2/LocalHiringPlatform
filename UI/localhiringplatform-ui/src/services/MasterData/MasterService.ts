import { API_ENDPOINTS } from "../../End_Points/apiEndpoints";
import { api } from "../../infra/apiClient";

import type { IndustryTypeResponseModel } from "../../types/IndustryTypeResponseModel";


function getBaseUrl() {

    return API_ENDPOINTS.IndustryTypes.root;
}


export async function getIndustryTypes():
    Promise<IndustryTypeResponseModel[]> {

    const response =
        await api.get<IndustryTypeResponseModel[]>(
            `${getBaseUrl()}`);

    return response.data;
}