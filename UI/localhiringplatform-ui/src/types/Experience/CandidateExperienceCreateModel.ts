export interface CandidateExperienceCreateModel {

    entityId?: string;

    companyName: string;

    designation: string;

    industryTypeId: number;

    city: string;

    state: string;

    country: string;

    startDate: string;

    endDate?: string;

    isCurrentCompany: boolean;

    summary: string;
}