import type { AIIntentType } from "./AIIntentType";

export interface AIIntentHandlerResponse {
    intent: AIIntentType;
    tag: string;
    message: string;
    data: unknown;
}