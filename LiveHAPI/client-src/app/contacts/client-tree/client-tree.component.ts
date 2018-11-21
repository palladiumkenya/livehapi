import {Component, OnDestroy, OnInit} from '@angular/core';
import {BreadcrumbService} from '../../breadcrumb.service';
import {Message} from 'primeng/api';
import {Subscription} from 'rxjs';
import {ClientStage} from '../../model/client-stage';
import {ClientContactsService} from '../services/client-contacts.service';
import {ClientContact} from '../model/client-contact';

@Component({
    selector: 'liveapp-client-tree',
    templateUrl: './client-tree.component.html',
    styleUrls: ['./client-tree.component.scss']
})
export class ClientTreeComponent implements OnInit, OnDestroy {

    public messages: Message[];
    public manager$: Subscription;
    public blockGenerateTree = false;
    public loading = false;
    public selectedClient: ClientContact = {};

    public constructor(public breadcrumbService: BreadcrumbService,
                       private clientService: ClientContactsService) {
        this.breadcrumbService.setItems([
            {label: 'Contact Tree'}
        ]);
    }

    public ngOnInit() {
        this.loadData();
    }

    private loadData(): void {
        this.blockGenerateTree = true;
    }

    public generateTree(): void {
        this.blockGenerateTree = true;
        this.messages = [];
        this.manager$ = this.clientService.generate()
            .subscribe(
                p => {
                },
                e => {
                    this.messages.push({severity: 'error', summary: 'Error Generating', detail: <any>e});
                },
                () => {
                    this.messages.push({severity: 'success', summary: 'Generated successfully'});
                    window.location.reload();
                    this.loadData();
                }
            );
    }

    public onShowTree(client: ClientContact) {
        this.selectedClient = client;
    }

    public ngOnDestroy(): void {
        if (this.manager$) {
            this.manager$.unsubscribe();
        }
    }
}
