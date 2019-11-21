import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Tasks } from '../_models/Tasks';

@Injectable({
  providedIn: 'root'
})

export class TasksService {
  baseURL = 'http://localhost:5000/api/tasks';

  constructor(private http: HttpClient) { }

  getAllTasks(): Observable<Tasks[]> {
    return this.http.get<Tasks[]>(this.baseURL);
  }

  getTasksByTitle(title: string): Observable<Tasks[]> {
    return this.http.get<Tasks[]>(`${this.baseURL}/getTaskByTitle/${title}`);
  }

  getTasksById(id: number): Observable<Tasks> {
    return this.http.get<Tasks>(`${this.baseURL}/${id}`);
  }

  postTask(task: Tasks) {
    return this.http.post(this.baseURL, task);
  }

  putTask(task: Tasks) {
    return this.http.put(`${this.baseURL}/${task.id}`, task);
  }

  deleteTask(id: number) {
    return this.http.delete(`${this.baseURL}/${id}`);
  }
}
