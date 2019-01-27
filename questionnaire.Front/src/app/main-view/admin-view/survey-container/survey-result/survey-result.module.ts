import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ChartModule } from 'primeng/chart';
import { SurveyResultComponent } from './survey-result.component';
import { SurveyResultRoutingModule } from './survey-result.routing';

@NgModule({
  imports: [CommonModule, SurveyResultRoutingModule, ChartModule],
  declarations: [SurveyResultComponent]
})
export class SurveyResultModule {}
