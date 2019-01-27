import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatSidenavModule } from '@angular/material';
import { DashboardComponent } from './dashboard.component';
import { DashboardRoutingModule } from './dashboard.routing';

@NgModule({
  imports: [CommonModule, DashboardRoutingModule, MatSidenavModule],
  declarations: [DashboardComponent],
  exports: [DashboardComponent]
})
export class DashboardModule {}
