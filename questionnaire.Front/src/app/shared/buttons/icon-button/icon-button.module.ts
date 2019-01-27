import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { MaterialsModule } from './../../../materials/materials.module';
import { IconButtonComponent } from './icon-button.component';

@NgModule({
  imports: [CommonModule, FontAwesomeModule, MaterialsModule],
  declarations: [IconButtonComponent],
  exports: [IconButtonComponent]
})
export class IconButtonModule {}
