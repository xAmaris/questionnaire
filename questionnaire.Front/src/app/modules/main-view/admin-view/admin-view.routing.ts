import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from '../../../core/auth/other/guard.auth';
import { AdminViewComponent } from './admin-view.component';

const adminRoutes: Routes = [
  {
    path: '',
    component: AdminViewComponent,
    children: [
      {
        path: '',
        redirectTo: 'd',
        pathMatch: 'full'
      },
      {
        path: 'd',
        loadChildren:
          './../../../shared/dashboard/dashboard.module#DashboardModule',
        canLoad: [AuthGuard]
      },
      {
        path: 'survey',
        loadChildren:
          './../../../shared/survey-container/survey-container.module#SurveyContainerModule',
        canLoad: [AuthGuard]
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(adminRoutes)],
  exports: [RouterModule]
})
export class AdminViewRoutingModule {}
