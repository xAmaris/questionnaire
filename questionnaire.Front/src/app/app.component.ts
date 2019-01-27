import { Component, OnDestroy, OnInit } from '@angular/core';
import {
  NavigationCancel,
  NavigationEnd,
  NavigationError,
  NavigationStart,
  Router,
  RouterEvent
} from '@angular/router';
import { BehaviorSubject, Observable, Subscription } from 'rxjs';
import { UserProfile } from './auth/other/user.model';
import { AccountService } from './auth/services/account.service';
import { AuthenticationService } from './auth/services/authentication.service';
import { AppBarTooltip } from './models/shared.models';
import { SharedService } from './services/shared.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit, OnDestroy {
  // subs
  userServiceSub: Subscription = new Subscription();
  creatorSub: Subscription = new Subscription();
  sendSub: Subscription = new Subscription();
  adminMainSub: Subscription = new Subscription();
  toggleSub: Subscription = new Subscription();
  backSub: Subscription = new Subscription();
  accountRoleSub: Subscription = new Subscription();
  userInfoSub: Subscription = new Subscription();
  previewSub: Subscription = new Subscription();
  profileDataSub: Subscription = new Subscription();
  // subjects
  private _showAdminMenu$: BehaviorSubject<boolean> = new BehaviorSubject<
    boolean
  >(false);
  private _isLogged$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(
    false
  );
  private _isPreview$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(
    false
  );
  private _showToggleButton$: BehaviorSubject<boolean> = new BehaviorSubject<
    boolean
  >(false);
  private _showUserInfo$: BehaviorSubject<boolean> = new BehaviorSubject<
    boolean
  >(false);
  private _showBackButton$: BehaviorSubject<boolean> = new BehaviorSubject<
    boolean
  >(false);
  private _showSendButton$: BehaviorSubject<boolean> = new BehaviorSubject<
    boolean
  >(false);
  private _showCreatorButton$: BehaviorSubject<boolean> = new BehaviorSubject<
    boolean
  >(false);
  private _accountRole$: BehaviorSubject<string> = new BehaviorSubject<string>(
    undefined
  );
  private _profileData$: BehaviorSubject<string> = new BehaviorSubject<string>(
    undefined
  );
  // subjects' getters
  get profileData$(): Observable<string> {
    return this._profileData$.asObservable();
  }
  get showAdminMenu$(): Observable<boolean> {
    return this._showAdminMenu$.asObservable();
  }
  get isLogged$(): Observable<boolean> {
    return this._isLogged$.asObservable();
  }
  get isPreview$(): Observable<boolean> {
    return this._isPreview$.asObservable();
  }
  get showToggleButton$(): Observable<boolean> {
    return this._showToggleButton$.asObservable();
  }
  get showUserInfo$(): Observable<boolean> {
    return this._showUserInfo$.asObservable();
  }
  get showBackButton$(): Observable<boolean> {
    return this._showBackButton$.asObservable();
  }
  get showSendButton$(): Observable<boolean> {
    return this._showSendButton$.asObservable();
  }
  get showCreatorButton$(): Observable<boolean> {
    return this._showCreatorButton$.asObservable();
  }
  get accountRole$(): Observable<string> {
    return this._accountRole$.asObservable();
  }

  // inputs
  loading: boolean;
  loadingOverlay: boolean;
  logoIMG = './../../../assets/logo-wsei.png';
  profileIMG = './../../../assets/profile-image.png';
  toolTipInfo: AppBarTooltip = new AppBarTooltip();

  constructor(
    private router: Router,
    private accountService: AccountService,
    private sharedService: SharedService,
    private authenticationService: AuthenticationService
  ) {
    this.router.events.subscribe((event: RouterEvent) => {
      this.navigationInterceptor(event);
    });
  }
  ngOnInit() {
    this.loggedAccountRole();
    this.checkIfLogged();
    this.showUser();
    this.showCreator();
    this.showSend();
    this.showingAdminMenu();
    this.showToggle();
    this.showBack();
    this.checkIfPreviewed();
    this.getProfileData();
  }
  getProfileData() {
    this.profileDataSub = this.accountService.profileData.subscribe(
      (user: UserProfile) => {
        if (user) {
          // console.log(user);
          const name = user.firstName + ' ' + user.lastName;
          Promise.resolve(null).then(() => this._profileData$.next(name));
        }
      }
    );
  }
  loggedAccountRole(): void {
    this.accountRoleSub = this.accountService.role.subscribe((role: string) => {
      Promise.resolve(null).then(() => this._accountRole$.next(role));
    });
  }
  checkIfLogged(): void {
    this.userServiceSub = this.accountService.isLogged.subscribe(
      (data: boolean) => {
        Promise.resolve(null).then(() => this._isLogged$.next(data));
      }
    );
  }
  checkIfPreviewed(): void {
    this.previewSub = this.sharedService.showPreview.subscribe(
      (data: boolean) => {
        Promise.resolve(null).then(() => this._isPreview$.next(data));
      }
    );
  }
  // showing bar buttons
  showUser(): void {
    this.userInfoSub = this.sharedService.showUserInfo.subscribe(
      (data: boolean) => {
        Promise.resolve(null).then(() => this._showUserInfo$.next(data));
      }
    );
  }
  showToggle(): void {
    this.toggleSub = this.sharedService.showToggle.subscribe(
      (data: boolean) => {
        Promise.resolve(null).then(() => this._showToggleButton$.next(data));
      }
    );
  }
  showCreator(): void {
    this.creatorSub = this.sharedService.showCreator.subscribe(
      (data: boolean) => {
        Promise.resolve(null).then(() => this._showCreatorButton$.next(data));
      }
    );
  }
  showSend(): void {
    this.sendSub = this.sharedService.showSend.subscribe((data: boolean) => {
      Promise.resolve(null).then(() => this._showSendButton$.next(data));
    });
  }
  showingAdminMenu(): void {
    this.adminMainSub = this.sharedService.showAdminMenu.subscribe(
      (data: boolean) => {
        Promise.resolve(null).then(() => this._showAdminMenu$.next(data));
      }
    );
  }
  showBack(): void {
    this.backSub = this.sharedService.showBack.subscribe((data: boolean) => {
      Promise.resolve(null).then(() => this._showBackButton$.next(data));
    });
  }

  // bar buttons actions
  sendSurvey(): void {
    this.sharedService.showSendSurveyDialog(true);
  }
  showSurvey(): void {
    this.sharedService.showSurveyButton(true);
  }
  openSidebar(): void {
    this.sharedService.toggleSideNav(true);
  }

  redirectTo(data: string): void {
    this.loadingOverlay = true;
    console.log(data);
    // const url: string = '/app/admin/d/' + data;
    this.router.navigateByUrl(data);
  }

  backTo(): void {
    const url = '/app/admin/d/';
    const currUrl = this.router.url;
    let routeUrl: string;
    if (currUrl.includes('result') || currUrl.includes('viewform/s')) {
      routeUrl = url + 'sent/(s:a//m:a)';
    } else if (
      currUrl.includes('create') ||
      currUrl.includes('password') ||
      currUrl.includes('settings')
    ) {
      routeUrl = url + 'survey/(s:a//m:a)';
    } else if (currUrl.includes('viewform/t')) {
      routeUrl = '/app/admin/survey/create/2';
    }
    this.router.navigateByUrl(routeUrl);
  }
  logout(): void {
    this.authenticationService.logout();
  }
  routeSwitch(e: string): void {
    // this.accountRole$.subscribe((data: string) => {
    //   this.sharedService.routeSwitch(data);
    // });
    console.log('e', e);
    this.redirectTo(e);
  }

  // loading component handler
  navigationInterceptor(event: RouterEvent): void {
    if (event instanceof NavigationStart) {
      this.loading = true;
    } else if (
      event instanceof NavigationEnd ||
      event instanceof NavigationCancel ||
      event instanceof NavigationError
    ) {
      this.loading = false;
      if (this.loadingOverlay) {
        this.loadingOverlay = false;
      }
    }
  }
  ngOnDestroy() {
    this.userServiceSub.unsubscribe();
    this.creatorSub.unsubscribe();
    this.sendSub.unsubscribe();
    this.adminMainSub.unsubscribe();
    this.toggleSub.unsubscribe();
    this.backSub.unsubscribe();
    this.accountRoleSub.unsubscribe();
    this.userInfoSub.unsubscribe();
  }
}
