import {
  Component,
  ElementRef,
  OnInit,
  ViewChild,
  ViewChildren
} from '@angular/core';
import { FileUpload } from 'primeng/fileupload';
import {
  UnregisteredUser,
  UnregisteredUserModel
} from '../../../../../../../models/user.model';
import { UserService } from '../../../../../survey-container/services/user.services';

@Component({
  selector: 'app-import-user-tab',
  templateUrl: './import-user-tab.component.html',
  styleUrls: ['./import-user-tab.component.scss']
})
export class ImportUserTabComponent implements OnInit {
  @ViewChild('fileInput')
  fileInput: FileUpload;
  @ViewChildren('progress')
  progress: ElementRef;
  attachmentsLength = 0;
  loading: boolean;
  completed: boolean;
  readyStatus = 'GOTOWE';
  errorStatus = 'BŁĄD PRZESŁANIA';
  status: string;
  constructor(private userService: UserService) {}

  ngOnInit() {}

  removeAttachment(event, i) {
    console.log(i);
    this.fileInput.remove(event, i);
    this.attachmentsLength = this.fileInput.files.length;
  }
  formatSize(bytes) {
    return this.fileInput.formatSize(bytes);
  }
  uploadFiles(fileObj) {
    this.loading = true;
    console.log(fileObj);
    const files = fileObj.files;
    const body = new FormData();
    for (const file of files) {
      body.append('File', file);
    }
    this.userService.importUsers(body).subscribe(
      () => {
        this.status = this.readyStatus;
        this.completed = true;
        this.loading = false;
      },
      () => {
        this.status = this.errorStatus;
        this.completed = true;
        this.loading = false;
      }
    );
  }
  onFileSelect(event) {
    this.completed = false;
    const progress = this.progress['_results'];
    const filesLength = this.fileInput.files.length;
    let value: number;
    // Reset progress indicator on new file selection.
    for (let i = this.attachmentsLength; i < filesLength; i++) {
      const reader: FileReader = new FileReader();
      reader.readAsDataURL(this.fileInput.files[i]);
      reader.onabort = () => {
        alert('File read cancelled');
      };
      reader.onprogress = e => {
        if (e.lengthComputable) {
          const val = (e.loaded * 100) / e.total;
          value = Math.round(val);
          progress[i].nativeElement.style.width = value + '%';
        }
      };
    }
    this.attachmentsLength = filesLength;
    this.fileInput.onFileSelect(event);
  }
}
