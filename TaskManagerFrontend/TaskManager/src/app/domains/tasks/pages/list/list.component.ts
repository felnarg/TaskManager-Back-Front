import { Component, OnInit, computed, inject, signal } from '@angular/core';
import { TaskComponent } from '../../components/task/task.component';
import { NewTaskComponent } from '../../components/new-task/new-task.component';
import { UpdateTaskComponent } from '../../components/update-task/update-task.component';
import { CommonModule } from '@angular/common';
import { ITask } from '../../../../models/task';
import { MasterService } from '../../../../services/master.service';
import { importance } from '../../../enums/importanceTask';
import { IUser } from '../../../../models/user';
import { notificationPreference } from '../../../enums/notificationPreferenceUser';

export enum filterTaskEnum {
  low = 0,
  medium = 1,
  high = 2,
  all = 3
}

@Component({
  selector: 'app-list',
  standalone: true,
  imports: [TaskComponent, NewTaskComponent, UpdateTaskComponent, CommonModule],
  templateUrl: './list.component.html',
  styleUrl: './list.component.css'
})
export class ListComponent{  
  viewFormNewTask: boolean = false;
  viewImportanceList: boolean = false;
  tasks = signal<ITask[]>([]);  
  private masterService = inject(MasterService);

  ngOnInit(){
    this.getAllTasksList();
  }

  onTaskChanged(){
    console.log('obtener lista de tareas');
    this.getAllTasksList();
  }

  getAllTasksList(){
    this.masterService.getAllTasks()
    .subscribe(tasks => {
      this.tasks.set(tasks)
    },
    error => console.log(error))
  }

  userData:IUser = {
    id: '',
    name: '',
    password: '',
    email: '',
    notificationPreference: notificationPreference.OneDay
  };


  notificationPreferenceChange(opcion: any){
    this.userData.id = "60724175-253a-4ba3-832a-b0df4f467a2d";
    const enumOpcion = opcion.value === "0" ? notificationPreference.OneHour : 
      opcion.value === "1" ? notificationPreference.OneDay : notificationPreference.OneWeek
    this.userData.notificationPreference = enumOpcion;
    console.log(this.userData)
    this.masterService.NotificationUpdateUser(this.userData)
    .subscribe(
      (response) => {
        console.log('preferencia de notificaión actualizada con éxito:', response);
        // Puedes manejar la respuesta del servidor aquí, si es necesario
      },
      (error) => {
        console.error('Error al actualizar preferencia de notificación:', error);
        // Puedes manejar el error aquí, si es necesario
      }
    );
  }  
  
  updateTaskParent(event: string){
    console.log('aqui la logica para actualizar la tarea');
    console.log(event);
  }

  filter = signal('All');
  tasksByFilter = computed(()=>{
    const filter = this.filter();
    const tasks = this.tasks();

    if (filter === 'low'){
      return tasks.filter(task => task.importance === importance.low);
    }
    if (filter === 'medium'){
      return tasks.filter(task => task.importance === importance.medium);
    }
    if (filter === 'high'){
      return tasks.filter(task => task.importance === importance.high);
    }
    else {
      return tasks;
    }
  })
  changeFilter(filter: string){
    this.filter.set(filter);
  }
}
