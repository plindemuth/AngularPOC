import { Component, ElementRef, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Subscription } from 'rxjs';
import { AppService } from './app.service';
import { Router } from '@angular/router';

@Component({
  selector: 'ep-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  pageTitle: string = 'Angular/.Net Core Excel Parser';
  description: string = 'This POC app will allow you to upload an excel spreadsheet, save it to mongodb, and allow you to view all previously uploaded rows.';
  rowsUploaded: number = 0;
  errorMessage: string = '';
  form: FormGroup;
  router: Router;

  @ViewChild('fileSelector')
  fileSelector!: ElementRef;

  private uploadUrl = 'https://localhost:44359/api/ExcelParser/upload';
  subScription!: Subscription;

  constructor(public fb: FormBuilder,
    private _appService: AppService,
    private rout: Router) {
      this.router = rout;
      this.form = this.fb.group({
        formData: []
      });
  }


  attachForm(event: any) {
    console.log("attachForm called...")
    let file = (event.target).files[0];
    console.log("file: " + file)
    this.form.patchValue({
      formData: file
    });
    this.form.updateValueAndValidity();
  }

  uploadSpreadsheet(): any {
    if(this.form.get('formData')!.value === null){
      console.log("nothing attached");
      alert("Please attach a spreadhsheet before attempting to upload.");
      return 0;
    }
    this.rowsUploaded = 0;
    console.log("uploadSpreadsheet called.");
    console.log("file data: " + this.form.get('formData')!.value);
    var formData: any = new FormData();
    formData.append("data", this.form.get('formData')!.value);
    this.subScription = this._appService.uploadSpreadsheet(formData).subscribe({
      next: num => {
        this.rowsUploaded = num;
      },
      error: error => this.errorMessage = error
    });
    this.fileSelector.nativeElement.value = '';
    
    this.redirectTo('');
  }

  private redirectTo(uri:string){
    this.router.navigateByUrl('/', {skipLocationChange: true}).then(()=>
    this.router.navigate([uri]));
 }

  ngOnInit(): void{}
  
  ngOnDestroy(): void {
    this.subScription.unsubscribe();
  }
}