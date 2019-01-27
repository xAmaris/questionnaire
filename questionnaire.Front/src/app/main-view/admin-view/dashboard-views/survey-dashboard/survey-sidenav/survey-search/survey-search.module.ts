import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatToolbarModule } from '@angular/material/toolbar';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { MaterialsModule } from '../../../../../../materials/materials.module';
import { SurveySearchComponent } from './survey-search.component';

@NgModule({
  imports: [CommonModule, MatToolbarModule, FontAwesomeModule, MaterialsModule],
  declarations: [SurveySearchComponent],
  exports: [SurveySearchComponent]
})
export class SurveySearchModule {}
