import {Component, OnDestroy, OnInit} from '@angular/core';
import {BreadcrumbService} from '../breadcrumb.service';
import {Subscription} from "rxjs";
import {ConfigService} from "../services/config.service";
import {Message} from "primeng/api";
import {Emr} from "../model/emr";

@Component({
  selector: 'liveapp-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit, OnDestroy {
    info = 'This is the Afya Mobile API that will allow data synchronization with your EMR';
    public getEmrVersion$: Subscription;
    private _configService: ConfigService;
    public emrVersionError: boolean;
    public sysMessages: Message[];
    public emr: Emr;

    public constructor(public breadcrumbService: BreadcrumbService,private configService: ConfigService) {
        this.breadcrumbService.setItems([
            {label: 'Dashboard'}
        ]);
        this._configService = configService;
    }

    public ngOnInit() {
    }

    public ngOnDestroy(): void {
        if (this.getEmrVersion$) {
            this.getEmrVersion$.unsubscribe();
        }
    }
}
