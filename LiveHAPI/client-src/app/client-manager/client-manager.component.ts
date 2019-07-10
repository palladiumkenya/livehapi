import {Component, OnDestroy, OnInit} from '@angular/core';
import {Message} from 'primeng/api';
import {Subscription} from 'rxjs';
import {ClientStage} from '../model/client-stage';
import {ClientManagerService} from '../services/client-manager.service';

@Component({
    selector: 'liveapp-client-manager',
    templateUrl: './client-manager.component.html',
    styleUrls: ['./client-manager.component.scss']
})
export class ClientManagerComponent implements OnInit, OnDestroy {

    public messages: Message[];

    public manager$: Subscription;
    public clientsCount$: Subscription;
    public clients$: Subscription;
    public clientsStagedCount$: Subscription;
    public clientsStaged$: Subscription;

    public clientsCount = 0;
    public clients: ClientStage[] = [];

    public clientsStagedCount = 0;
    public clientsStaged: ClientStage[] = [];

    public blockReprocess = false;
    public blockStageExport = false;
    public loading = false;

    public constructor(private clientService: ClientManagerService) {
    }

    public ngOnInit() {
        this.loadData();
    }

    private loadData(): void {
        this.blockReprocess = true;
        this.loading = true;
        this.clientsCount = 0;
        this.clientsStagedCount = 0;
        this.clients = [];
        this.clientsStaged = [];
        this.messages = [];
        this.clientsCount$ = this.clientService.getErrorsCount()
            .subscribe(
                p => {
                    this.clientsCount = p;
                },
                e => {
                    this.messages.push({severity: 'error', summary: 'Error Loading', detail: <any>e});
                },
                () => {
                }
            );
        this.clients$ = this.clientService.getErrors()
            .subscribe(
                p => {
                    this.clients = p;
                    this.loading = false;
                    this.blockReprocess = this.clientsCount === 0;
                },
                e => {
                    this.messages.push({severity: 'error', summary: 'Error Loading', detail: <any>e});
                    this.loading = false;
                },
                () => {
                }
            );

        this.clientsStagedCount$ = this.clientService.getStagedCount()
            .subscribe(
                p => {
                    this.clientsStagedCount = p;
                },
                e => {
                    this.messages.push({severity: 'error', summary: 'Error Loading', detail: <any>e});
                },
                () => {
                }
            );
        this.clientsStaged$ = this.clientService.getStaged()
            .subscribe(
                p => {
                    this.clientsStaged = p;
                    this.blockStageExport = this.clientsStagedCount === 0;
                },
                e => {
                    this.messages.push({severity: 'error', summary: 'Error Loading', detail: <any>e});
                    this.loading = false;
                },
                () => {
                }
            );
    }

    public reProcess(): void {
        this.blockReprocess = true;
        this.messages = [];
        this.manager$ = this.clientService.resendAll()
            .subscribe(
                p => {
                },
                e => {
                    this.messages.push({severity: 'error', summary: 'Error Re-processing', detail: <any>e});
                },
                () => {
                    this.messages.push({severity: 'success', summary: 'Failed records have been Staged successfully'});
                    window.location.reload();
                    this.loadData();
                }
            );
    }

    public ngOnDestroy(): void {
        if (this.manager$) {
            this.manager$.unsubscribe();
        }
        if (this.clients$) {
            this.clients$.unsubscribe();
        }
        if (this.clientsCount$) {
            this.clientsCount$.unsubscribe();
        }
        if (this.clientsStaged$) {
            this.clientsStaged$.unsubscribe();
        }
        if (this.clientsStagedCount$) {
            this.clientsStagedCount$.unsubscribe();
        }
    }
}
