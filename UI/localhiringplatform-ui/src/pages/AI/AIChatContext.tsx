import {
    createContext,
    useContext,
    useState,
    type ReactNode
} from "react";

import type { ChatMessage } from "../../types/AI/ChatMessage";

interface AIChatContextType {

    messages: ChatMessage[];

    setMessages: React.Dispatch<React.SetStateAction<ChatMessage[]>>;
}

const AIChatContext =
    createContext<AIChatContextType | undefined>(undefined);

export function AIChatProvider(
    { children }: { children: ReactNode }) {

    const [messages, setMessages] =
        useState<ChatMessage[]>([]);

    console.log("AIChatProvider Render");
    console.log(messages.length);

    return (
        <AIChatContext.Provider
            value={{
                messages,
                setMessages
            }}
        >
            {children}
        </AIChatContext.Provider>
    );
}

export function useAIChat() {

    const context =
        useContext(AIChatContext);

    if (!context)
        throw new Error("useAIChat must be used inside AIChatProvider");

    return context;
}