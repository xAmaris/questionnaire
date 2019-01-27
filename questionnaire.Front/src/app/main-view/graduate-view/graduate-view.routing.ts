import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GraduateViewComponent } from './graduate-view.component';

const graduateRoutes: Routes = [
  {
    path: '',
    component: GraduateViewComponent
  }
];


@NgModule({
  imports: [RouterModule.forChild(graduateRoutes)],
  exports: [RouterModule]
})
export class GraduateViewRoutingModule { }
