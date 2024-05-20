import { Component, EventEmitter, Input, OnInit, Output, inject } from '@angular/core';
import { UpdateTaskComponent } from '../update-task/update-task.component';
import { ITask } from '../../../../models/task';
import { CommonModule } from '@angular/common';
import { MasterService } from '../../../../services/master.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { statusTask } from '../../../enums/status';

@Component({
  selector: 'app-task',
  standalone: true,
  imports: [UpdateTaskComponent, CommonModule],
  templateUrl: './task.component.html',
  styleUrl: './task.component.css'
})
export class TaskComponent{  
  private masterService = inject(MasterService);  
  viewFormUpdateTask: boolean = false;
  isCheckboxChecked: boolean = false;
  @Input({required: true}) task!: ITask;
  @Output() updateTask = new EventEmitter();
  @Output() taskChanged = new EventEmitter();

  updateTaskHandler(){
    this.updateTask.emit('este es un mensaje desde el hijo al padre ' + this.task.title);
    console.log(this.viewFormUpdateTask);
    this.viewFormUpdateTask = !this.viewFormUpdateTask;
    console.log(this.viewFormUpdateTask);
  }

  emitUpdateTask(){
    this.taskChanged.emit();
  }

  DeleteTaskHandler(){
    this.masterService.deleteTask(this.task.id)
    .subscribe(
      (response) => {
        console.log('Eliminación exitosa de la tarea {task.title}:', response);
        this.taskChanged.emit();
      },
      (error) => {
        console.error('Error al eliminar la tarea: {task.title}', error);
      }
    );
  }
  
  toggleCheckbox(): void{
    this.masterService.changeStatusTask(this.task.id)
      .subscribe(
        (response) => {
          console.log('Cambio de estado exitoso de la tarea {task.title}:', response);
          this.taskChanged.emit();
        },
        (error) => {
          console.error('Error al cambiar de estado la tarea: {task.title}', error);
        }
      );
  }
}
