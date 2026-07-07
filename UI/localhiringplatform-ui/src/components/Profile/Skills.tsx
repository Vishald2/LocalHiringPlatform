import { useEffect, useState }
    from "react";

import { getMySkills, saveMySkills }
    from "../../services/CandidateSkillService";
import { getAllSkills }
    from "../../services/SkillService";

import type { Skill }
    from "../../types/Skill";


export default function Skills() {

    const [skills, setSkills] =
        useState<Skill[]>([]);

    const [selectedSkillIds,
        setSelectedSkillIds] =
        useState<string[]>([]);

    const [skillsLoading,
        setSkillsLoading] = useState(false);


    useEffect(() => {

        async function loadProfile() {

            setSkillsLoading(true);

            try {
                const allSkills = await getAllSkills();

                setSkills(allSkills);

                const mySkills = await getMySkills();

                setSelectedSkillIds(mySkills.map(x => x.skillId));
            }
            finally {
                setSkillsLoading(false);
            }
        }

        loadProfile();

    }, []);

    async function handleSaveSkills() {

        try {

            await saveMySkills(
                selectedSkillIds);

            alert(
                "Skills saved successfully.");
        }
        catch {

            alert(
                "Unable to save skills.");
        }
    }
    return (


<div
    className="card"
    style={{
        width: "400px",
        minWidth: "400px",
        position: "sticky",
        top: "20px"
    }}
>

    <h3>
        Skills
    </h3>

    {skillsLoading ? (
        <div className="loading-text">Loading skills...</div>
    ) : (
        skills.map(skill => (

            <div
                key={skill.entityId}
                style={{
                    marginBottom: "8px"
                }}
            >
        <label>

            <input
                type="checkbox"
                checked={
                    selectedSkillIds.includes(
                        skill.entityId)
                }
                onChange={(e) => {

                    if (e.target.checked) {

                        setSelectedSkillIds(
                            [
                                ...selectedSkillIds,
                                skill.entityId
                            ]);
                    }
                    else {

                        setSelectedSkillIds(
                            selectedSkillIds
                                .filter(
                                    id =>
                                        id !==
                                        skill.entityId));
                    }
                }}
            />

            {" "}

            {skill.skillName}

            {" "}

            (
            {skill.industryType}
            )

        </label>

            </div>
        )
        )
    )}
    <div
        style={{ marginTop: "20px" }}
    >

        <button
            className="primary-button"
            onClick={handleSaveSkills}
        >
            Save Skills
        </button>

    </div>
</div>
)}