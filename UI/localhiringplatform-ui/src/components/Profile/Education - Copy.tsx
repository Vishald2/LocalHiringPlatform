import { useEffect, useState } from "react";

import type { CandidateEducationModel }
    from "../../types/EducationModels/CandidateEducationModel";

import {
    getCandidateEducations, getAllEducations
}
    from "../../services/CandidateEducationService";
import type { CandidateEducationCreateModel } from "../../types/EducationModels/CandidateEducationCreateModel";
import type { EducationModel } from "../../types/EducationModels/EducationModel";

import { getCoursesByEducationId} from "../../services/CourseService";
import type { CourseModel } from "../../types/EducationModels/CourseModel";

export default function Education() {

    const [courses, setCourses] =
        useState<CourseModel[]>([]);

    const [educations, setEducations] =
        useState<CandidateEducationModel[]>([]);

    const [loading, setLoading] =
        useState(false);

    const emptyEducation: CandidateEducationCreateModel = {
        entityId: "",
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
        useState<CandidateEducationCreateModel>(emptyEducation);

    const [showEducationDialog,
        setShowEducationDialog] =
        useState(false);

    const [isEditMode,
        setIsEditMode] =
        useState(false);

    const [educationMasters,
        setEducationMasters] =
        useState<EducationModel[]>([]);

    useEffect(() => {

        async function loadEducations() {

            setLoading(true);

            try {

                const result =
                    await getCandidateEducations();

                setEducations(result);

                const masters =
                    await getAllEducations();

                setEducationMasters(masters);

            }
            finally {

                setLoading(false);
            }
        }

        loadEducations();



    }, []);

    function handleEdit(
        candidateEducation: CandidateEducationModel) {

        setEducation({
            entityId: candidateEducation.entityId,
            educationId: candidateEducation.educationId,
            courseId: candidateEducation.courseId,
            universityId: candidateEducation.universityId,
            instituteName: candidateEducation.instituteName,
            startYear: candidateEducation.startYear,
            endYear: candidateEducation.endYear,
            percentage: candidateEducation.percentage,
            cgpa: candidateEducation.cgpa,
            grade: candidateEducation.grade,
            isCompleted: candidateEducation.isCompleted,
            isHighestEducation: candidateEducation.isHighestEducation,
            courseSpecializationIds: [
                ...candidateEducation.courseSpecializationIds
            ]
        });

        setIsEditMode(true);

        setShowEducationDialog(true);
    }

    async function handleEducationChanged(
        e: React.ChangeEvent<HTMLSelectElement>) {

        const educationId =
            Number(e.target.value);

        const result =
            await getCoursesByEducationId(
                educationId);
        console.log(result);

        setCourses(result);

        setEducation({
            ...education,
            educationId,
            courseId: 0,
            courseSpecializationIds: []
        });
    }

    return (

        <div>

            <div className="education-header">
                {
                    isEditMode &&

                    <div className="validation-success">

                        Editing:
                        {" "}
                        {education.courseId}

                    </div>
                }
                <h3>
                    Education
                </h3>

                <button
                    className="primary-button"
                >
                    Add Education
                </button>

            </div>

            {
                loading &&

                <div className="loading-text">

                    Loading...

                </div>
            }

            {
                !loading &&
                educations.length === 0 &&

                <div>

                    No education found.

                </div>
            }

            {
                educations.map(candidateeducation => (

                    <div
                        key={candidateeducation.entityId}
                        className="education-item"
                    >

                        <div className="education-title">

                            {candidateeducation.courseName}

                        </div>

                        <div className="education-row">

                            <b>Education:</b>

                            {" "}

                            {candidateeducation.educationName}

                        </div>

                        <div className="education-row">

                            <b>University:</b>

                            {" "}

                            {
                                candidateeducation.universityName ??
                                candidateeducation.instituteName
                            }

                        </div>

                        <div className="education-row">

                            <b>Duration:</b>

                            {" "}

                            {candidateeducation.startYear}

                            {" - "}

                            {
                                candidateeducation.isCompleted
                                    ? candidateeducation.endYear
                                    : "Present"
                            }

                        </div>

                        <div className="education-row">

                            <b>Specializations:</b>

                            {" "}

                            {
                                candidateeducation.specializationNames.join(", ")
                            }

                        </div>

                        {
                            candidateeducation.isHighestEducation &&

                            <div className="education-highest">

                                Highest Education

                            </div>
                        }
                        {
                            showEducationDialog &&

                            <div className="dialog-overlay">

                                <div className="dialog">

                                    <h3>
                                        Edit Education
                                    </h3>
                                        <div className="form-group">

                                            <label className="form-label">
                                                Education
                                            </label>

                                            <select
                                                className="form-control"
                                                value={candidateeducation.educationId}
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

                                        </div>

                                        <div className="form-group">

                                            <label className="form-label">
                                                Course
                                            </label>

                                            <select
                                                className="form-control"
                                                value={candidateeducation.courseId}
                                                onChange={(e) =>
                                                    setEducation({
                                                        ...candidateeducation,
                                                        courseId: Number(e.target.value)
                                                    })
                                                }
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

                                        </div>

                                    <div className="dialog-buttons">

                                        <button
                                            className="secondary-button"
                                            onClick={() =>
                                                setShowEducationDialog(false)}
                                        >
                                            Close
                                        </button>

                                    </div>

                                </div>

                            </div>
                        }

                        <div className="education-actions">

                            <button
                                className="secondary-button"
                                onClick={() => handleEdit(candidateeducation)}
                            >
                                Edit
                            </button>

                            <button
                                className="secondary-button"
                            >
                                Delete
                            </button>

                        </div>

                    </div>

                ))
            }

        </div>
    );
}