import type { CourseModel }
    from "../types/EducationModels/CourseModel";

import { api }
    from "../infra/apiClient";

import { API_ENDPOINTS }
    from "../End_Points/apiEndpoints";

function getBaseUrl() {
    return API_ENDPOINTS.course.root;
}

export async function getCoursesByEducationId(
    educationId: number): Promise<CourseModel[]> {

    console.log(`${getBaseUrl()}/education/${educationId}`);

    const response =
        await api.get<CourseModel[]>(
            `${getBaseUrl()}/education/${educationId}`);

    return response.data;
}