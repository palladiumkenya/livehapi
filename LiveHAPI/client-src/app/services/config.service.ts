import {Injectable} from '@angular/core';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {Observable} from 'rxjs/Observable';
import {DbProtocol} from '../model/db-protocol';
import {Endpoint} from '../model/endpoint';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';
import {Facility} from '../model/facility';
import {throwError} from 'rxjs';
import {Emr} from '../model/emr';
import {Hapi} from '../model/hapi';

@Injectable()
export class ConfigService {

    private _url = './api/wizard';
    private _http: HttpClient;

    public constructor(http: HttpClient) {
        this._http = http;
    }

    public getDatabase(): Observable<DbProtocol> {
        return this._http.get<DbProtocol>(`${this._url}/db`)
            .catch(this.handleError);
    }

    public verifyServer(entity: DbProtocol): Observable<boolean> {
        return this._http.post<boolean>(`${this._url}/verifyserver`, entity)
            .catch(this.handleError);
    }

    public verifyDatabase(entity: DbProtocol): Observable<boolean> {
        return this._http.post<boolean>(`${this._url}/verifydb`, entity)
            .catch(this.handleError);
    }

    public saveDatabase(entity: DbProtocol): Observable<boolean> {
        return this._http.post<boolean>(`${this._url}/db`, entity)
            .catch(this.handleError);
    }

    public getEndpoint(): Observable<Endpoint> {
        return this._http.get<Endpoint>('./api/sync')
            .catch(this.handleError);
    }

    public verifyEndpoint(entity: Endpoint): Observable<Facility> {
        return this._http.post<Facility>('./api/sync', entity)
            .catch(this.handleError);
    }

    public showEmrVersion(entity: Endpoint): Observable<Emr> {
        return this._http.post<Emr>('./api/sync/emr', entity)
            .catch(this.handleError);
    }

    public saveEndpoint(entity: Endpoint): Observable<boolean> {
        return this._http.post<boolean>('./api/sync/url', entity)
            .catch(this.handleError);
    }

    public getVersion(): Observable<string> {
        return this._http.get<string>(`./api/activate/version`)
            .catch(this.handleError);
    }

    public getEmrStatus(): Observable<Hapi> {
        return this._http.get<Hapi>(`./api/sync/hapi`)
            .catch(this.handleError);
    }

    private handleError(err: HttpErrorResponse) {
        if (err.status === 404) {
            return throwError('could not be found');
        }
        return throwError(err.error);
    }
}
