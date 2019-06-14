import {SyncStatus} from './sync-status';

export interface ClientStage {
    serial?: string;
    firstName?: string;
    middleName?: string;
    lastName?: string;
    names?: string;
    clientId?: string;
    syncStatus?: SyncStatus;
    statusDate?: Date;
    timeAgo?: string;
    syncStatusInfo?: string;
    siteCode?: string;
    id?: string;
    userName?: string;
}
