/* eslint-disable react-hooks/set-state-in-effect */
import { useEffect, useState } from "react";

import type { CandidateEducationModel }
    from "../../types/EducationModels/CandidateEducationModel";

import {
    deleteCandidateEducation,
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

    async function loadEducations() {

        setLoading(true);

        try {

            const result =
                await getCandidateEducations();

            setEducations(result);

            console.log("Educations loaded:", result);

        }
        finally {

            setLoading(false);
        }
    }

    useEffect(() => {

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

    async function handleCloseEditor(
        refresh: boolean) {

        setShowEditor(false);

        setSelectedEducationId(null);

        if (refresh) {
            await loadEducations();
        }
    }


    if (showEditor) {

        return (

            <EducationEditor
                entityId={selectedEducationId}
                onClose={handleCloseEditor}
            />

        );
    }
    async function handleDelete(
        entityId: string) {

        if (!window.confirm(
            "Delete this education?"))
            return;

        await deleteCandidateEducation(
            entityId);

        await loadEducations();
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

                            {candidateeducation.educationName}-  {candidateeducation.specializationNames.join(", ")}

                        </div>

                        {candidateeducation.instituteName}

                        {
                            candidateeducation.instituteName &&
                            candidateeducation.universityName &&
                            " , "
                        }

                        {candidateeducation.universityName}
                        {
                            candidateeducation.city &&
                            candidateeducation.universityName &&
                            " , "
                        }
                        {candidateeducation.city}
                        {
                            candidateeducation.city &&
                            candidateeducation.state &&
                            " , "
                        }
                        {candidateeducation.state}
                        {
                            candidateeducation.country &&
                            candidateeducation.state &&
                            candidateeducation.country.toLowerCase() != "india" &&
                            " , "
                        }
                        {candidateeducation.country
                            && candidateeducation.country.toLowerCase() != "india" &&
                            candidateeducation.country
                        }

                        <div className="education-row">

                            {candidateeducation.startYear}

                            {" - "}

                            {
                                candidateeducation.isCompleted
                                    ? candidateeducation.endYear
                                    : "Present"
                            }

                        </div>

                        <div className="education-row">

                            {
                                candidateeducation.percentage != null &&
                                `${candidateeducation.percentage}%`
                            }
                            ,
                            {
                                candidateeducation.cgpa != null &&
                                ` CGPA- ${candidateeducation.cgpa}`
                            }
                            ,
                            {
                                candidateeducation.grade &&
                                ` Grade ${candidateeducation.grade}`
                            }

                        </div>

                        <div className="education-row">

                            {
                                candidateeducation.isCompleted &&
                                <span>Completed</span>
                            }

                            {
                                candidateeducation.isHighestEducation &&

                                <span className="education-highest">

                                    Highest Education

                                </span>
                            }

                        </div>

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
                                onClick={() =>
                                    handleDelete(candidateeducation.entityId)
                                }
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