import type { Interview, InterviewRow } from "../model/interview"

export interface InterviewRepository {
    loadRecords(filter: CriteriaFilter | null): Promise<GridRecord>,
    getById(interviewId: number):Promise<Interview>, 
    add(interview: Interview): Promise<InterviewRow>,
    edit(interview: Interview): Promise<InterviewRow>,
    deleteRow(id: number): Promise<void>
}
