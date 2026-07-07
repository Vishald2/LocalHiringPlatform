import { useState } from "react";
import { sendMessage } from "../../services/AIChatService";
import JobSearchResults from "../AI/JobSearchResults";

import type { AIIntentHandlerResponse } from "../../types/AI/AIIntentHandlerResponse";
import type { JobSearchResultModel } from "../../types/AI/JobSearchResultModel";

import { useAIChat } from "./AIChatContext";

        export default function AIChatPage() {

            const  messages1  = useAIChat();

            console.log("AIChatPage");
            console.log(messages1);

            const [message, setMessage] = useState("");

            const {
                messages,
                setMessages
            } = useAIChat();

            const [loading, setLoading] = useState(false);

        const handleSend = async () => {

        if (!message.trim())
        return;

        const userMessage = message;

        setMessages(prev => [
            ...prev,
            {
            sender: "user",
            text: userMessage
            }
        ]);

        setMessage("");

        try {

            setLoading(true);

            try {

                const response =
                    await sendMessage({
                        message: userMessage
                    });

                console.log(response);

                setMessages(prev => [
                    ...prev,
                    {
                        sender: "assistant",
                        response: response
                    }
                ]);
            }
            catch (error) {
                console.log("Error sending message:", error);
                throw error;
            }
                finally {
                    setLoading(false);
                }
        }
        catch (error) {

        console.error(error);

            setMessages(prev => [
                ...prev,
                {
                sender: "assistant",
                text: "Sorry, something went wrong."
                }
            ]);
        }
        };

            const renderIntent = (
                item: AIIntentHandlerResponse,
                index: number
            ) => {

                switch (item.intent) {

                    case "Greeting":

                        return (
                            <div
                                key={index}
                                style={{ marginBottom: "15px" }}
                            >
                                {item.data as string}
                            </div>
                        );

                    case "JobSearch":

                        if (item.data == null) {

                            return (
                                <div
                                    key={index}
                                    className="chat-bubble ai-bubble"
                                >
                                    {item.message}
                                </div>
                            );
                        }

                        return (
                            <JobSearchResults
                                key={index}
                                jobs={item.data as JobSearchResultModel[]}
                            />
                        );

                    default:

                        return (
                            <pre key={index}>
                                {JSON.stringify(item.data, null, 2)}
                            </pre>
                        );
                }
            };

return (
<div className="page-container">

<div className="card">

<h2>LocalHire AI Assistant</h2>

<div style={{ marginTop: "20px" }}>

<div
style={{
minHeight: "300px",
border: "1px solid #ddd",
padding: "15px",
marginTop: "20px",
marginBottom: "20px",
overflowY: "auto"
}}
>

{
messages.map((m) => (

    <div
        className={
            m.sender === "user"
                ? "chat-message user-message"
                : "chat-message ai-message"
        }
    >
        <strong>
            {m.sender === "user"
                ? "You"
                : "AI"}
        </strong>

        <div>

            {
                m.sender === "user"
                    ? m.text
                    : <div>
                            {
                                m.response?.response.map((item, index) =>
                                    renderIntent(item, index)
                                )
                            }
                    </div>
            }

        </div>
    </div>
))
                    }
                    {
                        loading && (

                            <div
                                style={{
                                    marginBottom: "15px",
                                    fontStyle: "italic"
                                }}
                            >
                                AI is thinking...
                            </div>

                        )
                    }

</div>

                <textarea
                    rows={4}
                    value={message}
                    disabled={loading}
                    onChange={(e) => setMessage(e.target.value)}
                    onKeyDown={(e) => {

                        if (
                            e.key === "Enter" &&
                            !e.shiftKey &&
                            message.trim()
                        ) {

                            e.preventDefault();

                            handleSend();
                        }

                    }}
                    placeholder="Ask me anything..."
                    style={{ width: "100%" }}
                />

</div>

<div style={{ marginTop: "20px" }}>

    <button style={{ width: "100px" }}
        className="primary-button"
        onClick={handleSend}
        disabled={loading}
    >
        {
            loading
                ? "Thinking..."
                : "Send"
        }
    </button>
</div>

</div>

</div>
);
}