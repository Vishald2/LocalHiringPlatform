import { useEffect } from "react";
//import { StreamingHubClient } from "../../services/SignalR/StreamingHubClient";
import { aiHubClient } from "../../services/SignalR/StreamingHubClient"
export function AIStreamingConnection() {

    // const aiHub = useRef(
    //     new StreamingHubClient()
    // );

    useEffect(() => {

        const hub = aiHubClient;// aiHub.current;

        const startConnection = async () => {

            await hub.start();

            console.log("AI Streaming Connection started, connectionId: ", hub.connectionId);

            // hub.onMessage(message => {

            //     console.log("AI Message:", message);

            // });
        };

        startConnection();

        return () => {

            hub.stop();

        };

    }, []);

    return null;
}