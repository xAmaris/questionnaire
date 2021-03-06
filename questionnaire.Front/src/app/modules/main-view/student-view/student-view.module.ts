import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { StudentViewComponent } from './student-view.component';
import { StudentViewRoutingModule } from './student-view.routing';

@NgModule({
  imports: [
    CommonModule,
    StudentViewRoutingModule
  ],
  declarations: [StudentViewComponent]
})
export class StudentViewModule { }
