export interface CollectionApi<T> {
    total: number;
    skip: number;
    take: number;
    count: number;
    items: Array<T>;
}
