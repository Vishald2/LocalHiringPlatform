// types/AI/JobSearchResultModel.ts

import type { Job } from "../Job";

export interface JobSearchResultModel {

    job: Job;

    matchScore: number;
}