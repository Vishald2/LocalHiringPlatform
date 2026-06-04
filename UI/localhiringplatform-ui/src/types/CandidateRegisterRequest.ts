export interface CandidateRegisterRequest {
    fullName: string;
    email: string;
    mobileNumber: string;
    password: string;
    confirmPassword: string;
    acceptTerms: boolean;
}