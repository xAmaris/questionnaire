import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MaterialsModule } from '../../materials/materials.module';
import { ConfirmDialogComponent } from './confirm-dialog.component';

@NgModule({
  imports: [CommonModule, MaterialsModule],
  declarations: [ConfirmDialogComponent],
  exports: [ConfirmDialogComponent]
})
export class ConfirmDialogModule {}
