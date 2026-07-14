import { useEffect, useState } from "react";
import { Link } from "react-router-dom";

import {
    deleteCandidateExperience,
    getCandidateExperiences
} from "../../services/Experience/CandidateExperiences";

import type { CandidateExperienceResponseModel }
    from "../../types/Experience/CandidateExperienceResponseModel";

export default function Employment() {

    const [experiences, setExperiences] =
        useState<CandidateExperienceResponseModel[]>([]);

    const [loading, setLoading] =
        useState(true);

    async function loadExperiences() {

        setLoading(true);

        const response =
            await getCandidateExperiences();

        setExperiences(response);

        setLoading(false);
    }

    useEffect(() => {

        async function loadExperiences() {

            setLoading(true);

            const response =
                await getCandidateExperiences();

            setExperiences(response);

            setLoading(false);
        }

        loadExperiences();

    }, []);

   

    async function handleDelete(
        entityId: string) {

        if (!window.confirm(
            "Are you sure you want to delete this employment?")) {

            return;
        }

        await deleteCandidateExperience(entityId);

       loadExperiences();
    }

    if (loading) {

        return <div>Loading...</div>;
    }

    return (

        <div className="profile-page">

            <div className="profile-section-header">

                <h2>Employment</h2>
                <Link
                    to="/profile/employment/add"
                    >

                    Add Employment

                </Link>

            </div>

            {
                experiences.length === 0 &&

                <div className="empty-state">

                    No employment added yet.

                </div>
            }

            {
                experiences.map(experience => (

                    <div
                        key={experience.entityId}
                        className="profile-card">
                        <br></br>

                        <div className="profile-card-header">

                            <div>

                                <h3>{experience.companyName}</h3>

                                <div className="profile-subtitle">
                                    
                                    {experience.designation}
                                </div>

                            </div>

                        </div>

                        <div className="profile-row">

                            <strong>Industry:</strong>

                            {" "}

                            {experience.industryTypeName}

                        </div>

                        <div className="profile-row">

                            <strong>Location:</strong>

                            {" "}

                            {experience.city}

                            {experience.state &&
                                `, ${experience.state}`}

                            {experience.country &&
                                `, ${experience.country}`}

                        </div>

                        <div className="profile-row">

                            <strong>Duration:</strong>

                            {" "}

                            {experience.startDate}

                            {" - "}

                            {
                                experience.isCurrentCompany
                                    ? "Present"
                                    : experience.endDate
                            }

                        </div>

                        {
                            experience.summary &&

                            <div className="profile-summary">

                                {experience.summary}

                            </div>
                        }

                        <div className="profile-actions">

                            <Link
                                to={`/profile/employment/edit/${experience.entityId}`}
                                className="link-button">

                                Edit

                            </Link>

                            <line
                                className="link-button danger"
                                onClick={() => handleDelete(experience.entityId)}>

                               <a href="#">Delete</a>

                            </line>

                        </div>

                    </div>

                ))
            }

        </div>
    );
}