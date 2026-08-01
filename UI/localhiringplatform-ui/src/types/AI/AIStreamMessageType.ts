export const AIStreamMessageType = {

    Token: 0,

    Status: 1,

    Progress: 2,

    Completed: 3,

    Error: 4

} as const;

export type AIStreamMessageType =
    typeof AIStreamMessageType[keyof typeof AIStreamMessageType];