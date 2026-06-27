import { API_BASE_URL } from "../config/api";
import type { CandidateSkill } from "../types/CandidateSkill";


export async function getMySkills()
    : Promise<CandidateSkill[]> {

    const token =
        localStorage.getItem("token");

    const response =
        await fetch(
            `${API_BASE_URL}/api/candidateskill/my`,
            {
                headers: {
                    "Authorization":
                        `Bearer ${token}`
                }
            });

    if (!response.ok) {

        const errorText =
            await response.json();

        throw new Error(
            errorText.message);
    }

    return await response.json();
}

export async function saveMySkills(
    skillIds: string[])
    : Promise<void> {

    const token =
        localStorage.getItem("token");

    const response =
        await fetch(
            `${API_BASE_URL}/api/candidateskill/my`,
            {
                method: "POST",

                headers: {
                    "Content-Type":
                        "application/json",

                    "Authorization":
                        `Bearer ${token}`
                },

                body:
                    JSON.stringify({
                        skillIds
                    })
            });

    if (!response.ok) {

        const errorText =
            await response.json();

        throw new Error(
            errorText.message);
    }
}