import { Component, OnDestroy, OnInit } from '@angular/core'
import { Subscription } from 'rxjs';
import { IEntry } from './entry'
import { EntryService } from './entry.service';
@Component({
    selector: 'ep-entries',
    providers: [EntryService],
    templateUrl: './entries.component.html',
    styleUrls: ['./entries.component.css']
})

export class EntriesComponent implements OnInit, OnDestroy{

    constructor(private _entryService: EntryService){}

    pageTitle: string = 'Previously Uploaded Rows';
    errorMessage: string = '';
    entries: IEntry[] = [];
    filteredEntries: IEntry[] = [];
    subScription!: Subscription;

    private _entryFilter: string = '';

    get entryFilter(): string{
        return this._entryFilter;
    }
    set entryFilter(value: string){
        this._entryFilter = value;
        this.filteredEntries = this.filter(value);
    }

    filter(value: string): IEntry[]{
        value = value.toLocaleLowerCase();
        return this.entries.filter((entry: IEntry) => entry.firstName.toLocaleLowerCase().includes(value));
    }

    ngOnInit(): void {
        this.subScription = this._entryService.getEntries().subscribe({
            next: entries => {
                this.entries = entries;
                this.filteredEntries = this.entries;
            },
            error: error => this.errorMessage = error
        });
    }

    ngOnDestroy(): void{
        this.subScription.unsubscribe();
    }
}