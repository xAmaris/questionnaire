import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { MatTabsModule } from '@angular/material';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { RouterModule, Routes } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { FileUploadModule } from 'primeng/components/fileupload/fileupload';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { MaterialsModule } from '../../../../../materials/materials.module';
import { ConfirmDialogComponent } from '../../../../../shared/confirm-dialog/confirm-dialog.component';
import { DashboardBarModule } from '../../../../../shared/dashboard/dashboard-bar/dashboard-bar.module';
import { DashboardListModule } from '../../../../../shared/dashboard/dashboard-list/dashboard-list.module';
import { IconButtonModule } from './../../../../../shared/buttons/icon-button/icon-button.module';
import { ConfirmDialogModule } from './../../../../../shared/confirm-dialog/confirm-dialog.module';
import { AddUserDialogComponent } from './add-user-dialog/add-user-dialog.component';
import { AddUserTabComponent } from './add-user-dialog/add-user-tab/add-user-tab.component';
import { ImportUserTabComponent } from './add-user-dialog/import-user-tab/import-user-tab.component';
import { UsersContentComponent } from './users-content.component';
import { UsersTileComponent } from './users-tile/users-tile.component';

export const routes: Routes = [{ path: '', component: UsersContentComponent }];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    DashboardBarModule,
    DashboardListModule,
    MaterialsModule,
    ReactiveFormsModule,
    ProgressSpinnerModule,
    MatDatepickerModule,
    MatTabsModule,
    FileUploadModule,
    FontAwesomeModule,
    MatProgressSpinnerModule,
    IconButtonModule,
    ConfirmDialogModule
  ],
  entryComponents: [ConfirmDialogComponent, AddUserDialogComponent, AddUserTabComponent],
  declarations: [
    UsersContentComponent,
    UsersTileComponent,
    AddUserDialogComponent,
    AddUserTabComponent,
    ImportUserTabComponent,
  ]
})
export class UsersContentModule {}
