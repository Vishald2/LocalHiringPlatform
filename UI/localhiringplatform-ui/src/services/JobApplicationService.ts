import { api } from "./api";
import { API_ENDPOINTS } from "../End_Points/apiEndpoints";

import type { JobApplicationRequest } from "../types/JobApplicationRequest";
import type { MyApplication } from "../types/MyApplication";
import type { Applicant } from "../types/Applicant";

function getBaseUrl() {
    return API_ENDPOINTS.jobApplication.employer.applicants;
}

export async function applyToJob(
    request: JobApplicationRequest) {

    console.log(request);
    console.log(getBaseUrl());

    await api.post(getBaseUrl(), request);
}


export async function getMyApplications() {
    const response =
        await api.get<MyApplication[]>("/jobapplication/employer/my");

    return response.data;
}

export async function getApplicants(
    jobId: string) {
    const response =
        await api.get<Applicant[]>(`/jobapplication/job/${jobId}`);

    return response.data;
}

export async function getApplicantsByEmployer() {
    const response =
        await api.get<Applicant[]>(getBaseUrl());

    return response.data;
}