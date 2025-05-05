import type { LinkTypeRepository } from '../domain/repository/linktypeRepository'
import type { LinkType, LinkTypeRow, LinkTypeFilter } from '../domain/model/linktype'
import { get, post, put, deleteRecord, DEFAULT_API_PATH } from '@/modules/shared/http_handler/index'
import { getDate } from '@/modules/shared/utility/date'
import { getRecords } from '@/modules/shared/utility/records'

const loadRecords = async (filter: LinkTypeFilter | null): Promise<GridRecord> => {    
    if(filter && filter.filters?.status === null) {
        delete filter?.filters.status;
    }

    if(filter && filter.filters?.hiredDateFrom) {
        filter.filters.hiredDateFrom = getDate(filter.filters?.hiredDateFrom);
    }

    if(filter && filter.filters?.hiredDateTo) {
        filter.filters.hiredDateTo = getDate(filter.filters?.hiredDateTo);
    }

    const response = await get(DEFAULT_API_PATH + '/LinkType?filter=' + filter?.filter);
    return getRecords(response);
}

const add = async (linktype: LinkType): Promise<LinkTypeRow> => {
    const requestData = await post(DEFAULT_API_PATH + '/LinkType', linktype);
    return requestData;
}

const edit = async (linktype: LinkType): Promise<LinkTypeRow> => {
    const requestData = await put(DEFAULT_API_PATH + '/LinkType', linktype);
    return requestData;
}

const getById = async (linktypeId: number): Promise<LinkType> => {
    const requestData = await get(DEFAULT_API_PATH + '/LinkType/' + linktypeId);
    return requestData.data;
}

const deleteRow = async (id: number): Promise<void> => {
    try {
      await deleteRecord(DEFAULT_API_PATH + '/LinkType/' + id);
    } catch (error: any) {
      throw new Error('Se ha presentado un eror: ' + error.message);
    }
}

export default {
    loadRecords,
    getById,
    add,
    edit,
    deleteRow
} as LinkTypeRepository