import { api } from "../infra/apiClient";

import type
{
    CandidateSearchRequest
}
    from "../types/CandidateSearchRequest";

import type
{
    CandidateSearchResult
}
    from "../types/CandidateSearchResult";

export async function searchCandidates(
    request:
        CandidateSearchRequest) {
    const response =
        await api.post<CandidateSearchResult[]>("/candidatesearch", request);

    return response.data;
}