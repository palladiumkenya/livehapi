import {Component, OnDestroy, OnInit} from '@angular/core';
import {ConfigService} from '../services/config.service';
import {Subscription} from 'rxjs/Subscription';
import {DbProtocol} from '../model/db-protocol';
import {ConfirmationService, Message} from 'primeng/api';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {Facility} from '../model/facility';
import {Endpoint} from '../model/endpoint';
import {BreadcrumbService} from '../breadcrumb.service';

@Component({
  selector: 'liveapp-config',
  templateUrl: './config.component.html',
  styleUrls: ['./config.component.scss']
})
export class ConfigComponent implements OnInit, OnDestroy {

    private _configService: ConfigService;

    public getDatabase$: Subscription;
    public getEndpoint$: Subscription;
    public verifyServer$: Subscription;
    public verifyDatabase$: Subscription;
    public saveDatabase$: Subscription;
    public verifyEndpoint$: Subscription;
    public saveEndpoint$: Subscription;

    public dbProtocol: DbProtocol;
    public endpoint: Endpoint;
    public sysMessages: Message[];
    public dbMessages: Message[];
    public apiMessages: Message[];
    public canConnect: boolean;
    public dbSaved: boolean;
    public epWorks: boolean;
    public epSaved: boolean;
    public faclity: Facility;


    public databaseForm: FormGroup;
    public synForm: FormGroup;

    public constructor(private configService: ConfigService, private fb: FormBuilder, private confirmationService: ConfirmationService,
                       public breadcrumbService: BreadcrumbService) {
        this._configService = configService;

        this.breadcrumbService.setItems([
            {label: 'Setting'}
        ]);

        this.databaseForm = this.fb.group({
            server: ['', [Validators.required]],
            database: ['', [Validators.required]],
            user: ['', [Validators.required]],
            password: ['', [Validators.required]],
        });

        this.synForm = this.fb.group({
            emr: [{value: 'IQCARE', disabled: true}, [Validators.required]],
            iqcare: ['', [Validators.required]],
        });
    }

    public ngOnInit() {
        this.loadData();
    }

    public loadData(): void {
        this.dbMessages = [];
        this.getDatabase$ = this._configService.getDatabase()
            .subscribe(
                p => {
                    this.dbProtocol = p;
                },
                e => {
                    this.dbMessages.push({severity: 'error', summary: 'Error Loading', detail: <any>e});
                },
                () => {
                    this.databaseForm.patchValue(this.dbProtocol);
                }
            );

        this.apiMessages = [];
        this.getEndpoint$ = this._configService.getEndpoint()
            .subscribe(
                p => {
                    this.endpoint = p;
                },
                e => {
                    this.apiMessages.push({severity: 'error', summary: 'Error Loading', detail: <any>e});
                },
                () => {
                    this.synForm.patchValue(this.endpoint);
                }
            );
    }

    public verifyServer(): void {
        this.dbMessages = [];
        this.verifyServer$ = this._configService.verifyServer(this.databaseForm.value)
            .subscribe(
                p => {
                    this.canConnect = p;
                },
                e => {
                    this.dbMessages.push({severity: 'error', summary: 'Error verifying', detail: <any>e});
                },
                () => {
                    if (this.canConnect) {
                        this.dbMessages.push({severity: 'success', summary: 'connection successful to server ok.'});
                    }
                }
            );
    }

    public verifyDatabase(): void {
        this.dbMessages = [];
        this.verifyDatabase$ = this._configService.verifyDatabase(this.databaseForm.value)
            .subscribe(
                p => {
                    this.canConnect = p;
                },
                e => {
                    this.dbMessages.push({severity: 'error', summary: 'Error verifying', detail: <any>e});
                },
                () => {
                    if (this.canConnect) {
                        this.dbMessages.push({severity: 'success', summary: 'connection successful to database ok'});
                    }
                }
            );
    }


    public saveDatabase(): void {
        this.dbMessages = [];
        this.sysMessages = [];
        this.saveDatabase$ = this._configService.saveDatabase(this.databaseForm.value)
            .subscribe(
                p => {
                    this.dbSaved = p;
                },
                e => {
                    this.dbMessages.push({severity: 'error', summary: 'Error saving', detail: <any>e});
                },
                () => {
                    if (this.dbSaved) {
                        this.dbMessages.push({severity: 'success', summary: 'saved successfully'});
                        this.sysMessages.push({
                            severity: 'warn',
                            summary: 'Restart the LiveHAPI service inorder to effect the changes made to the system settings'
                        });
                    }
                }
            );
    }

    public verifyEndpoint(): void {
        this.apiMessages = [];
        this.verifyEndpoint$ = this._configService.verifyEndpoint(this.synForm.value)
            .subscribe(
                p => {
                    this.faclity = p;
                },
                e => {
                    this.apiMessages.push({severity: 'error', summary: 'Error verifying', detail: <any>e});
                },
                () => {
                    if (this.faclity) {
                        this.epWorks = true;
                    }
                    if (this.epWorks) {
                        this.apiMessages.push({severity: 'success', summary: 'connection successful'});
                    }
                    console.log(this.faclity);
                }
            );
    }

    public saveEndpoint(): void {
        this.apiMessages = [];
        this.sysMessages = [];
        this.saveEndpoint$ = this._configService.saveEndpoint(this.synForm.value)
            .subscribe(
                p => {
                    this.epSaved = p;
                },
                e => {
                    this.apiMessages.push({severity: 'error', summary: 'Error saving', detail: <any>e});
                },
                () => {
                    if (this.epSaved) {
                        this.apiMessages.push({severity: 'success', summary: 'saved successfully'});
                        this.sysMessages.push({
                            severity: 'warn',
                            summary: 'Restart the LiveHAPI service inorder to effect the changes made to the system settings'
                        });
                    }
                }
            );
    }

    public ngOnDestroy(): void {

        if (this.getDatabase$) {
            this.getDatabase$.unsubscribe();
        }
        if (this.verifyServer$) {
            this.verifyServer$.unsubscribe();
        }
        if (this.verifyDatabase$) {
            this.verifyDatabase$.unsubscribe();
        }
        if (this.saveDatabase$) {
            this.saveDatabase$.unsubscribe();
        }
        if (this.getEndpoint$) {
            this.getEndpoint$.unsubscribe();
        }
        if (this.verifyEndpoint$) {
            this.verifyEndpoint$.unsubscribe();
        }
        if (this.saveEndpoint$) {
            this.saveEndpoint$.unsubscribe();
        }
    }
}
