import { useEffect, useRef } from "react";
import { useContext} from "react";
import { NotificationContext } from "../../context/NotificationContext";
import { NotificationHub } from "../../services/SignalR/NotificationHub";

export function SignalRConnection() {

    const {
        addNotification
    } = useContext(NotificationContext);

    const notificationHub = useRef(
        new NotificationHub()
    );

    useEffect(() => {

        const hub = notificationHub.current;

        if (!hub) {
            return;
        }

        const startConnection = async () => {

            await hub.start();

            hub.onNotification(notification => {
                console.log("Notification Received", notification);
                addNotification(notification);

            });
        };

        startConnection();

        return () => {

            hub.offNotification();

            hub.stop();

        };

    }, []);

    return null;
}