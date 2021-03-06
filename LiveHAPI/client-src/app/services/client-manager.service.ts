import { Injectable } from '@angular/core';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {ClientStage} from '../model/client-stage';
import {Observable} from 'rxjs/Observable';
import {throwError} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ClientManagerService {

    private _url = './api/Manager';
    private _http: HttpClient;

    public constructor(http: HttpClient) {
        this._http = http;
    }

    public getErrorsCount(): Observable<number> {
        return this._http.get<number>(`${this._url}/errorscount`)
            .catch(this.handleError);
    }

    public getErrors(): Observable<ClientStage[]> {
        return this._http.get<ClientStage[]>(`${this._url}/errors`)
            .catch(this.handleError);
    }

    public getStagedCount(): Observable<number> {
        return this._http.get<number>(`${this._url}/stagedcount`)
            .catch(this.handleError);
    }

    public getStaged(): Observable<ClientStage[]> {
        return this._http.get<ClientStage[]>(`${this._url}/staged`)
            .catch(this.handleError);
    }

    public resendAll(): Observable<ClientStage[]> {
        return this._http.post<ClientStage[]>(`${this._url}/resendall`, null)
            .catch(this.handleError);
    }

    public resend(ids: string[]): Observable<ClientStage[]> {
        return this._http.post<ClientStage[]>(`${this._url}/Resend`, ids)
            .catch(this.handleError);
    }

    private handleError(err: HttpErrorResponse) {
        if (err.status === 404) {
            return throwError('could not be found');
        }
        return throwError(err.error);
    }
}
