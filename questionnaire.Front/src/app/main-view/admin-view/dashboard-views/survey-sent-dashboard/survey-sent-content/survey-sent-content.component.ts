import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable, Subscription } from 'rxjs';
import { ConfirmDialogComponent } from '../../../../../shared/confirm-dialog/confirm-dialog.component';
import {
  SurveyModel,
  SurveySurvey
} from '../../../survey-container/models/survey.model';
import { SurveyService } from '../../../survey-container/services/survey.services';

@Component({
  selector: 'app-survey-sent-content',
  templateUrl: './survey-sent-content.component.html',
  styleUrls: ['./survey-sent-content.component.scss']
})
export class SurveySentContentComponent implements OnInit, OnDestroy {
  groupTitle = 'Grupa ankiet 1';
  emptyListInfo = 'Brak wysłanych ankiet';
  loading = false;
  surveyArr: SurveySurvey[];
  // subs
  getAllSurveysSub: Subscription = new Subscription();
  isLoadingSub: Subscription = new Subscription();

  private _items$: BehaviorSubject<SurveySurvey[]> = new BehaviorSubject<
    SurveySurvey[]
  >(undefined);
  get items$(): Observable<SurveySurvey[]> {
    return this._items$.asObservable();
  }
  constructor(
    private surveyService: SurveyService,
    private router: Router,
    public dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.getAllSurveys();
    this.isLoadingFromOutside();
  }

  saveSurveysFromApi(): void {
    this.surveyService.saveSentSurveysFromApi();
  }
  isLoadingFromOutside(): void {
    this.isLoadingSub = this.surveyService.openingCreatorLoader.subscribe(
      data => {
        this.loading = data;
      }
    );
  }
  getAllSurveys(): void {
    this.saveSurveysFromApi();
    this.getAllSurveysSub = this.surveyService.savedSentSurveys.subscribe(
      (data: SurveySurvey[]) => {
        if (data) {
          this._items$.next(data);
        }
      }
    );
  }
  openPreview(survey: SurveySurvey): void {
    this.surveyService.isCreatorLoading(true);
    this.router.navigateByUrl('/app/admin/survey/viewform/s/' + survey.id);
  }
  openResult(survey: SurveySurvey): void {
    this.router.navigateByUrl('/app/admin/survey/result/' + survey.id);
  }

  deleteSurvey(id: number): void {
    this.surveyService.deleteSurvey(id).subscribe(() => {
      this.saveSurveysFromApi();
    });
  }
  openConfimDeleteDialog(id: number): void {
    this.openSurveyDialog().subscribe((res: boolean) => {
      if (res === true) {
        this.deleteSurvey(id);
      }
    });
  }
  openSurveyDialog(): Observable<boolean> {
    const dialogRef: MatDialogRef<ConfirmDialogComponent> = this.dialog.open(
      ConfirmDialogComponent
    );
    return dialogRef.afterClosed();
  }
  changeSurveyToModel(item: SurveySurvey) {
    return new SurveyModel(item);
  }
  ngOnDestroy(): void {
    this.surveyService.isCreatorLoading(false);
  }
}
