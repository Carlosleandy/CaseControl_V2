import type { EvidenceRepository } from '../domain/repository/evidenceRepository'
import type { Evidence, EvidenceRow, EvidenceFilter } from '../domain/model/evidence'
import { get, post, put, deleteRecord, DEFAULT_API_PATH } from '@/modules/shared/http_handler/index'
import { getDate } from '@/modules/shared/utility/date'
import { getRecords } from '@/modules/shared/utility/records'

const loadRecords = async (filter: EvidenceFilter | null): Promise<GridRecord> => {    
    if(filter && filter.filters?.status === null) {
        delete filter?.filters.status;
    }

    if(filter && filter.filters?.hiredDateFrom) {
        filter.filters.hiredDateFrom = getDate(filter.filters?.hiredDateFrom);
    }

    if(filter && filter.filters?.hiredDateTo) {
        filter.filters.hiredDateTo = getDate(filter.filters?.hiredDateTo);
    }

    const response = await get(DEFAULT_API_PATH + '/Evidence?filter=' + filter?.filter);
    return getRecords(response);
}

const add = async (evidence: Evidence): Promise<EvidenceRow> => {
    const requestData = await post(DEFAULT_API_PATH + '/Evidence', evidence);
    return requestData;
}

const edit = async (evidence: Evidence): Promise<EvidenceRow> => {
    const requestData = await put(DEFAULT_API_PATH + '/Evidence', evidence);
    return requestData;
}

const getById = async (evidenceId: number): Promise<Evidence> => {
    const requestData = await get(DEFAULT_API_PATH + '/Evidence/' + evidenceId);
    return requestData.data;
}

const deleteRow = async (id: number): Promise<void> => {
    try {
      await deleteRecord(DEFAULT_API_PATH + '/Evidence/' + id);
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
} as EvidenceRepository