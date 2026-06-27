import { API_BASE_URL } from "../config/api";
import type { ChangePasswordRequest } from "../types/ChangePasswordRequest";
import type { LoginRequest } from "../types/LoginRequest";
import type { LoginResponse } from "../types/LoginResponse";

import type { RegisterCandidateApiRequest } from "../types/RegisterCandidateApiRequest";
import { api } from "../infra/apiClient";

export async function registerCandidate(
    request: RegisterCandidateApiRequest
): Promise<void> {

        await api.post(
        "/auth/register-candidate",
        request);
}

export async function login(
    request: LoginRequest
): Promise<LoginResponse> {

    console.log("in service");
    console.log(import.meta.env);
    console.log(import.meta.env.VITE_API_BASE_URL);

    console.log({ API_BASE_URL });

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

export async function changePassword(
    request: ChangePasswordRequest) {
    await api.post(
        "/auth/change-password",
        request);
}