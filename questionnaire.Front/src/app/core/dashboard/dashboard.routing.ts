import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DashboardComponent } from './dashboard.component';

const routes: Routes = [
  {
    path: '',
    component: DashboardComponent,
    children: [
      {
        path: '',
        loadChildren:
          './dashboard-content/dashboard-content-views/main-dashboard/main-dashboard.module#MainDashboardModule'
      },
      {
        path: 'd',
        loadChildren:
          './dashboard-content/dashboard-content.module#DashboardContentModule'
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DashboardRoutingModule {}
