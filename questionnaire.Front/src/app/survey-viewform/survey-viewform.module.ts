import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {
  MatCheckboxModule,
  MatRadioModule,
  MatSnackBarModule
} from '@angular/material';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { MaterialsModule } from '../materials/materials.module';
import { SurveyCompletedComponent } from './survey-completed/survey-completed.component';
import { SurveyCompletedModule } from './survey-completed/survey-completed.module';
import { SurveyViewformComponent } from './survey-viewform.component';
import { SurveyViewformRoutingModule } from './survey-viewform.routing';

@NgModule({
  imports: [
    CommonModule,
    SurveyViewformRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    MaterialsModule,
    MatRadioModule,
    MatCheckboxModule,
    FontAwesomeModule,
    MatSnackBarModule,
    SurveyCompletedModule
  ],
  entryComponents: [SurveyCompletedComponent],
  declarations: [SurveyViewformComponent],
  exports: [SurveyViewformComponent]
})
export class SurveyViewformModule {}
