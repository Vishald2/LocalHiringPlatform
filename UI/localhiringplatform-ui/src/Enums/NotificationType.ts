export const NotificationTypes = {
    JobApplied: 1,
    NewMessage: 2,
    InterviewScheduled: 3,
    ProfileViewed: 4,
    ResumeShortlisted: 5,
    SystemNotification: 6,
} as const;

export type NotificationTypes = typeof NotificationTypes[keyof typeof NotificationTypes];