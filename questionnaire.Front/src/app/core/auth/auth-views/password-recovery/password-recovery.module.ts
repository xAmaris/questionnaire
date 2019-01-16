import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { PasswordRecoveryComponent } from './password-recovery.component';
import { PasswordRecoveryRoutingModule } from './password-recovery.routing';
import {
  MatInputModule,
  MatFormFieldModule,
  MatButtonModule
} from '@angular/material';

export const routes: Routes = [
  { path: '', component: PasswordRecoveryComponent }
];

@NgModule({
  imports: [
    RouterModule.forChild(routes),
    ReactiveFormsModule,
    CommonModule,
    PasswordRecoveryRoutingModule,
    ReactiveFormsModule,
    MatInputModule,
    MatFormFieldModule,
    MatButtonModule
  ],
  declarations: [PasswordRecoveryComponent],
  exports: [PasswordRecoveryComponent]
})
export class PasswordRecoveryModule {}
