import axios from "axios";

import { API_BASE_URL } from "../config/api";

export const api =
    axios.create({
        baseURL:
            `${API_BASE_URL}/api`
    });

/*Interceptor lets us modify requests before sending them*/
api.interceptors.request.use(
    config => {
        const token =
            localStorage.getItem(
                "token");

        if (token) {
            config.headers.Authorization =
                `Bearer ${token}`;
        }

        /*Return the modified request.*/
        return config;
    });

api.interceptors.response.use(response => response,

    error => {
        if (error.response?.status === 401)
        {
            localStorage.clear();

            window.location.href =
                "/login";
        }

        return Promise.reject(
            error);
    });