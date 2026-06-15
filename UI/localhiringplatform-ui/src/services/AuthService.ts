import { API_BASE_URL } from "../config/api";
import type { LoginRequest } from "../types/LoginRequest";
import type { LoginResponse } from "../types/LoginResponse";

import type { RegisterCandidateApiRequest } from "../types/RegisterCandidateApiRequest";
import { api } from "./api";

export async function registerCandidate(
    request: RegisterCandidateApiRequest
): Promise<void> {

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
            await response.json();

        throw new Error(errorText.message);
    }
}

export async function login(
    request: LoginRequest
): Promise<LoginResponse> {

    const response = await fetch(
        `${API_BASE_URL}/api/auth/login`,
        {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(request)
        });

    if (!response.ok) {

        const error =
            await response.json();

        throw new Error(error.message);
    }

    return await response.json();
}

export async function verifyEmail(
    token: string) {

    await api.get(`/auth/verify-email?token=${token}`);
}