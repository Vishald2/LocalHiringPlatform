import type { NotificationTypes } from "../../Enums/NotificationType";

export interface NotificationModel {
    entityId: string;
    type: NotificationTypes; // Assuming NotificationType is an enum or union type defined elsewhere
    title: string;
    message: string;
    isRead: boolean;
    createdOn: Date | string; // Use Date if parsed, or string if raw ISO timestamp from JSON
}