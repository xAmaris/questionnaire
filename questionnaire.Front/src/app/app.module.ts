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
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { library } from '@fortawesome/fontawesome-svg-core';
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
  // faGripHorizontal,
  faGraduationCap,
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
import { SortablejsModule } from 'angular-sortablejs/dist';
import { AppComponent } from './app.component';
import { AppConfig } from './app.config';
import { AppRoutingModule } from './app.routing';
import { AuthGuard } from './auth/other/guard.auth';
import { GuidGuard } from './auth/other/guid.auth';
import { JwtInterceptor } from './auth/other/jwt.interceptor';
import { PreloadSelectedModulesList } from './auth/other/preload';
import { AccountService } from './auth/services/account.service';
import { AuthenticationService } from './auth/services/authentication.service';
import { BarModule } from './core/bar/app-bar.module';
import { SurveyViewformResolver } from './main-view/admin-view/survey-container/resolvers/survey-viewform.resolver';
import { SurveyService } from './main-view/admin-view/survey-container/services/survey.services';
import { MaterialsModule } from './materials/materials.module';
import { SharedService } from './services/shared.service';
import { LoadingOverlayModule } from './shared/loading-overlay/loading-overlay.module';
import { LoadingScreenModule } from './shared/loading-screen/loading-screen.module';

library.add(
  faTimes,
  faBars,
  faPlus,
  faTrash,
  faSortDown,
  faSortUp,
  // faGripHorizontal,
  faGripVertical,
  faPen,
  faGraduationCap,
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
    LoadingScreenModule,
    SortablejsModule.forRoot({ animation: 150 })
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
export class AppModule {}
