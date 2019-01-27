import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from '../../auth/other/guard.auth';
import { ViewformGuard } from '../../auth/other/viewform.auth';
import { SurveyCreatorResolver } from '../../main-view/admin-view/survey-container/resolvers/survey-creator.resolver';
import { SurveyResultResolver } from '../../main-view/admin-view/survey-container/resolvers/survey-result.resolver';
import { SurveyViewformResolver } from '../../main-view/admin-view/survey-container/resolvers/survey-viewform.resolver';
import { SurveyContainerComponent } from './survey-container.component';

const surveyContainerRoutes: Routes = [
  {
    path: '',
    component: SurveyContainerComponent,
    children: [
      {
        path: 'create/:id',
        loadChildren:
          './../../main-view/admin-view/survey-container/survey-creator/survey-creator.module#SurveyCreatorModule',
        canLoad: [AuthGuard],
        resolve: {
          cres: SurveyCreatorResolver
        },
        data: { preload: true, delay: true }
      },
      {
        path: 'viewform/:preview/:id/:hash',
        loadChildren:
          './../../survey-viewform/survey-viewform.module#SurveyViewformModule',
        canActivate: [ViewformGuard],
        resolve: {
          cres: SurveyViewformResolver
        },
        data: { preload: true, delay: true }
      },
      {
        path: 'viewform/:preview/:id',
        loadChildren:
          './../../survey-viewform/survey-viewform.module#SurveyViewformModule',
        resolve: {
          cres: SurveyViewformResolver
        },
        canLoad: [AuthGuard]
      },
      {
        path: 'result/:id',
        loadChildren:
          './../../main-view/admin-view/survey-container/survey-result/survey-result.module#SurveyResultModule',
        canLoad: [AuthGuard],
        resolve: {
          cres: SurveyResultResolver
        },
        data: { preload: true, delay: false }
      },
      {
        path: 'response',
        loadChildren:
          // './survey-viewform/survey-completed/survey-completed.module#SurveyCompletedModule'
          './../../survey-viewform/survey-completed/survey-completed.module#SurveyCompletedModule'
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(surveyContainerRoutes)],
  providers: [ViewformGuard],
  exports: [RouterModule]
})
export class SurveyContainerRoutingModule {}
