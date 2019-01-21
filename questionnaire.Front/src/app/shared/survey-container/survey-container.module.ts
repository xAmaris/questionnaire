import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { SurveyContainerComponent } from './survey-container.component';
import { SurveyContainerRoutingModule } from './survey-container.routing';
import { SurveyCreatorResolver } from 'src/app/core/dashboard/dashboard-views/resolvers/survey-creator.resolver';
import { SurveyResultResolver } from 'src/app/core/dashboard/dashboard-views/resolvers/survey-result.resolver';

@NgModule({
  imports: [CommonModule, SurveyContainerRoutingModule],
  declarations: [SurveyContainerComponent],
  providers: [SurveyCreatorResolver, SurveyResultResolver]
})
export class SurveyContainerModule {}
