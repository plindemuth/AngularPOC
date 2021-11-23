import { Injectable, ɵCodegenComponentFactoryResolver } from "@angular/core";
import { IEntry } from "./entry";
import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { Observable, pipe, throwError } from "rxjs";
import { catchError, tap } from "rxjs/operators";

@Injectable({
    providedIn: 'root'
})
export class EntryService{
    private getEntriesUrl = 'https://localhost:44359/api/ExcelParser/get';

    constructor(private httpClient: HttpClient){}

    getEntries(): Observable<IEntry[]>{
        return this.httpClient.get<IEntry[]>(this.getEntriesUrl).pipe(
            tap(data => console.log('Received: ', JSON.stringify(data))),
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

