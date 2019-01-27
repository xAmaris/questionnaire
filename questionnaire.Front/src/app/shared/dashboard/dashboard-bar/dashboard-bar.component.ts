import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { SurveyService } from '../../../main-view/admin-view/survey-container/services/survey.services';

@Component({
  selector: 'app-dashboard-bar',
  templateUrl: './dashboard-bar.component.html',
  styleUrls: ['./dashboard-bar.component.scss']
})
export class DashboardBarComponent implements OnInit {
  @Input()
  groupTitle;
  @Input()
  buttonDets;
  @Output()
  buttonClick: EventEmitter<boolean> = new EventEmitter<boolean>();

  constructor() {}

  ngOnInit() {}

  onButtonClick() {
    this.buttonClick.emit(true);
  }
}
