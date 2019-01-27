import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { StudentViewComponent } from './student-view.component';

const studentRoutes: Routes = [
  {
    path: '',
    component: StudentViewComponent
  }
];


@NgModule({
  imports: [RouterModule.forChild(studentRoutes)],
  exports: [RouterModule]
})
export class StudentViewRoutingModule {}
