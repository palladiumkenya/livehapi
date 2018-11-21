export interface ClientContactTree {
    serial?: string;
    isPrimary?: boolean;
    label?: string;
    children?: ClientContactTree[];
}
