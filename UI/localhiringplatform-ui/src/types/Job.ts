export interface Job {
    entityId: string;

    title: string;

    description: string;

    city: string;

    state: string;

    minSalary?: number;

    maxSalary?: number;

    experienceRequired: number;

    maxExperienceRequired: number;

    requiredSkills?: string;

    isActive: boolean;

    applicantCount?: number;
}