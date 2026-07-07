export const AIIntentType = {
    Unknown: "Unknown",
    Greeting: "Greeting",
    JobSearch: "JobSearch",
    CandidateQuestion: "CandidateQuestion",
    EmployerQuestion: "EmployerQuestion",
} as const;

export type AIIntentType =
    typeof AIIntentType[keyof typeof AIIntentType];