import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { ITask } from '../models/task';
import { HttpHeaders } from '@angular/common/http';
import { IUser } from '../models/user';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':  'application/json',
    Authorization: 'my-auth-token'
  })
};

@Injectable({
  providedIn: 'root'
})

export class MasterService {  

  private http = inject(HttpClient);  

  apiUrl: string = 'https://localhost:7273/Task/';

  getAllTasks(): Observable<ITask[]>{
    return this.http.get<ITask[]>(this.apiUrl + 'gettasks');
  }

  createTask(taskData: ITask): Observable<any> {
    return this.http.post<any>(this.apiUrl + 'createtask', taskData, httpOptions);
  }

  changeStatusTask(taskId: string): Observable<any>{
    return this.http.put<any>(this.apiUrl + 'changestatus/' + taskId, null, httpOptions);
  }

  UpdateTask(taskData: ITask): Observable<any> {
    return this.http.put<any>(this.apiUrl + 'update', taskData, httpOptions);
  }

  deleteTask(taskId: string): Observable<any> {
    return this.http.post<any>(this.apiUrl + 'delete/' + taskId, null, httpOptions);
  }

  NotificationUpdateUser(userData: IUser): Observable<any> {
    return this.http.put<any>('https://localhost:7273/user/notificationpreference', userData, httpOptions);
  }
}
