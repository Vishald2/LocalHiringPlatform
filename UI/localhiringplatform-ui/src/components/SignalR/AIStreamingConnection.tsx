import { useEffect } from "react";
import { aiHubClient } from "../../services/SignalR/StreamingHubClient"
export function AIStreamingConnection() {

    useEffect(() => {

        const hub = aiHubClient;// aiHub.current;

        const startConnection = async () => {

            await hub.start();

            console.log("AI Streaming Connection started, connectionId: ", hub.connectionId);
        };

        startConnection();

        return () => {

            hub.stop();

        };

    }, []);

    return null;
}