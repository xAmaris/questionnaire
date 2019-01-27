import { Component, OnInit, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-user-info',
  templateUrl: './user-info.component.html',
  styleUrls: ['./user-info.component.scss']
})
export class UserInfoComponent implements OnInit {
  @Input()
  name: string;
  @Output()
  logout: EventEmitter<boolean> = new EventEmitter<boolean>();
  @Output()
  routeSwitch: EventEmitter<string> = new EventEmitter<string>();
  constructor() {}
  ngOnInit() {
    // console.log(name);
  }
  emitLogout() {
    this.logout.emit(true);
  }

  emitRouteSwitch(data: string) {
    this.routeSwitch.emit(data);
  }
}