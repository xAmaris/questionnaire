import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SurveyDashboardComponent } from './survey-dashboard.component';

const routes: Routes = [
  {
    path: '',
    component: SurveyDashboardComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SurveyDashboardRoutingModule {}
