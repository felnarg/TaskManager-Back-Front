import { notificationPreference } from "../domains/enums/notificationPreferenceUser";
export interface IUser {
    id: string,
    name: string,
    password: string,
    email: string,
    notificationPreference: notificationPreference,
}