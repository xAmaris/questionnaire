import { AuthComponent } from './auth.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    component: AuthComponent,
    children: [
      { path: '', redirectTo: 'login', pathMatch: 'full' },
      {
        path: 'login',
        loadChildren: './auth-views/login/login.module#LoginModule'
      },
      {
        path: 'register',
        loadChildren: './auth-views/register/register.module#RegisterModule'
      },
      {
        path: 'recovery',
        loadChildren:
          './auth-views/password-recovery/password-recovery.module#PasswordRecoveryModule'
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthRoutingModule {}
