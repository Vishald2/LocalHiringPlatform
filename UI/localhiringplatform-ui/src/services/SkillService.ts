
import { API_BASE_URL } from "../config/api";
import type { AddSkillApiRequest } from "../types/AddSkillAPiRequest";

export async function addSkill(
    request: AddSkillApiRequest
): Promise<void> {

    console.log(`${API_BASE_URL}/api/master/skill`);

    console.log("REQUEST", request);
    console.log("JSON", JSON.stringify(request));

    const token =
        localStorage.getItem("token");

    const response = await fetch(
        `${API_BASE_URL}/api/master/skill`,
        {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Authorization":
                    `Bearer ${token}`
            },
            body: JSON.stringify(request)
        });

    if (!response.ok) {

        const errorText =
            await response.json();

        throw new Error(errorText.message);
    }
};

export async function getAllSkills() {

    

};