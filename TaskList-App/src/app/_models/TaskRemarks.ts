import { Tasks } from './Tasks';

export interface TaskRemarks {
    id: number;
    tasksId: number;
    insertDateTime: Date;
    remarks: string;
    tasks: Tasks;
}
