import {
  MatSidenavModule,
  MatMenuModule,
  MatButtonModule
} from '@angular/material';
import { DashboardRoutingModule } from './dashboard.routing';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard.component';
import { AccountService } from '../auth/auth-views/services/account.service';
import { UserInfoComponent } from './bar/user-info/user-info.component';
import { BarComponent } from './bar/bar.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

@NgModule({
  declarations: [DashboardComponent, BarComponent, UserInfoComponent],
  imports: [
    CommonModule,
    MatSidenavModule,
    DashboardRoutingModule,
    MatMenuModule,
    MatButtonModule,
    FontAwesomeModule
  ],
  providers: [AccountService]
})
export class DashboardModule {}
