import { Component, OnInit } from '@angular/core';
import { TasksService } from '../_services/Tasks.service';
import { Tasks } from '../_models/Tasks';
import { BsModalService } from 'ngx-bootstrap';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { defineLocale, BsLocaleService, ptBrLocale } from 'ngx-bootstrap';
import { ToastrService } from 'ngx-toastr';

defineLocale('pt-br', ptBrLocale);

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.css']
})

export class TasksComponent implements OnInit {
  titulo = 'My Tasks';
  tasks: Tasks[];
  task: Tasks;
  filteredTasks: Tasks[];
  registerForm: FormGroup;
  modeNewEdit = 'post';
  bodyDeleteTask = '';

  _taskFilter = '';
  constructor(
      private tasksService: TasksService
    , private modalService: BsModalService
    , private fb: FormBuilder
    , private localeService: BsLocaleService
    , private toastr: ToastrService
  ) {
    this.localeService.use('pt-br');
  }

  get taskFilter(): string {
    return this._taskFilter;
  }
  set taskFilter(value: string) {
    this._taskFilter = value;
    this.filteredTasks = this.taskFilter ? this.filterTasks(this.taskFilter) : this.tasks;
  }

  editTask(task: Tasks, template: any) {
    this.modeNewEdit = 'put';
    this.openModal(template);
    this.task = task;
    this.registerForm.patchValue(task);
  }

  newTask(template: any) {
    this.modeNewEdit = 'post';
    this.openModal(template);
  }

  openModal(template: any) {
    this.registerForm.reset();
    template.show();
  }

  filterTasks(filterBy: string): Tasks[] {
    filterBy = filterBy.toLocaleLowerCase();
    return this.tasks.filter(
      task => task.title.toLocaleLowerCase().indexOf(filterBy) !== -1
    );
  }

  ngOnInit() {
    this.validation();
    this.getTasks();
  }

  validation() {
    this.registerForm = this.fb.group({
      title: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      description: ['', Validators.maxLength(150)],
      userId: ['', Validators.required],
      creationDate: ['', Validators.required],
      status: ['', Validators.required],
      priority: ['', Validators.required],
      lastUpdateDateTime: ['', Validators.required]
    });
  }

  saveChanges(template: any) {
    if (this.registerForm.valid) {
      if (this.modeNewEdit === 'post') {
        this.task = Object.assign({}, this.registerForm.value);
        this.tasksService.postTask(this.task).subscribe (
          (newTask: Tasks) => {
            console.log(newTask);
            template.hide();
            this.getTasks();
            this.toastr.success('Task inserted successful');
          }, error => {
            this.toastr.success(`Error during insert operation: ${error}`);
            console.log(error);
          }
        );
      } else {
        this.task = Object.assign({id: this.task.id}, this.registerForm.value);
        this.tasksService.putTask(this.task).subscribe (
          () => {
            template.hide();
            this.getTasks();
            this.toastr.success('Task edited successful');
          }, error => {
            this.toastr.success(`Error during update operation: ${error}`);
            console.log(error);
          }
        );
      }
    }
  }

  getTasks() {
    this.tasksService.getAllTasks().subscribe(
      (_tasks: Tasks[]) => {
      this.tasks = _tasks;
      this.filteredTasks = this.tasks;
    }, error => {
      console.log(error);
    });
  }

  confirmDelete(template: any) {
    this.tasksService.deleteTask(this.task.id).subscribe(
      () => {
        template.hide();
        this.getTasks();
        this.toastr.success('Deleted successful');
      }, error => {
        this.toastr.error(`Error during delete operation: ${error}`);
        console.log(error);
      }
    );
  }

  deleteTask(task: Tasks, template: any) {
    this.openModal(template);
    this.task = task;
    this.bodyDeleteTask = `Are you sure to delete the task: ${task.title}, Code: ${task.id} ?`;
  }
}
