export type  CandidateProfile = {
    fullName: string;
    dateOfBirth: string | null;
    gender: number | null;
    city: string;
    state: string;
    currentSalary: number | null;
    expectedSalary: number | null;
    totalExperienceYears: number | null;
    profileSummary: string;
    isOpenToWork: boolean;
    profileCompletionPercentage: number;
    resumeFileName?: string;
    resumeFilePath?: string;
    emailVerified: boolean;
    mobileVerified: boolean;
    mobileNumber: string;
    email: string;
}