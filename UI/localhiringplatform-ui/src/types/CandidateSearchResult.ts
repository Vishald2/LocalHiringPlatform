export interface CandidateSearchResult {
    candidateProfileId: string;

    fullName: string;

    email: string;

    mobileNumber: string;

    city: string;

    totalExperienceYears: number;

    resumeFileName?: string;

    resumeFilePath?: string;

    isOpenToWork: boolean;
}