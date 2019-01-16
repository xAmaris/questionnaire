import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SurveyDashboardComponent } from './survey-dashboard.component';
import { ListModule } from 'src/app/shared/list/list.module';
import { SurveyListComponent } from './survey-list/survey-list.component';
import { SurveyListItemComponent } from './survey-list/survey-list-item/survey-list-item.component';
import { SurveyDashboardRoutingModule } from './survey-dashboard.routing';

@NgModule({
  declarations: [
    SurveyDashboardComponent,
    SurveyListComponent,
    SurveyListItemComponent
  ],
  imports: [
    CommonModule,
    FontAwesomeModule,
    ListModule,
    SurveyDashboardRoutingModule
  ]
})
export class SurveyDashboardModule {}
