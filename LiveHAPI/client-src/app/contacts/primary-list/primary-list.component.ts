import {Component, EventEmitter, OnDestroy, OnInit, Output} from '@angular/core';
import {Message} from 'primeng/api';
import {Subscription} from 'rxjs';
import {ClientStage} from '../../model/client-stage';
import {ClientContactsService} from '../services/client-contacts.service';
import {ClientContact} from '../model/client-contact';
import {ClientContactTree} from '../model/client-contact-tree';

@Component({
    selector: 'liveapp-primary-list',
    templateUrl: './primary-list.component.html',
    styleUrls: ['./primary-list.component.scss']
})
export class PrimaryListComponent implements OnInit, OnDestroy {

    @Output() treeRequested = new EventEmitter<ClientContactTree>();
    public messages: Message[];

    public clientsCount$: Subscription;
    public clients$: Subscription;
    public clientTrees$: Subscription;

    public clientsCount = 0;
    public clients: ClientContact[] = [];
    public primaryClients: ClientContact[] = [];

    public clientTrees: ClientContactTree[] = [];
    public primaryClientTrees: ClientContactTree[] = [];

    public loading = false;

    constructor(private clientService: ClientContactsService) {
    }

    public ngOnInit() {
        this.loadData();
    }

    private loadData(): void {
        this.loading = true;
        this.clientsCount = 0;
        this.clients = [];
        this.primaryClients = [];
        this.messages = [];
        this.clientsCount$ = this.clientService.getAllCount()
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
        this.clients$ = this.clientService.getAll()
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
                    this.primaryClients = this.clients.filter(x => x.isPrimary);
                    this.clientsCount = this.primaryClients.length;
                }
            );

        this.clientTrees$ = this.clientService.getAllTree()
            .subscribe(
                p => {
                    this.clientTrees = p;
                },
                e => {
                    this.messages.push({severity: 'error', summary: 'Error Loading', detail: <any>e});
                    this.loading = false;
                },
                () => {
                    this.loading = false;
                    this.primaryClientTrees = this.clientTrees.filter(x => x.isPrimary);
                    this.clientsCount = this.primaryClientTrees.length;
                }
            );
    }
    showTree(client: ClientContactTree) {
        this.treeRequested.emit(client);
    }

    public ngOnDestroy(): void {
        if (this.clients$) {
            this.clients$.unsubscribe();
        }
        if (this.clientsCount$) {
            this.clientsCount$.unsubscribe();
        }
    }
}
