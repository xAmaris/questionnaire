import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { MaterialsModule } from '../../../../../../../core/materials/materials.module';
import { GroupListComponent } from './group-list.component';

@NgModule({
  imports: [
    CommonModule,
    FontAwesomeModule,
    MaterialsModule,
  ],
  declarations: [GroupListComponent],
  exports: [GroupListComponent]
})
export class GroupListModule {}
