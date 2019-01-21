import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-button-single-control',
  templateUrl: './button-single-control.component.html',
  styleUrls: ['./button-single-control.component.scss']
})
export class ButtonSingleControlComponent implements OnInit {
  @Output()
  change: EventEmitter<boolean> = new EventEmitter<boolean>();
  constructor() {}

  ngOnInit() {}
  onFocus() {
    this.change.emit();
  }
}
