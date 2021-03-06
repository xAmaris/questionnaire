import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from '../../core/auth/other/guard.auth';
import { RoleGuard } from '../../core/auth/other/role.auth';
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
