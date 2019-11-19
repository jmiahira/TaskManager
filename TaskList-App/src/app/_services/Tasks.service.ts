import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Tasks } from '../_models/Tasks';

@Injectable({
  providedIn: 'root'
})

export class TasksService {
  baseURL = 'http://localhost:5000/api/tasks'

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


}
