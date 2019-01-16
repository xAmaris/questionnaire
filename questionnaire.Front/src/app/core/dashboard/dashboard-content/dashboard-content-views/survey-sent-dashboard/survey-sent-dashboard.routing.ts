import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SurveySentDashboardComponent } from './survey-sent-dashboard.component';

const routes: Routes = [
  {
    path: '',
    component: SurveySentDashboardComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SurveySentDashboardRoutingModule {}
