import { useEffect, useRef } from "react";
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

            // hub.onMessage(message => {

            //     console.log("AI Message:", message);

            // });
        };

        startConnection();

        return () => {

            hub.offMessage();

            hub.stop();

        };

    }, []);

    return null;
}