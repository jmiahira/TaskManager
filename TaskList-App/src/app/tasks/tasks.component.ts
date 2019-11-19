import { Component, OnInit, TemplateRef } from '@angular/core';
import { TasksService } from '../_services/Tasks.service';
import { Tasks } from '../_models/Tasks';
import { BsModalService, BsModalRef } from 'ngx-bootstrap';


@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.css']
})
export class TasksComponent implements OnInit {
  tasks: Tasks[];
  filteredTasks: Tasks[];
  modalRef: BsModalRef;

  _taskFilter: string;
  constructor(
      private tasksService: TasksService
    , private modalService: BsModalService
  ) { }

  get taskFilter(): string {
    return this._taskFilter;
  }
  set taskFilter(value: string) {
    this._taskFilter = value;
    this.filteredTasks = this._taskFilter ? this.filterTasks(this._taskFilter) : this.tasks;
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  ngOnInit() {
    this.getTasks();
  }

  filterTasks(filterBy: string): Tasks[] {
    filterBy = filterBy.toLocaleLowerCase();
    return this.tasks.filter(
      task => task.title.toLocaleLowerCase().indexOf(filterBy) !== -1
    );
  }

  getTasks() {
    this.tasksService.getAllTasks().subscribe(
      (_tasks: Tasks[]) => {
      this.tasks = _tasks;
    }, error => {
      console.log(error);
    });
  }
}
