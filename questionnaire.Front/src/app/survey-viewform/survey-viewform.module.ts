import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {
  MatCheckboxModule,
  MatRadioModule
} from '@angular/material';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { MaterialsModule } from '../materials/materials.module';
import { SurveySentComponent } from './survey-sent/survey-sent.component';
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
    FontAwesomeModule
  ],
  declarations: [SurveyViewformComponent, SurveySentComponent],
  exports: [SurveyViewformComponent]
})
export class SurveyViewformModule {}
