import { Component, EventEmitter, Input, OnInit, Output, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MasterService } from '../../../../services/master.service';
import { ITask } from '../../../../models/task';
import { ReactiveFormsModule } from '@angular/forms';

enum statusTask {
  activa = 0,
  inactiva = 1
}
enum importanceTask {
  low = 0,
  medium = 1,
  high = 2
}

@Component({
  selector: 'app-update-task',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './update-task.component.html',
  styleUrl: './update-task.component.css'
})
export class UpdateTaskComponent implements OnInit{
  private masterService = inject(MasterService); 
  updateTaskForm:FormGroup;
  @Input() viewFormUpdateTask: boolean = false;
  @Input() updateTaskData!: ITask;
  @Output() updateTaskChanged = new EventEmitter<void>();

  constructor(private formBuilder: FormBuilder) { 
    this.updateTaskForm = this.formBuilder.group({
    })
  }
  ngOnInit(): void {
    this.updateTaskForm = this.formBuilder.group({
      id: '',
      userId:'',
      title:'',
      details:'',
      expirationDate:new Date(),
      status:0,
      importance:0
    });
  }
    onSubmitUpdateTask() {
      const importanceValue = this.updateTaskForm.value.importance === 'baja' ? importanceTask.low : 
                       this.updateTaskForm.value.importance === 'media' ? importanceTask.medium : 
                       importanceTask.high;   
      this.updateTaskForm.patchValue({
        id: this.updateTaskData.id,
        status: this.updateTaskData.status,
        importance: importanceValue
      });
      console.log(this.updateTaskForm.value);
      this.masterService.UpdateTask(this.updateTaskForm.value)
        .subscribe(
          (response) => {
            console.log('Tarea actualizada con éxito:', response);
            this.updateTaskChanged.emit();
            this.updateTaskForm.reset();
            // Puedes manejar la respuesta del servidor aquí, si es necesario
          },
          (error) => {
            console.error('Error al actualizar tarea:', error);
            // Puedes manejar el error aquí, si es necesario
          }
        );
    }
  } 

