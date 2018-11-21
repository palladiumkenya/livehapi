import { Injectable } from '@angular/core';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {Observable} from 'rxjs/Observable';
import {throwError} from 'rxjs';
import {ClientStage} from '../../model/client-stage';
import {ClientContact} from '../model/client-contact';
import {ClientContactTree} from '../model/client-contact-tree';


@Injectable({
  providedIn: 'root'
})
export class ClientContactsService {

    private _url = './api/ClientContacts';
    private _http: HttpClient;

    public constructor(http: HttpClient) {
        this._http = http;
    }

    public generate(): Observable<boolean> {
        return this._http.get<boolean>(`${this._url}/Generate`)
            .catch(this.handleError);
    }

    public getAllCount(): Observable<number> {
        return this._http.get<number>(`${this._url}/Count`)
            .catch(this.handleError);
    }

    public getAll(): Observable<ClientContact[]> {
        return this._http.get<ClientContact[]>(`${this._url}`)
            .catch(this.handleError);
    }

    public getAllTree(): Observable<ClientContactTree[]> {
        return this._http.get<ClientContactTree[]>(`${this._url}/tree`)
            .catch(this.handleError);
    }

    private handleError(err: HttpErrorResponse) {
        if (err.status === 404) {
            return throwError('could not be found');
        }
        return throwError(err.error);
    }
}
