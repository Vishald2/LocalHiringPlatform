
import type { CreateJobApiRequest} from "../types/CreateJobApiRequest";
import { api } from "./api";
import { API_ENDPOINTS } from "../End_Points/apiEndpoints";
import type { Job } from "../types/Job";
import type { UpdateJobRequest } from "../types/UpdateJobRequest";

function getBaseUrl() {
    return API_ENDPOINTS.job.root;
}

export async function addJob(
    request: CreateJobApiRequest) {

    await api.post(getBaseUrl(), request);
}

export async function getJobs() {
    const response = await api.get<Job[]>(getBaseUrl());

    return response.data;
}

export async function getMyJobs() {

    const response =
        await api.get<Job[]>(
            `${getBaseUrl()}/myjobs`);

    return response.data;
}

export async function getJobById(
    id: string) {
    const response =
        await api.get<Job>(
            `${getBaseUrl()}/${id}`);

    return response.data;
}

export async function updateJob(
    request: UpdateJobRequest) {
    await api.put(getBaseUrl(),request);
}