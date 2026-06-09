import { API_BASE_URL } from "../config/api";

import type {
    CreateJobApiRequest
} from "../types/CreateJobApiRequest";

export async function addJob(
    request: CreateJobApiRequest) {
    const token =
        localStorage.getItem("token");

    const response =
        await fetch(
            `${API_BASE_URL}/api/job`,
            {
                method: "POST",
                headers:
                {
                    "Content-Type":
                        "application/json",

                    "Authorization":
                        `Bearer ${token}`
                },
                body:
                    JSON.stringify(
                        request)
            });

    if (!response.ok) {
        throw new Error(
            "Unable to create job");
    }
}