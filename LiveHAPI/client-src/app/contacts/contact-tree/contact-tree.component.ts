import {Component, Input, OnInit} from '@angular/core';
import {ClientContact} from '../model/client-contact';
import {TreeNode} from 'primeng/api';
import {ClientContactTree} from '../model/client-contact-tree';

@Component({
    selector: 'liveapp-contact-tree',
    templateUrl: './contact-tree.component.html',
    styleUrls: ['./contact-tree.component.scss']
})
export class ContactTreeComponent implements OnInit {

    private _client: ClientContactTree;
    @Input()
    set client(value: ClientContactTree) {
        this._client = value;
        this.showTree(value);
    }

    get client(): ClientContactTree {
        return this._client;
    }

    contactTree: ClientContactTree[];

    constructor() {
    }

    ngOnInit() {
    }

    showTree(client: ClientContactTree) {
        this.contactTree = [];
        this.contactTree.push(client);
    }
}
