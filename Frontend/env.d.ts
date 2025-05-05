/// <reference types="vite/client" />

type GenericKeyValue = {
    id: number;
    description: string;
}

type CriteriaFilter = {
    filters: any;
	limit: number;
	skip?: number;
    search?: string;
    filter?: string;
}

type RequestResponse = {
    status: number;
    data: any;
    headers: any;
}

type GridRecord = {
	records: any[];
	totalRecordsQty: number;
}

type PaginationDTO = {
    id: number;
    page: number;
    recordsNumber: number;
    filter?: string;
}


