import { SurveySentDashboardListComponent } from './survey-sent-dashboard-list/survey-sent-dashboard-list.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SurveySentDashboardComponent } from './survey-sent-dashboard.component';
import { ListModule } from 'src/app/shared/list/list.module';
// tslint:disable-next-line:max-line-length
import { SurveySentDashboardListItemComponent } from './survey-sent-dashboard-list/survey-sent-dashboard-list-item/survey-sent-dashboard-list-item.component';
import { SurveySentDashboardRoutingModule } from './survey-sent-dashboard.routing';

@NgModule({
  declarations: [
    SurveySentDashboardComponent,
    SurveySentDashboardListComponent,
    SurveySentDashboardListItemComponent
  ],
  imports: [CommonModule, ListModule, SurveySentDashboardRoutingModule]
})
export class SurveySentDashboardModule {}
