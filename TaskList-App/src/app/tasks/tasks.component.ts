import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.css']
})
export class TasksComponent implements OnInit {

  tasks: any;

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getTasks();
  }

  getTasks() {
    this.http.get('http://localhost:5000/api/values').subscribe( response => {
      this.tasks = response;
    }, error => {
      console.log(error);
    });
  }

}
