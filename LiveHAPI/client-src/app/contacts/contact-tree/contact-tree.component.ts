import {Component, Input, OnInit} from '@angular/core';
import {ClientContact} from '../model/client-contact';

@Component({
    selector: 'liveapp-contact-tree',
    templateUrl: './contact-tree.component.html',
    styleUrls: ['./contact-tree.component.scss']
})
export class ContactTreeComponent implements OnInit {

    private _client: ClientContact;
    @Input()
    set client(value: ClientContact) {
        this._client = value;
        this.showTree(value);
    }

    get client(): ClientContact {
        return this._client;
    }

    constructor() {
    }

    ngOnInit() {
    }

    showTree(client: ClientContact) {

    }
}
