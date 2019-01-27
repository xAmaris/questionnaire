import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from '../auth/other/guard.auth';
import { RoleGuard } from '../auth/other/role.auth';
import { MainViewComponent } from './main-view.component';

const mainRoutes: Routes = [
  {
    path: '',
    component: MainViewComponent,
    children: [
      {
        path: 'admin',
        loadChildren: './admin-view/admin-view.module#AdminViewModule',
        canLoad: [AuthGuard],
        canActivate: [RoleGuard],
        data: {
          expectedRole: 'careerOffice'
        }
      },
      {
        path: 'student',
        loadChildren: './student-view/student-view.module#StudentViewModule',
        canLoad: [AuthGuard],
        canActivate: [RoleGuard],
        data: {
          expectedRole: 'student'
        }
      },
      {
        path: 'graduate',
        loadChildren: './graduate-view/graduate-view.module#GraduateViewModule',
        canLoad: [AuthGuard],
        canActivate: [RoleGuard],
        data: {
          expectedRole: 'graduate'
        }
      },
      {
        path: 'employer',
        loadChildren: './employer-view/employer-view.module#EmployerViewModule',
        canLoad: [AuthGuard],
        canActivate: [RoleGuard],
        data: {
          expectedRole: 'employer'
        }
      },
      {
        path: 'settings',
        loadChildren: './settings/settings.module#SettingsModule',
        canLoad: [AuthGuard]
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(mainRoutes)],
  exports: [RouterModule]
})
export class MainViewRoutingModule {}
