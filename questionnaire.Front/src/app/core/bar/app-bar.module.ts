import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { MaterialsModule } from '../../materials/materials.module';
import { ProgressButtonModule } from './../../shared/buttons/progress-button/progress-button.module';
import { AppBarComponent } from './app-bar.component';
import { UserInfoComponent } from './user-info/user-info.component';

@NgModule({
  imports: [CommonModule, FontAwesomeModule, RouterModule, MaterialsModule, ProgressButtonModule],
  declarations: [AppBarComponent, UserInfoComponent],
  exports: [AppBarComponent]
})
export class BarModule {}
