import {Component, EventEmitter, OnDestroy, OnInit, Output} from '@angular/core';
import {ClientContactsService} from '../services/client-contacts.service';
import {ClientContactTree} from '../model/client-contact-tree';
import {Message} from 'primeng/api';
import {Subscription} from 'rxjs';
import {ClientContact} from '../model/client-contact';

@Component({
  selector: 'liveapp-primarytree-list',
  templateUrl: './primarytree-list.component.html',
  styleUrls: ['./primarytree-list.component.scss']
})
export class PrimarytreeListComponent implements OnInit, OnDestroy {
    @Output() treeRequested = new EventEmitter<ClientContactTree>();
    public messages: Message[];

    public clientsCount$: Subscription;
    public clientTrees$: Subscription;

    public clientsCount = 0;
    public clientTrees: ClientContactTree[] = [];
    public primaryClientTrees: ClientContactTree[] = [];

    public loading = false;
  constructor(private clientService: ClientContactsService) { }

    public ngOnInit() {
        this.loadData();
    }

    private loadData(): void {
        this.loading = true;
        this.clientsCount = 0;
        this.clientTrees = [];
        this.primaryClientTrees = [];
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
        if (this.clientTrees$) {
            this.clientTrees$.unsubscribe();
        }
        if (this.clientsCount$) {
            this.clientsCount$.unsubscribe();
        }
    }

}
