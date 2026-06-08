import axios from "axios";
import type { CandidateProfile } from "../types/CandidateProfile";

const API_URL =
    "http://localhost:5271/api/candidate/profile";

function getToken() {
    return localStorage.getItem("token");
}

export async function getProfile() {

    const response =
        await axios.get<CandidateProfile>(
            API_URL,
            {
                headers: {
                    Authorization:
                        `Bearer ${getToken()}`
                }
            });

    return response.data;
}

export async function updateProfile(
    profile: CandidateProfile) {

    await axios.put(
        API_URL,
        profile,
        {
            headers: {
                Authorization:
                    `Bearer ${getToken()}`
            }
        });
}