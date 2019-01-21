import { AccountService } from './../auth/auth-views/services/account.service';
import { Component, OnInit, ViewChild, OnDestroy } from '@angular/core';
import { Observable, BehaviorSubject, Subscription } from 'rxjs';
import { SharedService } from 'src/app/services/shared.service';
import { Router } from '@angular/router';
import { BarTooltip } from './models/bar-tooltip.model';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit, OnDestroy {
  @ViewChild('sidenav')
  sidenav: any;
  isSidebarOpened = true;
  // subs
  userServiceSub: Subscription = new Subscription();
  toggleSidebarSub: Subscription = new Subscription();
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
  // loading: boolean;
  // loadingOverlay: boolean;
  // logoIMG = './../../../assets/logo-wsei.png';
  // profileIMG = './../../../assets/profile-image.png';
  toolTipInfo: BarTooltip = new BarTooltip();

  constructor(
    private sharedService: SharedService,
    private accountService: AccountService,
    private router: Router
  ) {}
  ngOnInit() {
    this.toggleSidebar();
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

  // sidenav
  toggleSidebar(): void {
    this.toggleSidebarSub = this.sharedService.toggleSidebar.subscribe(() => {
      if (this.sidenav) {
        this.sidenav.toggle();
      }
    });
  }
  isLargeScreen(): boolean {
    const width =
      window.innerWidth ||
      document.documentElement.clientWidth ||
      document.body.clientWidth;
    if (width < 1100) {
      if (this.isSidebarOpened === true) {
        this.isSidebarOpened = false;
      }
      return false;
    } else {
      if (this.isSidebarOpened === false) {
        // this.isSidebarOpened = true;
      }
      return true;
    }
  }
  getProfileData() {
    this.profileDataSub = this.accountService.profileData.subscribe(
      (user: any) => {
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
    this.sharedService.toggleSideNav();
  }

  redirectTo(data: string): void {
    // this.loadingOverlay = true;
    // console.log(data);
    // const url: string = '/app/admin/d/' + data;
    this.router.navigateByUrl(data);
  }
  logout(): void {
    // this.authenticationService.logout();
  }

  ngOnDestroy() {
    this.userServiceSub.unsubscribe();
    this.toggleSidebarSub.unsubscribe();
    this.creatorSub.unsubscribe();
    this.sendSub.unsubscribe();
    this.adminMainSub.unsubscribe();
    this.toggleSub.unsubscribe();
    this.backSub.unsubscribe();
    this.accountRoleSub.unsubscribe();
    this.userInfoSub.unsubscribe();
    this.previewSub.unsubscribe();
    this.profileDataSub.unsubscribe();
  }
}
