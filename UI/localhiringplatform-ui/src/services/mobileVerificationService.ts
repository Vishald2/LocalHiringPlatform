import { api } from "../infra/apiClient";

export interface VerifyMobileRequest {
    accessToken: string;
}

export const verifyMobile = async (
    request: VerifyMobileRequest
): Promise<void> => {

    await api.post(
        "/mobileverification/verify",
        request);
};