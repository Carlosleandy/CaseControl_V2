import type { LinkType, LinkTypeRow } from "../model/linktype"

export interface LinkTypeRepository {
    loadRecords(filter: CriteriaFilter | null): Promise<GridRecord>,
    getById(linktypeId: number):Promise<LinkType>, 
    add(linktype: LinkType): Promise<LinkTypeRow>,
    edit(linktype: LinkType): Promise<LinkTypeRow>,
    deleteRow(id: number): Promise<void>
}
