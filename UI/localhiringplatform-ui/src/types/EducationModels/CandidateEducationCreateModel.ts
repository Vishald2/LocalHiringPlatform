export interface CandidateEducationCreateModel {
    entityId: string;

    educationId: number;

    courseId: number;

    universityId?: number;

    instituteName?: string;

    startYear?: number;

    endYear?: number;

    percentage?: number;

    cgpa?: number;

    grade?: string;

    isCompleted: boolean;

    isHighestEducation: boolean;

    courseSpecializationIds: number[];
}