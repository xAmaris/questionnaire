import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UsersSidenavComponent } from './users-sidenav.component';

export const routes: Routes = [{ path: '', component: UsersSidenavComponent }];

@NgModule({
  imports: [
    CommonModule, RouterModule.forChild(routes)
  ],
  declarations: [UsersSidenavComponent]
})
export class UsersSidenavModule { }
