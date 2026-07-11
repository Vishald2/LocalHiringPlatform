import { useEffect, useState } from "react";

import {
    getCandidateEducation,
    getAllEducations
}
    from "../../services/CandidateEducationService";

import {
    getCoursesByEducationId
}
    from "../../services/CourseService";
import type { CandidateEducationCreateModel } from "../../types/EducationModels/CandidateEducationCreateModel";

interface EducationEditorProps {

    entityId: string | null;

    onClose: () => void;
}

export default function EducationEditor({

    entityId,

    onClose

}: EducationEditorProps) {

    const isEditMode =
        entityId !== null;

    const [loading, setLoading] =
        useState(false);

    const [educationMasters, setEducationMasters] =
        useState<EducationModel[]>([]);

    const [courses, setCourses] =
        useState<CourseModel[]>([]);

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

                if (!entityId)
                    return;

                const result =
                    await getCandidateEducation(
                        entityId);

                setEducation(result);

                const courseList =
                    await getCoursesByEducationId(
                        result.educationId);

                setCourses(courseList);
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

            <p>

                Education Editor Component

            </p>

            {
                isEditMode &&

                <p>

                    Entity Id:
                    {" "}
                    {entityId}

                </p>
            }

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

            <select
                className="form-control"
                value={education.courseId}
                onChange={(e) =>
                    setEducation({
                        ...education,
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

            <button
                className="secondary-button"
                onClick={onClose}
            >
                Back
            </button>

        </div>
    );
}