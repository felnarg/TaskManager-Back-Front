import { Component, EventEmitter, Input, OnInit, Output, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MasterService } from '../../../../services/master.service';
import { FormBuilder, FormGroup, FormsModule } from '@angular/forms';
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
  selector: 'app-new-task',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './new-task.component.html',
  styleUrl: './new-task.component.css'
})
export class NewTaskComponent implements OnInit{
  newTaskForm:FormGroup;
  @Output() newTaskChanged = new EventEmitter<void>();

  constructor(private formBuilder: FormBuilder) { 
    this.newTaskForm = this.formBuilder.group({
    })
  }
  ngOnInit(): void {
    this.newTaskForm = this.formBuilder.group({
      userId:'',
      title:'',
      details:'',
      expirationDate:new Date(),
      status:0,
      importance:0
    });
  } 
  @Input() viewFormNewTask: boolean = false;
  private masterService = inject(MasterService);

  onSubmitCreateTask() {
    const statusValue = this.newTaskForm.value.status === 'activa' ? statusTask.activa : statusTask.inactiva; 
    const importanceValue = this.newTaskForm.value.importance === 'baja' ? importanceTask.low : 
                     this.newTaskForm.value.importance === 'media' ? importanceTask.medium : 
                     importanceTask.high;   
    this.newTaskForm.patchValue({
      status: statusValue,
      importance: importanceValue
    });
    this.masterService.createTask(this.newTaskForm.value)
      .subscribe(
        (response) => {
          console.log('Tarea creada con éxito:', response);
          this.newTaskChanged.emit();
          this.newTaskForm.reset();
          // Puedes manejar la respuesta del servidor aquí, si es necesario
        },
        (error) => {
          console.error('Error al crear tarea:', error);
          // Puedes manejar el error aquí, si es necesario
        }
      );
  }
}
