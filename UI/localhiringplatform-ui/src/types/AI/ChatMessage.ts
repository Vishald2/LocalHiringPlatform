import type { AIChatResponse } from "./AIChatResponse";

export interface ChatMessage {

    sender: "user" | "assistant";

    text?: string;

    response?: AIChatResponse;
}