import { useEffect, useState } from "react";

import {
    getCandidateEducation,
    getAllEducations,
    updateCandidateEducation,
    addCandidateEducation
} from "../../services/CandidateEducationService";

import {
    getCoursesByEducationId
} from "../../services/CourseService";

import type {
    CandidateEducationCreateModel
} from "../../types/EducationModels/CandidateEducationCreateModel";

import {
    getCourseSpecializationsByCourseId
} from "../../services/Education/CourseSpecializationService";

import type {
    CourseSpecializationResponseModel
} from "../../types/EducationModels/CourseSpecializationResponseModel";
import type { EducationModel } from "../../types/EducationModels/EducationModel";
import type { CourseModel } from "../../types/EducationModels/CourseModel";

interface EducationEditorProps {

    entityId: string | null;

    onClose: (refresh: boolean) => void;
}

export default function EducationEditor({

    entityId,

    onClose

}: EducationEditorProps) {

    const [courseSpecializations,
        setCourseSpecializations] =
        useState<CourseSpecializationResponseModel[]>([]);

    const isEditMode =
        entityId !== null;

    const [loading, setLoading] =
        useState(false);

    const [educationMasters, setEducationMasters] =
        useState<EducationModel[]>([]);

    const [courses, setCourses] =
        useState<CourseModel[]>([]);

    const emptyEducation: CandidateEducationCreateModel = {

        entityId: null,

        educationId: 0,

        courseId: 0,

        universityId: undefined,

        instituteName: "",

        startYear: undefined,

        endYear: undefined,

        percentage: undefined,

        cgpa: undefined,

        grade: "",

        isCompleted: true,

        isHighestEducation: false,

        courseSpecializationIds: []
    };

    const [education, setEducation] =
        useState<CandidateEducationCreateModel>(
            emptyEducation);

    useEffect(() => {

        async function load() {

            setLoading(true);

            try {

                const masters =
                    await getAllEducations();

                console.log("Masters:", masters);

                setEducationMasters(masters);

                if (!isEditMode) {

                    setEducation({
                        ...emptyEducation
                    });

                    setCourses([]);

                    setCourseSpecializations([]);

                    return;
                }

                const result =
                    await getCandidateEducation(
                        entityId);

                console.log("CandidateEducation", result)

                setEducation(result);

                const courseList =
                    await getCoursesByEducationId(
                        result.educationId);

                setCourses(courseList);

                const specializationList =
                    await getCourseSpecializationsByCourseId(
                        result.courseId);

                setCourseSpecializations(
                    specializationList);

                console.log("entityId", entityId);
                console.log("Specializations:", specializationList);
            }
            finally {

                setLoading(false);
            }
        }

        load();

    }, []);

    async function handleEducationChanged(
        e: React.ChangeEvent<HTMLSelectElement>) {

        const educationId = Number(e.target.value);

        const courseList =
            await getCoursesByEducationId(educationId);

        setCourses(courseList);

        setEducation(prev => ({
            ...prev,
            educationId,
            courseId: 0,
            courseSpecializationIds: []
        }));
    }

    async function handleCourseChanged(
        e: React.ChangeEvent<HTMLSelectElement>) {

        const courseId =
            Number(e.target.value);

        const result =
            await getCourseSpecializationsByCourseId(
                courseId);

        setCourseSpecializations(result);

        setEducation(prev => ({
            ...prev,
            courseId,
            courseSpecializationIds: []
        }));
    }

    async function handleSave() {

        if (isEditMode) {

            await updateCandidateEducation(
                education);
        }
        else {

            await addCandidateEducation(
                education);
        }

        onClose(true);
    }

    return (

        <div className="card">

            <h2>

                {
                    isEditMode
                        ? "Edit Education"
                        : "Add Education"
                }

            </h2>

            <hr />

            <label className="form-label">
                Education
            </label>
            <select
                className="form-control"
                value={education.educationId}
                onChange={handleEducationChanged}
            >
                <option value={0}>
                    Select Education
                </option>

                {
                    educationMasters.map(item => (

                        <option
                            key={item.educationId}
                            value={item.educationId}
                        >
                            {item.educationName}
                        </option>

                    ))
                }
            </select>
            <p></p>
            <label className="form-label">
                Course
            </label>
            <select
                className="form-control"
                value={education.courseId}
                onChange={handleCourseChanged}
            >
                <option value={0}>
                    Select Course
                </option>

                {
                    courses.map(course => (

                        <option
                            key={course.courseId}
                            value={course.courseId}
                        >
                            {course.name}
                        </option>

                    ))
                }
            </select>
            <p></p>
            <div className="form-group">

                <label className="form-label">
                    Specializations
                </label>

                <select
                    className="form-control education-multiselect"
                    multiple
                    size={8}
                    value={education.courseSpecializationIds.map(String)}
                    onChange={(e) => {
                        const selectedIds =
                            Array.from(e.target.selectedOptions)
                                .map(option => Number(option.value));

                        setEducation({
                            ...education,
                            courseSpecializationIds: selectedIds
                        });
                    }}
                >

                    {
                        courseSpecializations.map(item => (

                            <option
                                key={item.courseSpecializationId}
                                value={item.specializationId}
                            >
                                {item.specializationName}-{item.specializationId}
                            </option>

                        ))
                    }

                </select>

            </div>

            <button
                className="primary-button"
                onClick={handleSave}
            >
                Save
            </button>

            <button
                className="secondary-button"
                onClick={onClose}
            >
                Back
            </button>

        </div>
    );
}