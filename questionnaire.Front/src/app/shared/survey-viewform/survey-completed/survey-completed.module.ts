import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { Routes } from '@angular/router';
import { SurveyCompletedComponent } from './survey-completed.component';

export const routes: Routes = [{ path: '', component: SurveyCompletedComponent }];

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [SurveyCompletedComponent]
})
export class SurveyCompletedModule { }
