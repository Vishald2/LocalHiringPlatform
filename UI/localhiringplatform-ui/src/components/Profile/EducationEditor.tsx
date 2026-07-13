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

import {
    getAllUniversities
}
    from "../../services/Education/UniversityService";

import type {
    UniversityResponseModel
}
    from "../../types/EducationModels/UniversityResponseModel";

interface EducationEditorProps {

    entityId: string | null;

    onClose: (refresh: boolean) => void;
}

export default function EducationEditor({

    entityId,

    onClose

}: EducationEditorProps) {

    const currentYear =
        new Date().getFullYear();

    const years =
        Array.from(
            { length: currentYear - 1970 + 6 },
            (_, index) => currentYear + 5 - index);

    const [universities,
        setUniversities] =
        useState<UniversityResponseModel[]>([]);

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

                const universityList =
                    await getAllUniversities();

                setUniversities(universityList);

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

        setLoading(true);

        try {

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
        finally {

            setLoading(false);
        }
    }

    return (

        <div className="card">
            {
                loading &&

                <div className="loading-text">

                    Processing...

                </div>
            }
            <h2>

                {
                    isEditMode
                        ? "Edit Education"
                        : "Add Education"
                }

            </h2>

            <hr />

            <div className="form-group">

                <label className="form-label">
                    University
                </label>

                <select
                    className="form-control"
                    value={education.universityId ?? 0}
                    onChange={(e) =>
                        setEducation(prev => ({
                            ...prev,
                            universityId:
                                Number(e.target.value) || undefined
                        }))
                    }
                >

                    <option value={0}>
                        Select University
                    </option>

                    {
                        universities.map(item => (

                            <option
                                key={item.universityId}
                                value={item.universityId}
                            >
                                {item.name}
                            </option>

                        ))
                    }

                </select>

            </div>

            <div className="form-group">

                <label className="form-label">
                    Institute Name
                </label>

                <input
                    type="text"
                    className="form-control"
                    value={education.instituteName ?? ""}
                    onChange={(e) =>
                        setEducation(prev => ({
                            ...prev,
                            instituteName: e.target.value
                        }))
                    }
                />

            </div>

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

            <div className="form-row">

                <div className="form-group form-group-small">

                    <label className="form-label">
                        Start Year
                    </label>

                    <select
                        className="form-control"
                        value={education.startYear ?? 0}
                        onChange={(e) =>
                            setEducation(prev => ({
                                ...prev,
                                startYear:
                                    Number(e.target.value) || undefined
                            }))
                        }
                    >

                        <option value={0}>
                            Select Year
                        </option>

                        {
                            years.reverse().map(year => (

                                <option
                                    key={year}
                                    value={year}
                                >
                                    {year}
                                </option>

                            ))
                        }

                    </select>

                </div>

                <div className="form-group form-group-small">

                    <label className="form-label">
                        End Year
                    </label>

                    <select
                        className="form-control"
                        value={education.endYear ?? 0}
                        onChange={(e) =>
                            setEducation(prev => ({
                                ...prev,
                                endYear:
                                    Number(e.target.value) || undefined
                            }))
                        }
                    >

                        <option value={0}>
                            Select Year
                        </option>

                        {
                            years.reverse().map(year => (

                                <option
                                    key={year}
                                    value={year}
                                >
                                    {year}
                                </option>

                            ))
                        }

                    </select>

                </div>
            </div>
            <div className="form-row">
                    <div className="form-group form-group-third">

                        <label className="form-label">
                            Percentage
                        </label>

                        <input
                            type="number"
                            step="0.01"
                            className="form-control"
                            value={education.percentage ?? ""}
                            onChange={(e) =>
                                setEducation(prev => ({
                                    ...prev,
                                    percentage:
                                        e.target.value === ""
                                            ? undefined
                                            : Number(e.target.value)
                                }))
                            }
                        />

                    </div>

                    <div className="form-group form-group-third">

                        <label className="form-label">
                            CGPA
                        </label>

                        <input
                            type="number"
                            step="0.01"
                            className="form-control"
                            value={education.cgpa ?? ""}
                            onChange={(e) =>
                                setEducation(prev => ({
                                    ...prev,
                                    cgpa:
                                        e.target.value === ""
                                            ? undefined
                                            : Number(e.target.value)
                                }))
                            }
                        />

                    </div>
                    <div className="form-group">

                        <label className="form-label">
                            Grade
                        </label>

                        <input
                            type="text"
                            className="form-control"
                            value={education.grade ?? ""}
                            onChange={(e) =>
                                setEducation(prev => ({
                                    ...prev,
                                    grade: e.target.value
                                }))
                            }
                        />

                    </div>

                </div>
                <div className="form-row">

                    <label className="checkbox-label">

                        <input
                            type="checkbox"
                            checked={education.isCompleted}
                            onChange={(e) =>
                                setEducation(prev => ({
                                    ...prev,
                                    isCompleted: e.target.checked
                                }))
                            }
                        />

                        Completed

                    </label>

                    <label className="checkbox-label">

                        <input
                            type="checkbox"
                            checked={education.isHighestEducation}
                            onChange={(e) =>
                                setEducation(prev => ({
                                    ...prev,
                                    isHighestEducation: e.target.checked
                                }))
                            }
                        />

                        Highest Education

                    </label>

                </div>



            <button
                className="primary-button"
                onClick={handleSave}
                disabled={loading}
            >
                Save
            </button>

            <button
                className="secondary-button"
                onClick={() => onClose(false)}
                disabled={loading}
            >
                Back
            </button>

        </div>
    );
}