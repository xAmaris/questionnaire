import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {
  MAT_MOMENT_DATE_FORMATS,
  MomentDateAdapter
} from '@angular/material-moment-adapter';
import {
  DateAdapter,
  MAT_DATE_FORMATS,
  MAT_DATE_LOCALE
} from '@angular/material/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {
  FontAwesomeModule,
  FaIconLibrary
} from '@fortawesome/angular-fontawesome';
import { faChartBar, faEye } from '@fortawesome/free-regular-svg-icons';
import {
  faArrowLeft,
  faBars,
  faBriefcase,
  faClone,
  faCog,
  faEllipsisH,
  faEllipsisV,
  faFileExcel,
  faGripVertical,
  faPen,
  faPlus,
  faSearch,
  faSortDown,
  faSortUp,
  faTimes,
  faTrash,
  faUserAlt
} from '@fortawesome/free-solid-svg-icons';
import { AppComponent } from './app.component';
import { AppConfig } from './core/others/url.config';
import { AppRoutingModule } from './app.routing';
import { AuthGuard } from './core/auth/other/guard.auth';
import { GuidGuard } from './core/auth/other/guid.auth';
import { JwtInterceptor } from './core/auth/other/jwt.interceptor';
import { PreloadSelectedModulesList } from './core/auth/other/preload';
import { AccountService } from './core/auth/services/account.service';
import { AuthenticationService } from './core/auth/services/authentication.service';
import { BarModule } from './core/bar/app-bar.module';
import { SurveyViewformResolver } from './modules/main-view/admin-view/survey-container/resolvers/survey-viewform.resolver';
import { SurveyService } from './modules/main-view/admin-view/survey-container/services/survey.services';
import { MaterialsModule } from './core/materials/materials.module';
import { SharedService } from './shared/services/shared.service';
import { LoadingOverlayModule } from './shared/loading-overlay/loading-overlay.module';
import { LoadingScreenModule } from './shared/loading-screen/loading-screen.module';

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    MaterialsModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FontAwesomeModule,
    LoadingOverlayModule,
    BarModule,
    LoadingScreenModule
  ],
  providers: [
    AuthenticationService,
    AccountService,
    SharedService,
    AuthGuard,
    GuidGuard,
    AppConfig,
    SurveyService,
    SurveyViewformResolver,
    PreloadSelectedModulesList,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtInterceptor,
      multi: true
    },
    {
      provide: MAT_DATE_LOCALE,
      useValue: 'pl-PL'
    },
    {
      provide: DateAdapter,
      useClass: MomentDateAdapter,
      deps: [MAT_DATE_LOCALE]
    },
    {
      provide: MAT_DATE_FORMATS,
      useValue: MAT_MOMENT_DATE_FORMATS
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
  constructor(library: FaIconLibrary) {
    library.addIcons(
      faTimes,
      faBars,
      faPlus,
      faTrash,
      faSortDown,
      faSortUp,
      faGripVertical,
      faPen,
      faBriefcase,
      faCog,
      faEye,
      faClone,
      faUserAlt,
      faEllipsisV,
      faSearch,
      faEllipsisH,
      faChartBar,
      faArrowLeft,
      faFileExcel
    );
  }
}
