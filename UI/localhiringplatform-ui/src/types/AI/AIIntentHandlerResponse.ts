import type { AIIntentType } from "./AIIntentType";

export interface AIIntentHandlerResponse {
    intent: AIIntentType;
    data: unknown;
}