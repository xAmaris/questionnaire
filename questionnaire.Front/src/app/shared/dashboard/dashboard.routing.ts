import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard.component';

const dashboards = {
  main: 'main',
  survey: 'survey',
  users: 'users',
  sent: 'sent'
};
const outlets = {
  sidebar: 's',
  manage: 'm'
};
const outletPath = 'a';
const mainString = '/app/admin/d/';
const outletString =
  '/(' +
  outlets.sidebar +
  ':' +
  outletPath +
  '//' +
  outlets.manage +
  ':' +
  outletPath +
  ')';
const surveyString = mainString + dashboards.survey + outletString;
const usersString = mainString + dashboards.users + outletString;
const sentString = mainString + dashboards.sent + outletString;

const routes: Routes = [
  {
    path: '',
    component: DashboardComponent,
    children: [
      {
        path: dashboards.survey,
        children: [
          {
            path: '',
            redirectTo: surveyString,
            pathMatch: 'full'
          },
          {
            path: outletPath,
            outlet: outlets.sidebar,
            loadChildren:
              './../../main-view/admin-view/dashboard-views/survey-dashboard/survey-sidenav/survey-sidenav.module#SurveySidenavModule',
            data: { preload: true, delay: true }
          },
          {
            path: outletPath,
            outlet: outlets.manage,
            loadChildren:
              './../../main-view/admin-view/dashboard-views/survey-dashboard/survey-content/survey-content.module#SurveyContentModule',
            data: { preload: true, delay: true }
          }
        ]
      },
      {
        path: dashboards.sent,
        children: [
          {
            path: '',
            redirectTo: sentString,
            pathMatch: 'full'
          },
          {
            path: outletPath,
            outlet: outlets.sidebar,
            loadChildren:
              // tslint:disable-next-line:max-line-length
              './../../main-view/admin-view/dashboard-views/survey-sent-dashboard/survey-sent-sidenav/survey-sent-sidenav.module#SurveySentSidenavModule',
            data: { preload: true, delay: true }
          },
          {
            path: outletPath,
            outlet: outlets.manage,
            loadChildren:
              // tslint:disable-next-line:max-line-length
              './../../main-view/admin-view/dashboard-views/survey-sent-dashboard/survey-sent-content/survey-sent-content.module#SurveySentContentModule',
            data: { preload: true, delay: true }
          }
        ]
      },
      {
        path: dashboards.users,
        children: [
          {
            path: '',
            redirectTo: usersString,
            pathMatch: 'full'
          },
          {
            path: outletPath,
            outlet: outlets.sidebar,
            loadChildren:
              './../../main-view/admin-view/dashboard-views/users-dashboard/users-sidenav/users-sidenav.module#UsersSidenavModule',
            data: { preload: true, delay: true }
          },
          {
            path: outletPath,
            outlet: outlets.manage,
            loadChildren:
              './../../main-view/admin-view/dashboard-views/users-dashboard/users-content/users-content.module#UsersContentModule',
            data: { preload: true, delay: true }
          }
        ]
      }
      // {
      //   path: '**',
      //   redirectTo: dashboards.survey
      // }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DashboardRoutingModule {}
