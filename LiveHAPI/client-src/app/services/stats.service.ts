import { Injectable } from '@angular/core';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {Observable} from 'rxjs/Observable';
import {Stats} from '../model/stats';

@Injectable({
  providedIn: 'root'
})
export class StatsService {
    private _url = './api/Manager';
    private _http: HttpClient;

    public constructor(http: HttpClient) {
        this._http = http;
    }

    public getStats(): Observable<Stats> {
        return this._http.get<Stats>(`${this._url}/stats`)
            .catch(this.handleError);
    }

    private handleError(err: HttpErrorResponse) {
        if (err.status === 404) {
            return Observable.throw('could not be found');
        }
        return Observable.throw(err.error);
    }
}
