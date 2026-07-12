import { useEffect, useState } from "react";

import type { CandidateEducationModel }
    from "../../types/EducationModels/CandidateEducationModel";

import {
    getCandidateEducations
}
    from "../../services/CandidateEducationService";
import EducationEditor from "./EducationEditor";

export default function Education() {


    const [educations, setEducations] =
        useState<CandidateEducationModel[]>([]);

    const [loading, setLoading] =
        useState(false);

    const [showEditor, setShowEditor] =
        useState(false);

    const [selectedEducationId, setSelectedEducationId] =
        useState<string | null>(null);

    useEffect(() => {

        async function loadEducations() {

            setLoading(true);

            try {

                const result =
                    await getCandidateEducations();

                setEducations(result);

            }
            finally {

                setLoading(false);
            }
        }

        loadEducations();

    }, []);

    function handleAdd() {

        setSelectedEducationId(null);

        setShowEditor(true);
    }

    function handleEdit(
        entityId: string) {

        setSelectedEducationId(entityId);

        setShowEditor(true);
    }

    function handleCloseEditor() {

        setSelectedEducationId(null);

        setShowEditor(false);
    }


    if (showEditor) {

        return (

            <EducationEditor
                entityId={selectedEducationId}
                onClose={handleCloseEditor}
            />

        );
    }
    return (

        <div>

            <div>
                <button style={{width:"120px"} }
                    className="primary-button"
                    onClick={handleAdd}
                >
                    Add Education
                </button>
            </div>

            <div className="education-header">
                <h3>
                    Educations
                </h3>
            </div>

            {
                loading &&

                <div className="loading-text">

                    Loading...

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
                        <div className="education-actions">

                            <button
                                className="secondary-button"
                                onClick={() =>
                                    handleEdit(candidateeducation.entityId)
                                }
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