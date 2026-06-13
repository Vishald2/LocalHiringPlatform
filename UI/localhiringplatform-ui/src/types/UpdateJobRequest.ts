export interface UpdateJobRequest {
    entityId: string;
    title: string;
    description: string;
    city: string;
    state: string;
    minSalary?: number;
    maxSalary?: number;
    experienceRequired: number;
    requiredSkills?: string;
    isActive: boolean;
}