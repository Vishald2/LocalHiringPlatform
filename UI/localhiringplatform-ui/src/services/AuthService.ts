import { API_BASE_URL } from "../config/api";

import type { RegisterCandidateApiRequest } from "../types/RegisterCandidateApiRequest";

export async function registerCandidate(
    request: RegisterCandidateApiRequest
): Promise<void> {


    console.log(`${API_BASE_URL}/api/auth/register-candidate`);
    const response = await fetch(
        `${API_BASE_URL}/api/auth/register-candidate`,
        {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(request)
        });

    if (!response.ok) {

        const errorText =
            await response.text();

        throw new Error(errorText);
    }
}