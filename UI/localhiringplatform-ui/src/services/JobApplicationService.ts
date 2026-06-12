import { api } from "./api";
import { API_ENDPOINTS } from "../End_Points/apiEndpoints";

import type { JobApplicationRequest } from "../types/JobApplicationRequest";
import type { MyApplication } from "../types/MyApplication";
import type { Applicant } from "../types/Applicant";

import type { UpdateApplicationStatusRequest } from "../types/UpdateApplicationStatusRequest";

function getBaseUrl() {
    return API_ENDPOINTS.jobApplication.employer.updatestatus;
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
        await api.get<Applicant[]>(`${API_ENDPOINTS.jobApplication.root}/employer/my`);

    return response.data;
}

export async function updateApplicationStatus(
    request: UpdateApplicationStatusRequest) {
    await api.put(`${API_ENDPOINTS.jobApplication.root}/status`, request);
}