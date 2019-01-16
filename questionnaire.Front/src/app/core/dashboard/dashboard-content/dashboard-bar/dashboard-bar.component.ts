import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-dashboard-bar',
  templateUrl: './dashboard-bar.component.html',
  styleUrls: ['./dashboard-bar.component.scss']
})
export class DashboardBarComponent implements OnInit {
  @Input()
  groupTitle: any;
  @Input()
  buttonDets: any;
  @Output()
  buttonClick: EventEmitter<boolean> = new EventEmitter<boolean>();

  constructor() {}

  ngOnInit() {}

  onButtonClick() {
    this.buttonClick.emit(true);
  }

  searchSurveyList(searchString: string): void {
    // this.surveyService.filterSurveyList(searchString);
  }
}
