import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { Observable } from 'rxjs/Observable';
import { Subscription } from 'rxjs/Subscription';
import { DeleteTemplateDialog } from '../../../../../data/shared.data';
import { ConfirmDialogComponent } from '../../../../../shared/confirm-dialog/confirm-dialog.component';
import {
  SurveyModel,
  SurveyTemplate
} from '../../../survey-container/models/survey.model';
import { SurveyService } from '../../../survey-container/services/survey.services';

@Component({
  selector: 'app-survey-content',
  templateUrl: './survey-content.component.html',
  styleUrls: ['./survey-content.component.scss']
})
export class SurveyContentComponent implements OnInit, OnDestroy {
  groupTitle = 'Szablony';
  buttonDets = 'Stwórz nowy szablon';
  emptyListInfo = 'Brak szablonów';
  loading = false;
  surveyArr: SurveyTemplate[];
  // subs
  getAllSurveysSub: Subscription = new Subscription();
  isLoadingSub: Subscription = new Subscription();

  private _items$: BehaviorSubject<SurveyTemplate[]> = new BehaviorSubject<
    SurveyTemplate[]
  >(undefined);
  get items$(): Observable<SurveyTemplate[]> {
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
    this.filterSurveyList();
  }

  redirectToNew(): void {
    this.surveyService.isCreatorLoading(true);
    const obj = {
      title: '',
      questions: []
    };
    this.surveyService.createSurvey(obj).subscribe(
      data => {
        const string: string = '/app/admin/survey/create/' + data;
        this.router.navigateByUrl(string);
      },
      error => {
        console.log(error);
      }
    );
  }

  saveSurveysFromApi(): void {
    this.surveyService.saveSurveysFromApi();
  }
  filterSurveyList(): void {
    this.surveyService.filterSurveyListInput.subscribe(data => {
      // this.surveyArr.filter(filtered => console.log(filtered));
      this.surveyArr.filter(sth => {
        console.log(data);
        console.log(sth);
      });
    });
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
    this.getAllSurveysSub = this.surveyService.savedSurveys.subscribe(
      (data: SurveyTemplate[]) => {
        if (data) {
          this._items$.next(data);
        }
      },
      error => {
        console.log(error);
      }
    );
  }
  openCreator(survey: SurveyTemplate): void {
    this.surveyService.isCreatorLoading(true);
    this.router.navigateByUrl('/app/admin/survey/create/' + survey.id);
  }
  openResult(survey: SurveyTemplate): void {
    this.router.navigateByUrl('/app/admin/survey/result/' + survey.id);
  }

  deleteSurvey(id: number): void {
    this.surveyService.deleteSurvey(id).subscribe(
      () => {
        this.saveSurveysFromApi();
      },
      error => {
        console.log(error);
      }
    );
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
      ConfirmDialogComponent,
      { data: new DeleteTemplateDialog() }
    );
    return dialogRef.afterClosed();
  }
  changeSurveyToModel(item: SurveyTemplate) {
    return new SurveyModel(item);
  }
  ngOnDestroy(): void {
    this.surveyService.isCreatorLoading(false);
  }
}
