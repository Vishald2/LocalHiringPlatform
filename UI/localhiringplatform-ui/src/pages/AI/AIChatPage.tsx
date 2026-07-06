import { useState } from "react";
import { sendMessage } from "../../services/AIChatService";
import JobSearchResults from "../AI/JobSearchResults";

import type { AIChatResponse } from "../../types/AI/AIChatResponse";
import type { Job } from "../../types/Job";
// import "../css/aichat.css";
import type { AIIntentHandlerResponse } from "../../types/AI/AIIntentHandlerResponse";

        interface ChatMessage {
        sender: "user" | "assistant";

        text?: string;

        response?: AIChatResponse;
        }

        export default function AIChatPage() {

        const [message, setMessage] = useState("");
            const [messages, setMessages] = useState<ChatMessage[]>([]);

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

                        return (
                            <JobSearchResults
                                key={index}
                                jobs={item.data as Job[]}
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
messages.map((m, index) => (

    <div
        key={index}
        style={{
            textAlign:
                m.sender === "user"
                    ? "right"
                    : "left",
            marginBottom: "15px"
        }}
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
onChange={(e) => setMessage(e.target.value)}
placeholder="Ask me anything..."
style={{ width: "100%" }}
/>

</div>

<div style={{ marginTop: "20px" }}>

                <button
                    className="btn btn-primary"
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