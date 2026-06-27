
import type { CreateJobApiRequest} from "../types/CreateJobApiRequest";
import { api } from "../infra/apiClient";
import { API_ENDPOINTS } from "../End_Points/apiEndpoints";
import type { Job } from "../types/Job";
import type { UpdateJobRequest } from "../types/UpdateJobRequest";
import type { SearchJobsRequest } from "../types/SearchJobsRequest";

function getBaseUrl() {
    return API_ENDPOINTS.job.root;
}

export async function addJob(
    request: CreateJobApiRequest) {

    await api.post(getBaseUrl(), request);
}

export async function getJobs() {

    console.log("calling -  const response = await api.get<Job[]>(`/candidate/profile/recommended-jobs`);");

    const response = await api.get<Job[]>(`/candidate/profile/recommended-jobs`);

    console.log("called -  const response = await api.get<Job[]>(`/candidate/profile/recommended-jobs`);");

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

export async function searchJobs(
    request: SearchJobsRequest) {

    const response =
        await api.post<Job[]>(
            "/job/search",
            request);

    return response.data;
}