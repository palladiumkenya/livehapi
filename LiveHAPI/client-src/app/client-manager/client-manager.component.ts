import {Component, OnDestroy, OnInit} from '@angular/core';
import {Message} from '../../../node_modules/primeng/api';
import {Subscription} from '../../../node_modules/rxjs/Subscription';
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

    public clientsCount = 0;
    public clients: ClientStage[] = [];
    public blockReprocess = true;
    public loading = true;

    public constructor(private clientService: ClientManagerService) {
    }

    public ngOnInit() {
        this.loadData();
    }

    private loadData(): void {
        this.clientsCount = 0;
        this.clients = [];
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
                },
                e => {
                    this.messages.push({severity: 'error', summary: 'Error Loading', detail: <any>e});
                    this.loading = false;
                },
                () => {
                    this.loading = false;
                    this.blockReprocess = this.clientsCount === 0;
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
    }
}
