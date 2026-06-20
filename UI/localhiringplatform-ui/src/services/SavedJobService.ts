
import { api } from "./api";
import type { Job } from "../types/Job";

export async function saveJob(jobId: string) {
    await api.post(`/savedjob/${jobId}`);
}

export async function getMySavedJobs() {
    const response = await api.get<Job[]>("/savedjob/my");

    return response.data;
}

export async function removeSavedJob(jobId: string) {
    await api.delete(`/savedjob/${jobId}`);
}