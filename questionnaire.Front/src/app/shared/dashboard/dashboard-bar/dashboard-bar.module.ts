import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { MaterialsModule } from '../../../materials/materials.module';
import { DashboardBarComponent } from './dashboard-bar.component';

@NgModule({
  imports: [CommonModule, FontAwesomeModule, MaterialsModule],
  declarations: [DashboardBarComponent],
  exports: [DashboardBarComponent]
})
export class DashboardBarModule {}
