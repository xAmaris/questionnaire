import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SurveyResultComponent } from './survey-result.component';

const surveyResultRoutes: Routes = [
  {
    path: '',
    component: SurveyResultComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(surveyResultRoutes)],
  exports: [RouterModule]
})
export class SurveyResultRoutingModule {}
