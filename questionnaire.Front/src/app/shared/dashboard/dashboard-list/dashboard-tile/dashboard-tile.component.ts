import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-dashboard-tile',
  templateUrl: './dashboard-tile.component.html',
  styleUrls: ['./dashboard-tile.component.scss']
})
export class DashboardTileComponent implements OnInit {
  @Input()
  item_tile: any;
  @Output()
  tileClick: EventEmitter<any> = new EventEmitter<any>();
  constructor() {}

  ngOnInit() {}
  boxClick(item: any) {
    this.tileClick.emit(item);
  }
}
