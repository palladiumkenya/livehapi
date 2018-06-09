import {DbProtocol} from './db-protocol';

export interface DbView {
    hapi?: DbProtocol;
    emr?: DbProtocol;
}
