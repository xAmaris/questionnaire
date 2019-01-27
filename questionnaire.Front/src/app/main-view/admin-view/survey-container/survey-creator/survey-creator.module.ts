import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {
  MatCheckboxModule,
  MatRadioModule,
  MatSlideToggleModule
} from '@angular/material';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { SortablejsModule } from 'angular-sortablejs/dist';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { MaterialsModule } from '../../../../materials/materials.module';
import { ConfirmDialogComponent } from '../../../../shared/confirm-dialog/confirm-dialog.component';
import { ConfirmDialogModule } from './../../../../shared/confirm-dialog/confirm-dialog.module';
import { MoveQuestionDialogComponent } from './move-question-dialog/move-question-dialog.component';
import { ButtonSingleControlComponent } from './survey-creator-component/button-single-control/button-single-control.component';
import { SurveyCreatorComponent } from './survey-creator.component';
import { SurveyCreatorRoutingModule } from './survey-creator.routing';

@NgModule({
  imports: [
    CommonModule,
    SurveyCreatorRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    MaterialsModule,
    MatRadioModule,
    MatCheckboxModule,
    FontAwesomeModule,
    ProgressSpinnerModule,
    MatSlideToggleModule,
    SortablejsModule,
    ConfirmDialogModule
  ],
  declarations: [
    SurveyCreatorComponent,
    ButtonSingleControlComponent,
    MoveQuestionDialogComponent
  ],
  entryComponents: [ConfirmDialogComponent, MoveQuestionDialogComponent]
})
export class SurveyCreatorModule {}
