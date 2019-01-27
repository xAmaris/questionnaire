import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { ProgressBarModule } from 'primeng/progressbar';
import { MaterialsModule } from '../../materials/materials.module';
import { RestorePasswordComponent } from './restore-password.component';

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
