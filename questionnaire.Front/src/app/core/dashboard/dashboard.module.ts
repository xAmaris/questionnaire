import { MatSidenavModule } from '@angular/material';
import { DashboardRoutingModule } from './dashboard.routing';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard.component';

@NgModule({
  declarations: [DashboardComponent],
  imports: [CommonModule, MatSidenavModule, DashboardRoutingModule]
})
export class DashboardModule {}
