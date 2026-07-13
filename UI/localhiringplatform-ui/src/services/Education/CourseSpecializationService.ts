import { api } from "../../infra/apiClient";

import { API_ENDPOINTS }
    from "../../End_Points/apiEndpoints";

import type { CourseSpecializationResponseModel }
    from "../../types/EducationModels/CourseSpecializationResponseModel";

function getBaseUrl() {

    return API_ENDPOINTS.CourseSpecialization.root;
}

export async function getCourseSpecializationsByCourseId(
    courseId: number): Promise<CourseSpecializationResponseModel[]> {

    const url = `${getBaseUrl()}/course/${courseId}`;

    console.log("url", url);

    const response =
        await api.get<CourseSpecializationResponseModel[]>(
            url);

    return response.data;
}