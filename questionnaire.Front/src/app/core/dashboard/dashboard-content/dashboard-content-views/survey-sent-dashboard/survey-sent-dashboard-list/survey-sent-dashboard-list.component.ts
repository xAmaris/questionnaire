import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-survey-sent-dashboard-list',
  templateUrl: './survey-sent-dashboard-list.component.html',
  styleUrls: ['./survey-sent-dashboard-list.component.scss']
})
export class SurveySentDashboardListComponent implements OnInit {
  @Input()
  surveySentList: any;
  constructor() {}

  ngOnInit() {}
}
