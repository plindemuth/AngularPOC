import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, pipe, throwError } from "rxjs";
import { catchError, tap } from "rxjs/operators";

@Injectable({
    providedIn: 'root'
})
export class AppService{
    private uploadUrl = 'https://localhost:44359/api/ExcelParser/upload';

    constructor(private httpClient: HttpClient){}

    uploadSpreadsheet(formData: any): Observable<any> {
        return this.httpClient.post(this.uploadUrl, formData).pipe(
          tap(data => console.log('Upload complete, ' + data + ' rows uploaded successfully.')),
          catchError(this.handleError)
        );
    }

    private handleError(error: HttpErrorResponse){
        let errorMessage: string = 'An unknown error has occurred.';

        if(error.message){
            errorMessage = error.message;
        }

        console.error(errorMessage);
        return throwError(errorMessage);
    }
}