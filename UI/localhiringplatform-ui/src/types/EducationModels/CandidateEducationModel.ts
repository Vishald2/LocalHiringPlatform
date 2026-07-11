export interface CandidateEducationModel {
    entityId: string;

    educationId: number;

    educationName: string;

    code: string;

    courseId: number;

    courseName: string;

    universityId?: number;

    universityName?: string;

    instituteName?: string;

    startYear?: number;

    endYear?: number;

    percentage?: number;

    cgpa?: number;

    grade?: string;

    isCompleted: boolean;

    isHighestEducation: boolean;

    courseSpecializationIds: number[];

    specializationNames: string[];
}