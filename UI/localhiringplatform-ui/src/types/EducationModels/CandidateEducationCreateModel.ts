export interface CandidateEducationCreateModel {
    entityId: string | null;

    educationId: number;

    courseId: number;

    universityId?: number;

    instituteName?: string;

    city?: string;

    state?: string;

    country?: string;

    startYear?: number;

    endYear?: number;

    percentage?: number;

    cgpa?: number;

    grade?: string;

    isCompleted: boolean;

    isHighestEducation: boolean;

    courseSpecializationIds: number[];
}