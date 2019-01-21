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
          './dashboard-content/dashboard-content.module#DashboardContentModule'
      },
      {
        path: 'survey',
        loadChildren:
          // './dashboard-views/survey-creator/survey-creator.module#SurveyCreatorModule'
          './../../shared/survey-container/survey-container.module#SurveyContainerModule'
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DashboardRoutingModule {}
