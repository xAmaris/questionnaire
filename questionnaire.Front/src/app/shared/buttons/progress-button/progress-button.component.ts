import {
  ChangeDetectionStrategy,
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output
} from '@angular/core';

@Component({
  selector: 'app-progress-button',
  templateUrl: './progress-button.component.html',
  styleUrls: ['./progress-button.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ProgressButtonComponent implements OnInit {
  @Input()
  buttonText: string;
  sth = 'wys';
  @Input()
  loader: boolean;
  @Output()
  buttonClick: EventEmitter<boolean> = new EventEmitter<boolean>();
  constructor() {}

  ngOnInit() {}
  emitClick() {
    this.buttonClick.emit(true);
  }
}
