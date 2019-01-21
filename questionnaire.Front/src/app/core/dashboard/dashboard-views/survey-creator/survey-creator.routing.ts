import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SurveyCreatorComponent } from './survey-creator.component';

const surveyCreatorRoutes: Routes = [
  {
    path: '',
    component: SurveyCreatorComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(surveyCreatorRoutes)],
  exports: [RouterModule]
})
export class SurveyCreatorRoutingModule {}
