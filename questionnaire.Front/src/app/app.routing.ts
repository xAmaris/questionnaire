import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './core/auth/other/guard.auth';
import { PreloadSelectedModulesList } from './core/auth/other/preload';

const appRoutes: Routes = [
  { path: '', redirectTo: '/auth/login', pathMatch: 'full' },
  { path: 'auth', loadChildren: './core/auth/auth.module#AuthModule' },
  {
    path: 'app',
    loadChildren: './modules/main-view/main-view.module#MainViewModule',
    canLoad: [AuthGuard]
  },
  {
    path: 'survey',
    loadChildren:
      './shared/survey-container/survey-container.module#SurveyContainerModule'
  },
  { path: '**', redirectTo: '/auth/login' }
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
