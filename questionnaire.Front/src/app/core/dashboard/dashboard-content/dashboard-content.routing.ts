import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DashboardContentComponent } from './dashboard-content.component';

const routes: Routes = [
  {
    path: '',
    component: DashboardContentComponent,
    children: [
      // {
      //   path: '',
      //   loadChildren:
      //     './dashboard-content-views/main-dashboard/main-dashboard.module#MainDashboardModule'
      // },
      {
        path: '',
        redirectTo: 'survey',
        pathMatch: 'full'
      },
      {
        path: 'survey',
        loadChildren:
          './dashboard-content-views/survey-dashboard/survey-dashboard.module#SurveyDashboardModule'
      },
      {
        path: 'sent',
        loadChildren:
          './dashboard-content-views/survey-sent-dashboard/survey-sent-dashboard.module#SurveySentDashboardModule'
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DashboardContentRoutingModule {}
