import { Component, OnInit, EventEmitter, Output, Input } from '@angular/core';
import { BarTooltip } from '../models/bar-tooltip.model';

@Component({
  selector: 'app-bar',
  templateUrl: './bar.component.html',
  styleUrls: ['./bar.component.scss']
})
export class BarComponent implements OnInit {
  logoIMG = './../../../../assets/logo-wsei.png';
  profileIMG = './../../../../assets/profile-image.png';

  // inputs
  @Input()
  showAdmin: boolean;
  @Input()
  showSendButton: boolean;
  @Input()
  showToggleButton: boolean;
  @Input()
  isLogged: boolean;
  @Input()
  isPreview: boolean;
  @Input()
  accountRole: string;
  @Input()
  profileName: string;
  @Input()
  showCreatorButton: boolean;
  @Input()
  showAdminMenu: boolean;
  @Input()
  showBackButton: boolean;
  @Input()
  showUserInfo: boolean;
  @Input()
  toolTipInfo: BarTooltip;

  // outputs
  @Output()
  showSurveyButton: EventEmitter<boolean> = new EventEmitter<boolean>();
  @Output()
  openSidebarButton: EventEmitter<boolean> = new EventEmitter<boolean>();
  @Output()
  redirectToButton: EventEmitter<string> = new EventEmitter<string>();
  @Output()
  sendSurveyDialog: EventEmitter<boolean> = new EventEmitter<boolean>();
  @Output()
  backToButton: EventEmitter<boolean> = new EventEmitter<boolean>();

  // child outputs
  @Output()
  logout: EventEmitter<boolean> = new EventEmitter<boolean>();
  @Output()
  routeSwitch: EventEmitter<string> = new EventEmitter<string>();

  constructor() {}

  ngOnInit() {}

  // emit button actions
  redirectTo(data: string) {
    this.redirectToButton.emit('/app/d/' + data);
  }
  backTo(): void {
    this.backToButton.emit(true);
  }
  showSurvey(): void {
    this.showSurveyButton.emit(true);
  }
  sendSurvey(): void {
    this.sendSurveyDialog.emit(true);
  }
  openSidebar(): void {
    this.openSidebarButton.emit(true);
  }

  // emit child actions

  emitLogout() {
    this.logout.emit(true);
  }
  emitRouteSwitch(data: string) {
    this.routeSwitch.emit(data);
  }
}
