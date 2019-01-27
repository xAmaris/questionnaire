import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StudentComponent } from './student.component';

export const routes: Routes = [{ path: '', component: StudentComponent }];

@NgModule({
  imports: [CommonModule, RouterModule.forChild(routes)],
  declarations: [StudentComponent],
  exports: [StudentComponent]
})
export class StudentModule {}
