import { useEffect, useRef } from "react";
// import { useContext} from "react";
// import { NotificationContext } from "../../context/NotificationContext";
import { SignalRClient } from "../../services/SignalR/SignalRClient";

export function SignalRConnection() {

    // const {
    //     addNotification
    // } = useContext(NotificationContext);

    const notificationHub = useRef(
        new SignalRClient()
    );

    useEffect(() => {

        const hub = notificationHub.current;

        if (!hub) {
            return;
        }

        // const startConnection = async () => {

        //  //   await hub.start();

        //     console.log("Adding Callback function");

        //     hub.onNotification(notification => {
        //         console.log("Notification Received, ConnectionId-", notification, notificationHub.current);
        //         addNotification(notification);

        //     });
        // };

        // startConnection();

        return () => {

            hub.offNotification();

            hub.stop();

        };

    }, []);

    return null;
}