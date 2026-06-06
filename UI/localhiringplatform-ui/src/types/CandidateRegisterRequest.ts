export interface CandidateRegisterRequest {
    email: string;
    mobileNumber: string;
    password: string;
    confirmPassword: string;
    acceptTerms: boolean;
    role: number
}