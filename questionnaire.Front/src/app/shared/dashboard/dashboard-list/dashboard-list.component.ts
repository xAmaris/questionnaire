import {
  Component,
  ContentChild,
  Input,
  OnDestroy,
  OnInit,
  TemplateRef
} from '@angular/core';
import { MatDialog } from '@angular/material';
import { Subscription } from 'rxjs/Subscription';
import { SurveyService } from '../../../main-view/admin-view/survey-container/services/survey.services';

@Component({
  selector: 'app-dashboard-list',
  templateUrl: './dashboard-list.component.html',
  styleUrls: ['./dashboard-list.component.scss']
})
export class DashboardListComponent implements OnInit, OnDestroy {
  @ContentChild(TemplateRef)
  parentTemplate;
  private _itemArr: any[];
  @Input()
  set itemArr(itemArr) {
    this._itemArr = itemArr;
  }
  get itemArr() {
    if (this._itemArr) {
      this.fetching = false;
    }
    return this._itemArr;
  }
  @Input()
  emptyListInfo: string;

  loading = false;
  fetching = true;

  // subs
  isLoadingSub: Subscription = new Subscription();

  constructor(private surveyService: SurveyService, public dialog: MatDialog) {
  }

  ngOnInit(): void {
    this.isLoadingFromOutside();
  }
  isLoadingFromOutside(): void {
    this.isLoadingSub = this.surveyService.openingCreatorLoader.subscribe(
      data => {
        this.loading = data;
      }
    );
  }
  ngOnDestroy(): void {
    this.loading = false;
    this.isLoadingSub.unsubscribe();
  }
}
