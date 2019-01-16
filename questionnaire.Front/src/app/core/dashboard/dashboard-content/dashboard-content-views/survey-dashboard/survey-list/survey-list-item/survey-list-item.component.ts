import {
  Component,
  OnInit,
  Input,
  ChangeDetectionStrategy,
  EventEmitter,
  Output
} from '@angular/core';

@Component({
  selector: 'app-survey-list-item',
  templateUrl: './survey-list-item.component.html',
  styleUrls: ['./survey-list-item.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class SurveyListItemComponent implements OnInit {
  @Input()
  survey: any;

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
