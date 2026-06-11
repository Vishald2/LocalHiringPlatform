import type { ApplyToJobRequest } from "../types/ApplyToJobRequest";
import { api } from "./api";
import { API_ENDPOINTS } from "../End_Points/apiEndpoints";
import type { MyApplication } from "../types/MyApplication";
import type { Applicant } from "../types/Applicant";

function getBaseUrl() {
    return API_ENDPOINTS.jobApplication.root;
}

export async function applyToJob(
    request: ApplyToJobRequest) {

    console.log(request);

    await api.post(getBaseUrl(), request);
}


export async function getMyApplications() {
    const response =
        await api.get<MyApplication[]>("/jobapplication/my");

    return response.data;
}

export async function getApplicants(
    jobId: string) {
    const response =
        await api.get<Applicant[]>(
            `/jobapplication/job/${jobId}`);

    return response.data;
}