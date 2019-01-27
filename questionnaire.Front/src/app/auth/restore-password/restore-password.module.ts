import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RestorePasswordComponent } from './restore-password.component';
import { Routes, RouterModule } from '@angular/router';
import { MaterialsModule } from '../../materials/materials.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ProgressBarModule } from 'primeng/progressbar';

export const routes: Routes = [
  { path: '', component: RestorePasswordComponent }
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    MaterialsModule,
    FormsModule,
    ReactiveFormsModule,
    ProgressBarModule
  ],
  declarations: [RestorePasswordComponent]
})
export class RestorePasswordModule {}
