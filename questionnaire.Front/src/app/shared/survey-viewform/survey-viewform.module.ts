import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {
  MatCheckboxModule,
  MatRadioModule,
  MatFormFieldModule,
  MatButtonModule,
  MatInputModule,
  MatSelectModule
} from '@angular/material';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { SurveySentComponent } from './survey-sent/survey-sent.component';
import { SurveyViewformComponent } from './survey-viewform.component';
import { SurveyViewformRoutingModule } from './survey-viewform.routing';

@NgModule({
  imports: [
    CommonModule,
    SurveyViewformRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    MatFormFieldModule,
    MatButtonModule,
    MatInputModule,
    MatSelectModule,
    MatRadioModule,
    MatCheckboxModule,
    FontAwesomeModule
  ],
  declarations: [SurveyViewformComponent, SurveySentComponent],
  exports: [SurveyViewformComponent]
})
export class SurveyViewformModule {}
