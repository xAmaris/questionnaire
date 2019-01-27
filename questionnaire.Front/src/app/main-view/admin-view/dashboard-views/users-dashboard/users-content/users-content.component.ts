import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material';
import { BehaviorSubject, Observable, Subscription } from 'rxjs';
import {
  RegisteredUser,
  UnregisteredUser
} from '../../../../../models/user.model';
import { UserService } from '../../../survey-container/services/user.services';
import { AddUserDialogComponent } from './add-user-dialog/add-user-dialog.component';

@Component({
  selector: 'app-users-content',
  templateUrl: './users-content.component.html',
  styleUrls: ['./users-content.component.scss']
})
export class UsersContentComponent implements OnInit {
  groupTitle = 'Grupa użytkowników 1';
  buttonDets = 'Dodaj nowego użytkownika';
  emptyListInfo = 'Brak niezarejestrowanych użytkowników';
  // tslint:disable-next-line:max-line-length
  private _items$: BehaviorSubject<
    Array<RegisteredUser | UnregisteredUser>
  > = new BehaviorSubject<Array<RegisteredUser | UnregisteredUser>>(undefined);
  get items$(): Observable<Array<RegisteredUser | UnregisteredUser>> {
    return this._items$.asObservable();
  }

  // subs
  getAllUsersSub: Subscription = new Subscription();
  constructor(private userService: UserService, public dialog: MatDialog) {}

  ngOnInit() {
    this.getAllUsers();
  }
  see() {
    console.log('click');
  }
  getAllUsers() {
    this.getAllUsersSub = this.userService.getAllUsers().subscribe(
      (data: Array<RegisteredUser | UnregisteredUser>) => {
        // console.log(data);
        if (data) {
          this._items$.next(data);
        }
      },
      error => {
        console.log(error);
      }
    );
  }
  openAddUserDialog(): void {
    const dialogRef: MatDialogRef<AddUserDialogComponent> = this.dialog.open(
      AddUserDialogComponent
    );
    // return dialogRef.afterClosed();
  }
}
