import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SurveyCompletedComponent } from './survey-completed.component';

export const routes: Routes = [
  { path: '', component: SurveyCompletedComponent }
];

@NgModule({
  imports: [CommonModule, RouterModule.forChild(routes)],
  declarations: [SurveyCompletedComponent],
  exports: [SurveyCompletedComponent]
})
export class SurveyCompletedModule {}
