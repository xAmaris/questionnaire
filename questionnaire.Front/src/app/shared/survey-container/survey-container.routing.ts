import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SurveyContainerComponent } from './survey-container.component';
import { SurveyCreatorResolver } from 'src/app/core/dashboard/dashboard-views/resolvers/survey-creator.resolver';

const routes: Routes = [
  {
    path: '',
    component: SurveyContainerComponent,
    children: [
      {
        path: 'create/:id',
        loadChildren:
          './../../core/dashboard/dashboard-views/survey-creator/survey-creator.module#SurveyCreatorModule',
        // canLoad: [AuthGuard],
        resolve: {
          cres: SurveyCreatorResolver
        }
        // data: { preload: true, delay: true }
      },
      {
        path: 'viewform/:preview/:id/:hash',
        loadChildren:
          './../survey-viewform/survey-viewform.module#SurveyViewformModule'
        // canActivate: [ViewformGuard],
        // resolve: {
        //   cres: SurveyViewformResolver
        // },
        // data: { preload: true, delay: true }
      },
      {
        path: 'viewform/:preview/:id',
        loadChildren:
          './../survey-viewform/survey-viewform.module#SurveyViewformModule'
        // resolve: {
        //   cres: SurveyViewformResolver
        // },
        // canLoad: [AuthGuard]
      },
      {
        path: 'result/:id',
        loadChildren:
          './../../core/dashboard/dashboard-views/survey-result/survey-result.module#SurveyResultModule'
        // canLoad: [AuthGuard],
        // resolve: {
        //   cres: SurveyResultResolver
        // },
        // data: { preload: true, delay: false }
      },
      {
        path: 'response',
        loadChildren:
          // './survey-viewform/survey-completed/survey-completed.module#SurveyCompletedModule'
          './../survey-viewform/survey-completed/survey-completed.module#SurveyCompletedModule'
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  // providers: [ViewformGuard],
  exports: [RouterModule]
})
export class SurveyContainerRoutingModule {}
