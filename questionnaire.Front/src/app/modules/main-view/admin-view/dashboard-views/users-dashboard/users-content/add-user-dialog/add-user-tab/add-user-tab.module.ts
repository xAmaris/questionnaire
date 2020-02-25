import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { MaterialsModule } from '../../../../../../../../core/materials/materials.module';
import { AddUserTabComponent } from './add-user-tab.component';

@NgModule({
  imports: [
    CommonModule,
    MaterialsModule,
    ReactiveFormsModule,
    ProgressSpinnerModule,
    MatDatepickerModule
  ],
  declarations: [AddUserTabComponent]
})
export class AddUserTabModule {}
