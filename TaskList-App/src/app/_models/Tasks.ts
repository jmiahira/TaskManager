import { StatusTaskType } from './StatusTaskType.enum';
import { PriorityTaskType } from './PriorityTaskType.enum';
import { TaskRemarks } from './TaskRemarks';

export interface Tasks {
    id: number;
    title: string;
    description: string;
    creationDate: Date;
    status: StatusTaskType;
    userId: number;
    priority: PriorityTaskType;
    lastUpdateDateTime?: Date;
    taskRemarks: TaskRemarks[];
}
