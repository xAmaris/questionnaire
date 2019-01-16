import { DashboardBarComponent } from './dashboard-bar/dashboard-bar.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardContentComponent } from './dashboard-content.component';
import { DashboardContentRoutingModule } from './dashboard-content.routing';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import {
  MatTooltipModule,
  MatToolbarModule,
  MatButtonModule
} from '@angular/material';

@NgModule({
  declarations: [DashboardContentComponent, DashboardBarComponent],
  imports: [
    CommonModule,
    DashboardContentRoutingModule,
    FontAwesomeModule,
    MatTooltipModule,
    MatToolbarModule,
    MatButtonModule
  ]
})
export class DashboardContentModule {}
