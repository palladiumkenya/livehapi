import {Component, OnDestroy, OnInit} from '@angular/core';
import {BreadcrumbService} from '../breadcrumb.service';
import {Subscription} from 'rxjs';
import {ConfigService} from '../services/config.service';
import {Message} from 'primeng/api';
import {Emr} from '../model/emr';
import {Hapi} from '../model/hapi';

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
    public hapi: Hapi;

    public constructor(public breadcrumbService: BreadcrumbService, private configService: ConfigService) {
        this.breadcrumbService.setItems([
            {label: 'Dashboard'}
        ]);
        this._configService = configService;
    }

    public ngOnInit() {
        this.loadStatus();
    }

    public loadStatus() {
        this.sysMessages = [];
        this.getEmrVersion$ = this._configService.getEmrStatus()
            .subscribe(
                p => {
                    this.hapi = p;
                    if (this.hapi) {
                        // if (!this.hapi.isVerifed) {
                        //     this.sysMessages.push({severity: 'warn', summary: 'Please verify ypu '});
                        // }
                        if (this.hapi.syncVersion === 0) {
                            this.sysMessages.push({
                                severity: 'warn',
                                summary: 'Please update IQCare to latest version and Verify settings '
                            });
                        }
                    }
                },
                e => {
                    this.sysMessages.push({severity: 'error', summary: 'Initialization error ', detail: <any>e});
                },
                () => {
                    console.log(this.emr);
                }
            );
    }

    public ngOnDestroy(): void {
        if (this.getEmrVersion$) {
            this.getEmrVersion$.unsubscribe();
        }
    }
}
