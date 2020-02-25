import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { FileUploadModule } from 'primeng/fileupload';
import { ImportUserTabComponent } from './import-user-tab.component';

@NgModule({
  imports: [
    CommonModule,
    FileUploadModule,
    FontAwesomeModule,
    MatButtonModule,
    MatProgressSpinnerModule,
  ],
  declarations: [ImportUserTabComponent]
})
export class ImportUserTabModule {}
