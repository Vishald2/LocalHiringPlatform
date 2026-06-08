import { useState } from "react";

import { addSkill} from "../../services/SkillService";

import type { Skill }
    from "../../types/Skill";

export default function ManageSkills() {

    const [skill, setSkill] =
        useState<Skill>({
            SkillName: "",
            SkillCategory: null,
            IsApproved:false
        });

    const [successMessage, setSuccessMessage] =
        useState("");

    async function handleSave() {

        try {

            await addSkill(skill);

            //const updatedProfile =
            //    await getProfile();

            //setProfile(updatedProfile);

            setSuccessMessage(
                "Profile updated successfully.");
        }
        catch {

            setSuccessMessage(
                "Unable to update profile.");
        }
    }

    function handleChange(
        field: keyof Skill,
        value: Skill[keyof Skill]) {
        setSkill({
            ...skill,
            [field]: value
        });
    }

    return (

        <div className="page-container">

            <div className="form-card form-card-large">

                <h2 className="form-title">
                    Candidate Profile
                </h2>

                <div className="form-group">

                    <label className="form-label">
                        Skill Name
                    </label>

                    <input
                        className="form-control"
                        value={skill.SkillName}
                        onChange={(e) =>
                            handleChange(
                                "SkillName",
                                e.target.value)}
                    />

                </div>

                <div className="form-group">

                    <label className="form-label">
                        Skill Category
                    </label>

                    <input
                        className="form-control"
                        value={skill.SkillCategory}
                        onChange={(e) =>
                            handleChange(
                                "SkillCategory",
                                Number(e.target.value))}
                    />

                </div>

                <div className="checkbox-group">

                    <label className="checkbox-label">

                        <input
                            className="form-checkbox"
                            type="checkbox"
                            checked={
                                skill.IsApproved
                            }
                            onChange={(e) =>
                                handleChange(
                                    "IsApproved",
                                    e.target.checked)}
                        />

                        Is Approved

                    </label>

                </div>
                <div
                    style={{
                        marginTop: "20px"
                    }}
                >

                    <button
                        className="primary-button"
                        onClick={handleSave}
                    >
                        Save Profile
                    </button>

                </div>
            </div>
            {
                successMessage &&
                <div
                    className=
                    "validation-success"
                >
                    {successMessage}
                </div>
            }
        </div>

    );
}