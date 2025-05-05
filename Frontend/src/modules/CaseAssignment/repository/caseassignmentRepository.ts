import type { CaseAssignmentRepository } from '../domain/repository/caseassignmentRepository'
import type { CaseAssignment, CaseAssignmentRow, CaseAssignmentFilter } from '../domain/model/caseassignment'
import { get, post, put, deleteRecord, DEFAULT_API_PATH } from '@/modules/shared/http_handler/index'
import { getDate } from '@/modules/shared/utility/date'
import { getRecords } from '@/modules/shared/utility/records'

const loadRecords = async (filter: CaseAssignmentFilter | null): Promise<GridRecord> => {    
    if(filter && filter.filters?.status === null) {
        delete filter?.filters.status;
    }

    if(filter && filter.filters?.hiredDateFrom) {
        filter.filters.hiredDateFrom = getDate(filter.filters?.hiredDateFrom);
    }

    if(filter && filter.filters?.hiredDateTo) {
        filter.filters.hiredDateTo = getDate(filter.filters?.hiredDateTo);
    }

    const response = await get(DEFAULT_API_PATH + '/CaseAssignment?filter=' + filter?.filter);
    return getRecords(response);
}

const add = async (caseassignment: CaseAssignment): Promise<CaseAssignmentRow> => {
    const requestData = await post(DEFAULT_API_PATH + '/CaseAssignment', caseassignment);
    return requestData;
}

const edit = async (caseassignment: CaseAssignment): Promise<CaseAssignmentRow> => {
    const requestData = await put(DEFAULT_API_PATH + '/CaseAssignment', caseassignment);
    return requestData;
}

const getById = async (caseassignmentId: number): Promise<CaseAssignment> => {
    const requestData = await get(DEFAULT_API_PATH + '/CaseAssignment/' + caseassignmentId);
    return requestData.data;
}

const deleteRow = async (id: number): Promise<void> => {
    try {
      await deleteRecord(DEFAULT_API_PATH + '/CaseAssignment/' + id);
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
} as CaseAssignmentRepository