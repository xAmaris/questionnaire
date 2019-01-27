import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './auth/other/guard.auth';
import { PreloadSelectedModulesList } from './auth/other/preload';

const appRoutes: Routes = [
  { path: '', redirectTo: '/auth/login', pathMatch: 'full' },
  { path: 'auth', loadChildren: './auth/auth.module#AuthModule' },
  {
    path: 'app',
    loadChildren: './main-view/main-view.module#MainViewModule',
    canLoad: [AuthGuard]
  },
  {
    path: 'info',
    loadChildren: './info/info.module#InfoModule'
  },
  {
    path: 'survey',
    loadChildren:
      './shared/survey-container/survey-container.module#SurveyContainerModule'
  }

  // { path: '**', redirectTo: '/auth/login' }
];

@NgModule({
  imports: [
    RouterModule.forRoot(appRoutes, {
      preloadingStrategy: PreloadSelectedModulesList
    })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule {}
