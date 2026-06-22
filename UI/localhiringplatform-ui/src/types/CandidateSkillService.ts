
import api from "../config/api";
import type { Applicant } from "../types/Applicant";
import type { CandidateSkill } from "./CandidateSkill";

export interface SaveCandidateSkillsRequest {
    skillIds: string[];
}

export async function getMySkills()
    : Promise<CandidateSkill[]> {

    const response =
        await api.get<CandidateSkill[]>(
            "/api/candidateskill/my");

    return response.data;
}

export async function saveMySkills(
    request: SaveCandidateSkillsRequest) {
    await api.post(
        "/api/candidateskill/my",
        request);
}