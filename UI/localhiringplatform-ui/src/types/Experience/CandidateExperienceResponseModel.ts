export interface CandidateExperienceResponseModel {

    entityId: string;

    companyName: string;

    designation: string;

    industryTypeId: number;

    industryTypeName: string;

    city: string;

    state: string;

    country: string;

    startDate: string;

    endDate?: string;

    isCurrentCompany: boolean;

    summary: string;
}