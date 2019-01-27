import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminSidenavComponent } from './admin-sidenav.component';

export const routes: Routes = [{ path: '', component: AdminSidenavComponent }];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ],
  declarations: [AdminSidenavComponent]
})
export class AdminSidenavModule { }
