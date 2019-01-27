import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { InfoComponent } from './info.component';

const infoRoutes: Routes = [
  {
    path: '',
    component: InfoComponent,
    children: [
      {
        path: 'student',
        loadChildren: './student/student.module#StudentModule'
      },
      {
        path: 'graduate',
        loadChildren: './graduate/graduate.module#GraduateModule'
      },
      {
        path: 'employer',
        loadChildren: './employer/employer.module#EmployerModule'
      },
      {
        path: 'contact',
        loadChildren: './contact/contact.module#ContactModule'
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(infoRoutes)],
  exports: [RouterModule]
})
export class InfoRoutingModule {}
