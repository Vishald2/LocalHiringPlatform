import { useEffect, useState }
    from "react";

import type { CandidateProfile }
    from "../../types/CandidateProfile";
import { getProfile, updateProfile } from "../../services/CandidateProfileService";

export default function BasicInfo() {

    const [profile, setProfile] =
        useState<CandidateProfile>({
            fullName: "",
            dateOfBirth: null,
            gender: null,
            city: "",
            state: "",
            currentSalary: null,
            expectedSalary: null,
            totalExperienceYears: null,
            profileSummary: "",
            isOpenToWork: false,
            profileCompletionPercentage: 0,
            emailVerified: false,
            mobileVerified: false,
            mobileNumber: "",
            email: ""
        });

    const [successMessage, setSuccessMessage] =
        useState("");

    useEffect(() => {

        async function loadProfile() {

            const result = await getProfile();

            setProfile(result);

        }

        loadProfile();

    }, []);

    async function handleSave() {

        try {

            await updateProfile(profile);

            const updatedProfile =
                await getProfile();

            setProfile(updatedProfile);

            setSuccessMessage(
                "Profile updated successfully.");
        }
        catch {

            setSuccessMessage("Unable to update profile.");
        }
    }

    function handleChange(
        field: keyof CandidateProfile,
        value: CandidateProfile[keyof CandidateProfile]) {
        setProfile({
            ...profile,
            [field]: value
        });
    }

    return (

        <div className="page-container">

            <div
                style={{
                    display: "flex",
                    gap: "30px",
                    alignItems: "flex-start"
                }}
            >

                {/* Left Side */}

                <div
                    className="form-card form-card-large"
                    style={{
                        flex: 1
                    }}
                >

                    <h2 className="form-title">
                        Candidate Profile
                    </h2>

                    <div className="form-group">

                        <label className="form-label">
                            Full Name
                        </label>

                        <input
                            className="form-control"
                            value={profile.fullName}
                            onChange={(e) =>
                                handleChange(
                                    "fullName",
                                    e.target.value)}
                        />

                    </div>

                    <div className="form-group">

                        <label className="form-label">
                            Mobile Number
                        </label>

                        <div
                            style={{
                                display: "flex",
                                alignItems: "center",
                                gap: "10px"
                            }}
                        >
                            <div
                                className="form-control"
                                title={profile.mobileNumber}
                                style={{ flex: 1 }}
                            >
                                {profile.mobileNumber}
                            </div>

                            {
                                profile.mobileVerified &&

                                <span
                                    style={{
                                        color: "green",
                                        fontWeight: "bold",
                                        fontSize: "24px"
                                    }}
                                >
                                    ✓
                                </span>
                            }
                        </div>

                    </div>

                    <div className="form-group">

                        <label className="form-label">
                            Date Of Birth
                        </label>

                        <input
                            className="form-control"
                            type="date"
                            value={
                                profile.dateOfBirth
                                    ? profile.dateOfBirth.substring(0, 10)
                                    : ""
                            }
                            onChange={(e) =>
                                handleChange(
                                    "dateOfBirth",
                                    e.target.value)}
                        />

                    </div>

                    <div className="form-group">

                        <label className="form-label">
                            Gender
                        </label>

                        <select
                            className="form-control"
                            value={profile.gender ?? ""}
                            onChange={(e) =>
                                handleChange(
                                    "gender",
                                    Number(e.target.value))}
                        >
                            <option value="">
                                Select Gender
                            </option>

                            <option value="1">
                                Male
                            </option>

                            <option value="2">
                                Female
                            </option>

                            <option value="3">
                                Other
                            </option>

                            <option value="4">
                                Prefer Not To Say
                            </option>

                        </select>

                    </div>

                    <div className="form-group">

                        <label className="form-label">
                            City
                        </label>

                        <input
                            className="form-control"
                            value={profile.city}
                            onChange={(e) =>
                                handleChange(
                                    "city",
                                    e.target.value)}
                        />

                    </div>

                    <div className="form-group">

                        <label className="form-label">
                            State
                        </label>

                        <input
                            className="form-control"
                            value={profile.state}
                            onChange={(e) =>
                                handleChange(
                                    "state",
                                    e.target.value)}
                        />

                    </div>

                    <div className="form-group">

                        <label className="form-label">
                            Current Salary
                        </label>

                        <input
                            className="form-control"
                            type="number"
                            value={profile.currentSalary ?? ""}
                            onChange={(e) =>
                                handleChange(
                                    "currentSalary",
                                    e.target.value === ""
                                        ? null
                                        : Number(e.target.value))
                            }
                        />

                    </div>

                    <div className="form-group">

                        <label className="form-label">
                            Expected Salary
                        </label>

                        <input
                            className="form-control"
                            type="number"
                            value={profile.expectedSalary ?? ""}
                            onChange={(e) =>
                                handleChange(
                                    "expectedSalary",
                                    Number(e.target.value))}
                        />

                    </div>

                    <div className="form-group">

                        <label className="form-label">
                            Total Experience (Years)
                        </label>

                        <input
                            className="form-control"
                            type="number"
                            value={
                                profile.totalExperienceYears
                                ?? ""
                            }
                            onChange={(e) =>
                                handleChange(
                                    "totalExperienceYears",
                                    Number(e.target.value))}
                        />

                    </div>

                    <div className="form-group">

                        <label className="form-label">
                            Profile Summary
                        </label>

                        <textarea
                            className="form-textarea"
                            value={profile.profileSummary}
                            onChange={(e) =>
                                handleChange(
                                    "profileSummary",
                                    e.target.value)}
                        />

                    </div>

                    <div className="checkbox-group">

                        <label className="checkbox-label">

                            <input
                                className="form-checkbox"
                                type="checkbox"
                                checked={profile.isOpenToWork}
                                onChange={(e) =>
                                    handleChange(
                                        "isOpenToWork",
                                        e.target.checked)}
                            />

                            Open To Work

                        </label>

                    </div>

                    <div
                        style={{
                            marginTop: "20px"
                        }}
                    >

                        <strong>
                            Profile Completion:
                        </strong>

                        {" "}

                        {
                            profile.profileCompletionPercentage
                        }

                        %

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

                {/* Right Side */}



            </div>

        </div>
    );

}