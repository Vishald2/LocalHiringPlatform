export interface Applicant {

    jobApplicationId: string;

    candidateProfileId: string;

    candidateName: string;

    email: string;

    mobileNumber: string;

    appliedOn: string;

    status: string;

    jobTitle: string;

    resumeFileName?: string;

    resumeFilePath?: string;

    matchPercentage: number;

    hasAiAnalysis: boolean;

    aiMatchScore: number | null;

    skills: string[];
}