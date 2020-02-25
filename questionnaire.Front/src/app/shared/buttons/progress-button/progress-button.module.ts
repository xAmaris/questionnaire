import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { MaterialsModule } from '../../../core/materials/materials.module';
import { ProgressButtonComponent } from './progress-button.component';

@NgModule({
  imports: [CommonModule, ProgressSpinnerModule, MaterialsModule],
  declarations: [ProgressButtonComponent],
  exports: [ProgressButtonComponent]
})
export class ProgressButtonModule {}
