export interface ClientContact {
    id?: string;
    clientId?: string;
    serial?: string;
    names?: string;
    firstName?: string;
    middleName?: string;
    lastName?: string;
    dateOfBirth?: Date;
    sex?: number;
    relation?: string;
    clientContactNetworkId?: string;
    networks?: ClientContact[];
    generated?: Date;
    isPrimary?: boolean;
}
