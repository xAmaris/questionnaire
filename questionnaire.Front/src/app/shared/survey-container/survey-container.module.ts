import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { SurveyCreatorResolver } from '../../main-view/admin-view/survey-container/resolvers/survey-creator.resolver';
import { SurveyResultResolver } from '../../main-view/admin-view/survey-container/resolvers/survey-result.resolver';
import { SurveyContainerComponent } from './survey-container.component';
import { SurveyContainerRoutingModule } from './survey-container.routing';

@NgModule({
  imports: [CommonModule, SurveyContainerRoutingModule],
  declarations: [SurveyContainerComponent],
  providers: [SurveyCreatorResolver, SurveyResultResolver]
})
export class SurveyContainerModule {}
