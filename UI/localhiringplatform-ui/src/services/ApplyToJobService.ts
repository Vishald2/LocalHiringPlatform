
import type { ApplyToJobRequest } from "../types/ApplyToJobRequest";
import { api } from "./api";
import { API_ENDPOINTS } from "../End_Points/apiEndpoints";

function getBaseUrl() {
    return API_ENDPOINTS.jobApplication.root;
}

export async function applyToJob(
    request: ApplyToJobRequest) {

    console.log(request);

    await api.post(getBaseUrl(), request);

}