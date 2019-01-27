import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SurveyViewformComponent } from './survey-viewform.component';

const routes: Routes = [
  {
    path: '',
    component: SurveyViewformComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SurveyViewformRoutingModule {}
