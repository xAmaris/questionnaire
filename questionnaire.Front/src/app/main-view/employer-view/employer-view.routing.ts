import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EmployerViewComponent } from './employer-view.component';

const employerRoutes: Routes = [
  {
    path: '',
    component: EmployerViewComponent
  }
];


@NgModule({
  imports: [RouterModule.forChild(employerRoutes)],
  exports: [RouterModule]
})
export class EmployerViewRoutingModule { }
