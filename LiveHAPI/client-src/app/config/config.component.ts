import {Component, OnDestroy, OnInit} from '@angular/core';
import {ConfigService} from '../services/config.service';
import {Subscription} from 'rxjs/Subscription';
import {DbProtocol} from '../model/db-protocol';
import {ConfirmationService, Message} from 'primeng/api';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';

@Component({
  selector: 'liveapp-config',
  templateUrl: './config.component.html',
  styleUrls: ['./config.component.scss']
})
export class ConfigComponent implements OnInit, OnDestroy {

    private _configService: ConfigService;

    public getDatabase$: Subscription;
    public verifyDatabase$: Subscription;
    public saveDatabase$: Subscription;

    public dbProtocol: DbProtocol;
    public sysMessages: Message[];
    public dbMessages: Message[];
    public apiMessages: Message[];
    public canConnect: boolean;
    public dbSaved: boolean;

    public databaseForm: FormGroup;

    public constructor(private configService: ConfigService, private fb: FormBuilder, private confirmationService: ConfirmationService) {
        this._configService = configService;
        this.databaseForm = this.fb.group({
            server: ['', [Validators.required]],
            database: ['', [Validators.required]],
            user: ['', [Validators.required]],
            password: ['', [Validators.required]],
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
    }

    public verifyDatabase(): void {
        this.dbMessages = [];
        this.getDatabase$ = this._configService.verifyDatabase(this.databaseForm.value)
            .subscribe(
                p => {
                    this.canConnect = p;
                },
                e => {
                    this.dbMessages.push({severity: 'error', summary: 'Error verifying', detail: <any>e});
                },
                () => {
                    if (this.canConnect) {
                        this.dbMessages.push({severity: 'success', summary: 'connection successful'});
                    }
                }
            );
    }


    public saveDatabase(): void {
        this.dbMessages = [];
        this.sysMessages = [];
        this.getDatabase$ = this._configService.saveDatabase(this.databaseForm.value)
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
                            severity: 'error',
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
        if (this.verifyDatabase$) {
            this.verifyDatabase$.unsubscribe();
        }
        if (this.saveDatabase$) {
            this.saveDatabase$.unsubscribe();
        }
    }
}
