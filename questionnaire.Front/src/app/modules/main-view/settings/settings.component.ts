import {
  ChangeDetectionStrategy,
  Component,
  OnDestroy,
  OnInit
} from '@angular/core';
import { BehaviorSubject, Observable ,  Subscription } from 'rxjs';
import { UserProfile } from '../../../core/auth/other/user.model';
import { AccountService } from '../../../core/auth/services/account.service';
import { SharedService } from '../../../shared/services/shared.service';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class SettingsComponent implements OnInit, OnDestroy {
  userInfoSub: Subscription = new Subscription();
  userName: string;
  email: string;
  private _userInfo$: BehaviorSubject<UserProfile> = new BehaviorSubject<
    UserProfile
  >(undefined);
  get userInfo$(): Observable<UserProfile> {
    if (this._userInfo$ && this._userInfo$.value) {
      this.setUserName();
    }
    return this._userInfo$.asObservable();
  }
  setUserName() {
    this.userName =
      this._userInfo$.value.firstName + ' ' + this._userInfo$.value.lastName;
    this.email = this._userInfo$.value.email;
  }

  constructor(
    private sharedService: SharedService,
    private accountService: AccountService
  ) {}

  ngOnInit() {
    this.backButton(true);
    this.getUserInfo();
  }
  getUserInfo() {
    this.userInfoSub = this.accountService.profileData.subscribe(data => {
      Promise.resolve(null).then(() => this._userInfo$.next(data));
    });
  }
  backButton(x: boolean): void {
    this.sharedService.showBackButton(x);
  }
  ngOnDestroy() {
    this.backButton(false);
  }
}
