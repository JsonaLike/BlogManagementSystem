export interface PageListModel<T> {
	items: T[];
	totalCount: number;
	pageNumber: number;
	pageSize: number;
}