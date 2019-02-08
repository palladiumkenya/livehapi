import {Component, Input, OnInit} from '@angular/core';
import {AppComponent} from '../app.component';

@Component({
  selector: 'liveapp-app-menu',
  templateUrl: './app-menu.component.html',
  styleUrls: ['./app-menu.component.scss']
})
export class AppMenuComponent implements OnInit {

    @Input() reset: boolean;

    model: any[];

    constructor(public app: AppComponent) {}

    ngOnInit() {
        this.model = [
            {label: 'Dashboard', icon: 'pie_chart', routerLink: ['/dashboard']},
            {label: 'Contact Tree', icon: 'share', routerLink: ['/tree']},
            {label: 'Settings', icon: 'settings_application', routerLink: ['/config']}
        ];
    }
}
