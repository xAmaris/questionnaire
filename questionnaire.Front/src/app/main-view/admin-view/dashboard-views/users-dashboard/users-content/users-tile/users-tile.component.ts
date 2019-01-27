import { Component, Input, OnInit } from '@angular/core';
import { UnregisteredUser, UnregisteredUserModel } from '../../../../../../models/user.model';

@Component({
  selector: 'app-users-tile',
  templateUrl: './users-tile.component.html',
  styleUrls: ['./users-tile.component.scss']
})
export class UsersTileComponent implements OnInit {
  @Input()
  user: UnregisteredUser;
  unregisteredUser: UnregisteredUserModel;
  constructor() {}

  ngOnInit() {
    this.unregisteredUser = new UnregisteredUserModel(this.user);
  }
  openCreatorClick() {
  }
}
