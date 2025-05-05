import { getHash } from "./hash";

export const getRecords = (response: RequestResponse): GridRecord => {
    return {
        records: response.data,
        totalRecordsQty: Number(response.headers['x-totalrecords'])
    }
}


export const evaluateFilter = (filter: any, lastFilterHash: any): boolean => {
    const filterHash = getHash(JSON.stringify(filter.value.filters));
    let hasChanges = false;
    
    if(lastFilterHash.value !== filterHash) {
        filter.value.skip = 0;
        hasChanges = true;
    } else {
        filter.value.skip =  ((filter.value.skip || 0) + filter.value.limit );
    }

    lastFilterHash.value = filterHash;
    return hasChanges;
}

export const resetPageScroll = () => {
    document.querySelectorAll('.mobile-card-wrapper, .p-datatable-wrapper').forEach((e) => {
        e.scrollTop = 0;
    });
}