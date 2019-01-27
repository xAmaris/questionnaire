import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ConfirmDialogComponent } from '../../../../../shared/confirm-dialog/confirm-dialog.component';
import { DashboardBarModule } from '../../../../../shared/dashboard/dashboard-bar/dashboard-bar.module';
import { DashboardListModule } from '../../../../../shared/dashboard/dashboard-list/dashboard-list.module';
import { IconButtonModule } from './../../../../../shared/buttons/icon-button/icon-button.module';
import { ConfirmDialogModule } from './../../../../../shared/confirm-dialog/confirm-dialog.module';
import { SurveyContentComponent } from './survey-content.component';
import { SurveyTileComponent } from './survey-tile/survey-tile.component';

export const routes: Routes = [{ path: '', component: SurveyContentComponent }];

@NgModule({
  imports: [
    CommonModule,
    DashboardBarModule,
    DashboardListModule,
    RouterModule.forChild(routes),
    IconButtonModule,
    ConfirmDialogModule
  ],
  entryComponents: [ConfirmDialogComponent],
  declarations: [SurveyContentComponent, SurveyTileComponent]
})
export class SurveyContentModule {}
