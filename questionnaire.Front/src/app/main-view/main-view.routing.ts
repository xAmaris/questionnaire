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
