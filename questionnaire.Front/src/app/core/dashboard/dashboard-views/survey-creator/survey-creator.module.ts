import { ConfirmDialogModule } from './../../../../shared/confirm-dialog/confirm-dialog.module';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {
  MatCheckboxModule,
  MatRadioModule,
  MatTooltipModule,
  MatMenuModule,
  MatFormFieldModule,
  MatSelectModule,
  MatDialogModule,
  MatInputModule,
  MatButtonModule
} from '@angular/material';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ConfirmDialogComponent } from '../../../../shared/confirm-dialog/confirm-dialog.component';
import { MoveQuestionDialogComponent } from './move-question-dialog/move-question-dialog.component';
import { SendSurveyDialogComponent } from './send-survey-dialog/send-survey-dialog.component';
import { SurveyCreatorComponent } from './survey-creator.component';
import { SurveyCreatorRoutingModule } from './survey-creator.routing';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { ButtonSingleControlComponent } from './button-single-control/button-single-control.component';
import { SortablejsModule } from 'angular-sortablejs';

@NgModule({
  imports: [
    CommonModule,
    SurveyCreatorRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    MatRadioModule,
    MatCheckboxModule,
    FontAwesomeModule,
    MatTooltipModule,
    MatSlideToggleModule,
    MatMenuModule,
    MatFormFieldModule,
    MatSelectModule,
    ConfirmDialogModule,
    MatDialogModule,
    SortablejsModule,
    MatInputModule,
    MatButtonModule
  ],
  declarations: [
    SurveyCreatorComponent,
    SendSurveyDialogComponent,
    ButtonSingleControlComponent,
    MoveQuestionDialogComponent
  ],
  entryComponents: [ConfirmDialogComponent, MoveQuestionDialogComponent]
})
export class SurveyCreatorModule {}
