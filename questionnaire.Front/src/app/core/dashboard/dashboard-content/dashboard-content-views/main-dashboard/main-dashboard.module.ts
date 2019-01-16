import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MainDashboardComponent } from './main-dashboard.component';
import { MainDashboardRoutingModule } from './main-dashboard.routing';

@NgModule({
  declarations: [MainDashboardComponent],
  imports: [CommonModule, MainDashboardRoutingModule]
})
export class MainDashboardModule {}
