import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-survey-sent-dashboard-list-item',
  templateUrl: './survey-sent-dashboard-list-item.component.html',
  styleUrls: ['./survey-sent-dashboard-list-item.component.scss']
})
export class SurveySentDashboardListItemComponent implements OnInit {
  @Input()
  surveySent: any;

  @Output()
  formItemClick: EventEmitter<number> = new EventEmitter<number>();
  @Output()
  itemPreviewClick: EventEmitter<number> = new EventEmitter<number>();
  @Output()
  itemAnswerClick: EventEmitter<number> = new EventEmitter<number>();

  constructor() {}

  ngOnInit() {}
  onItemClick(id: number): void {
    this.formItemClick.emit(id);
  }
  onItemPreviewClick(id: number): void {
    this.itemPreviewClick.emit(id);
  }
  onItemAnswerClick(id: number): void {
    this.itemAnswerClick.emit(id);
  }
}
