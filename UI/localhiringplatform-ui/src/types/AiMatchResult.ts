
export interface AiMatchResult {
    score: number;

    recommendation: string;

    strengths: string[];

    gaps: string[];
}