import { API_BASE_URL } from "../config/api";
import type { ApplyToJobRequest } from "../types/ApplyToJobRequest";

export async function applyToJob(
    request: ApplyToJobRequest) {
    const token =
        localStorage.getItem("token");

    const response =
        await fetch(
            `${API_BASE_URL}/api/jobapplication`,
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
            "Unable to apply");
    }
}