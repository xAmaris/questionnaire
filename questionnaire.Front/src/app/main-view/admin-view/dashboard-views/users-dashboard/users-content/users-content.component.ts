import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material';
import { BehaviorSubject, Observable, Subscription } from 'rxjs';
import {
  RegisteredUser,
  UnregisteredUser
} from '../../../../../models/user.model';
import { UserService } from '../../../survey-container/services/user.services';
import { DeleteTemplateDialogData } from './../../../../../data/shared.data';
import { UnregisteredUserModel } from './../../../../../models/user.model';
import { ConfirmDialogComponent } from './../../../../../shared/confirm-dialog/confirm-dialog.component';
import { AddUserDialogComponent } from './add-user-dialog/add-user-dialog.component';
import { AddUserTabComponent } from './add-user-dialog/add-user-tab/add-user-tab.component';

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
  getAllUsers() {
    this.saveUsersFromApi();
    this.getAllUsersSub = this.userService.savedUnregisteredUsers.subscribe(
      (data: Array<RegisteredUser | UnregisteredUser>) => {
        if (data) {
          this._items$.next(data);
        }
      }
    );
  }
  openAddUserDialog(): void {
    this.dialog.open(AddUserDialogComponent);
  }
  openConfimDeleteDialog(id: number): void {
    this.openSurveyDialog().subscribe((res: boolean) => {
      if (res === true) {
        this.deleteUnregisteredUser(id);
      }
    });
  }
  openUserUpdateDialog(user): void {
    this.openUpdateDialog(user).subscribe((res: any) => {
      if (res) {
        this.updateUnregisteredUser(user.id, res);
      }
    });
  }
  updateUnregisteredUser(id, user) {
    const usermodel: UnregisteredUserModel = new UnregisteredUserModel(user);
    this.userService.updateUserById(id, usermodel).subscribe(data => {
      this.getAllUsers();
    });
  }
  deleteUnregisteredUser(id: number): void {
    this.userService.deleteUserById(id).subscribe(() => {
      this.saveUsersFromApi();
    });
  }
  saveUsersFromApi() {
    this.userService.saveUsersFromApi();
  }
  openSurveyDialog(): Observable<boolean> {
    const dialogRef: MatDialogRef<ConfirmDialogComponent> = this.dialog.open(
      ConfirmDialogComponent,
      { data: new DeleteTemplateDialogData() }
    );
    return dialogRef.afterClosed();
  }
  openUpdateDialog(survey): Observable<boolean> {
    const dialogRef: MatDialogRef<AddUserTabComponent> = this.dialog.open(
      AddUserTabComponent,
      {
        data: survey
      }
    );
    return dialogRef.afterClosed();
  }
}
