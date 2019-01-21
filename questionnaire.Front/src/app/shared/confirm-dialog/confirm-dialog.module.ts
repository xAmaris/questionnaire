import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ConfirmDialogComponent } from './confirm-dialog.component';
import { MatButtonModule, MatDialogModule } from '@angular/material';

@NgModule({
  imports: [CommonModule, MatButtonModule, MatDialogModule],
  declarations: [ConfirmDialogComponent],
  exports: [ConfirmDialogComponent]
})
export class ConfirmDialogModule {}
