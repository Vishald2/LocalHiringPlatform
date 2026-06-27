import { useState } from "react";

import { addSkill} from "../../services/SkillService";

import type { Skill }
    from "../../types/Skill";

export default function ManageSkills() {

    const [skill, setSkill] =
        useState<Skill>({
            skillName: "",
            skillCategory: 0,
            isApproved: false,
            entityId: "",
            industryType:""
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
                    Create Skill
                </h2>

                <div className="form-group">

                    <label className="form-label">
                        Skill Name
                    </label>

                    <input
                        className="form-control"
                        value={skill.skillName}
                        onChange={(e) =>
                            handleChange(
                                "skillName",
                                e.target.value)}
                    />

                </div>

                <div className="form-group">

                    <label className="form-label">
                        Skill Category
                    </label>

                    <input
                        className="form-control"
                        value={skill.skillCategory}
                        onChange={(e) =>
                            handleChange(
                                "skillCategory",
                                Number(e.target.value))}
                    />

                </div>

                <div className="checkbox-group">

                    <label className="checkbox-label">

                        <input
                            className="form-checkbox"
                            type="checkbox"
                            checked={
                                skill.isApproved
                            }
                            onChange={(e) =>
                                handleChange(
                                    "isApproved",
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