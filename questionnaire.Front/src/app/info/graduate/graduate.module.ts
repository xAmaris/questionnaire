import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GraduateComponent } from './graduate.component';

export const routes: Routes = [{ path: '', component: GraduateComponent }];

@NgModule({
  imports: [CommonModule, RouterModule.forChild(routes)],
  declarations: [GraduateComponent],
  exports: [GraduateComponent]
})
export class GraduateModule {}
