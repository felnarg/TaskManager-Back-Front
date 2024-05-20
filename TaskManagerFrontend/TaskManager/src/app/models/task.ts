import { importance } from "../domains/enums/importanceTask";
import { statusTask } from "../domains/enums/status";


export interface ITask {
    id: string,
    userId: string,
    title: string,
    status: statusTask,
    importance: importance,
    expirationDate: Date,
    details: string
}

