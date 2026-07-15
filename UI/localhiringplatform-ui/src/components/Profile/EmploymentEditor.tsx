import { useEffect, useState } from "react";
import { addCandidateExperience, getCandidateExperience, updateCandidateExperience } from "../../services/Experience/CandidateExperiences";
import type { CandidateExperienceCreateModel } from "../../types/Experience/CandidateExperienceCreateModel";
import { getIndustryTypes } from "../../services/MasterData/MasterService";
import type { IndustryTypeResponseModel } from "../../types/IndustryTypeResponseModel";

export interface EmploymentEditorProps {

    mode: string;
    candidateExperienceEntityId?: string | undefined;

    onCancel: () => void;

    onSaved: () => void;

}

export default function EmploymentEditor(
    props: EmploymentEditorProps) {

    const [industryTypes, setIndustryTypes] =
        useState<IndustryTypeResponseModel[]>([]);

    const [model, setModel] =
        useState<CandidateExperienceCreateModel>({
            entityId: undefined,

            companyName: "",

            designation: "",

            industryTypeId: 0,

            city: "",

            state: "",

            country: "",

            startDate: "",

            endDate: undefined,

            isCurrentCompany: false,

            summary: ""
        });

    async function loadIndustryTypes() {

        const response =
            await getIndustryTypes();

        setIndustryTypes(response);
    }

    async function loadMasters() {

        await loadIndustryTypes();

        // Later
        // await loadStates();
        // await loadCountries();
    }

    useEffect(() => {
        async function loadProfile() {

            if (props.mode === "EmploymentEdit" && props.candidateExperienceEntityId) {

                const result = await getCandidateExperience(
                    props.candidateExperienceEntityId!)

                console.log(result);

                setModel(result);
            }
        }

        async function loadIndustryTypes() {

            const response =
                await getIndustryTypes();

            console.log("loadIndustryTypes", response);

            setIndustryTypes(response);
        }

        loadIndustryTypes();

        loadProfile();

    }, []);


    async function save() {

        if (props.mode === "EmploymentAdd") {
            await addCandidateExperience(model);
        }
        else {
            await updateCandidateExperience(model);
        }

        props.onSaved();
    }



    return (

        <div className="editor-container">

            <h1>
                {
                    props.mode === "EmploymentAdd"
                        ? "Add Employment"
                        : "Edit Employment"
                }
            </h1>

            <div className="form-group">

                <label>Company Name</label>

                <input
                    type="text"
                    value={model.companyName}
                    onChange={e =>
                        setModel({
                            ...model,
                            companyName: e.target.value
                        })}
                />

            </div>

            <div className="form-group">

                <label>Designation</label>

                <input
                    type="text"
                    value={model.designation}
                    onChange={e =>
                        setModel({
                            ...model,
                            designation: e.target.value
                        })}
                />

            </div>

            <div className="form-group">

                <label>Industry</label>

                <select
                    value={model.industryTypeId}
                    onChange={e =>
                        setModel({
                            ...model,
                            industryTypeId: Number(e.target.value)
                        })}
                >

                    <option value={0}>
                        Select Industry
                    </option>

                    {
                        industryTypes.map((industry, index) =>(

                            <option
                                key={index}
                                value={industry.industryTypeId}
                            >
                                {industry.name}
                            </option>

                        ))
                    }

                </select>

            </div>

            <div className="form-group">

                <label>City</label>

                <input
                    type="text"
                    value={model.city}
                    onChange={e =>
                        setModel({
                            ...model,
                            city: e.target.value
                        })}
                />

            </div>

            <div className="form-group">

                <label>State</label>

                <input
                    type="text"
                    value={model.state}
                    onChange={e =>
                        setModel({
                            ...model,
                            state: e.target.value
                        })}
                />

            </div>

            <div className="form-group">

                <label>Country</label>

                <input
                    type="text"
                    value={model.country}
                    onChange={e =>
                        setModel({
                            ...model,
                            country: e.target.value
                        })}
                />

            </div>

            <div className="form-group">

                <label>Start Date</label>

                <input
                    type="date"
                    value={model.startDate}
                    onChange={e =>
                        setModel({
                            ...model,
                            startDate: e.target.value
                        })}
                />

            </div>

            <div className="form-group">

                <label>

                    <input
                        type="checkbox"
                        checked={model.isCurrentCompany}
                        onChange={e =>
                            setModel({
                                ...model,
                                isCurrentCompany: e.target.checked,
                                endDate: e.target.checked
                                    ? undefined
                                    : model.endDate
                            })}
                    />

                    {" "}Current Company

                </label>

            </div>

            <div className="form-group">

                <label>End Date</label>

                <input
                    type="date"
                    disabled={model.isCurrentCompany}
                    value={model.endDate ?? ""}
                    onChange={e =>
                        setModel({
                            ...model,
                            endDate:
                                e.target.value === ""
                                    ? undefined
                                    : e.target.value
                        })}
                />

            </div>

            <div className="form-group">

                <label>Summary</label>

                <textarea
                    rows={5}
                    value={model.summary}
                    onChange={e =>
                        setModel({
                            ...model,
                            summary: e.target.value
                        })}
                />

            </div>

            <div className="button-panel">

                <button
                    className="primary-button"
                    onClick={save}
                >
                    Save
                </button>

                <button
                    className="secondary-button"
                    onClick={props.onCancel}
                >
                    Cancel
                </button>

            </div>

        </div>

    );
}
