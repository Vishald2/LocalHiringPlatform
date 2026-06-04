import type { CandidateRegisterRequest }
    from "../types/CandidateRegisterRequest";

export async function registerCandidate(
    request: CandidateRegisterRequest
): Promise<void> {

    console.log("Register Candidate");

    console.log(request);
}