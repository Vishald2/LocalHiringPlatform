import { useEffect, useState }
    from "react";

import { getProfile, updateProfile, uploadResume }
    from "../../services/CandidateProfileService";
import { getMySkills, saveMySkills }
    from "../../services/CandidateSkillService";
import { getAllSkills }
    from "../../services/SkillService";

import type { CandidateProfile }
    from "../../types/CandidateProfile";
import type { Skill }
    from "../../types/Skill";

declare global {
    interface Window {
        initSendOTP: (config: any) => void;
        sendOtp: any;

        verifyOtp: any;
    }

    interface Window {
        verifyOtp: any;
    }
}
export default function CandidateProfilePage() {

    const [otp, setOtp] = useState("");
    const [showOtpBox, setShowOtpBox] = useState(false);

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
            mobileNumber: null,
        });

    const [successMessage, setSuccessMessage] =
        useState("");

    const [selectedFile,
        setSelectedFile] =
        useState<File | null>(null);

    const [skills, setSkills] =
        useState<Skill[]>([]);

    const [selectedSkillIds,
        setSelectedSkillIds] =
        useState<string[]>([]);

    function handleFileChange(
        e: React.ChangeEvent<HTMLInputElement>) {
        if (e.target.files &&
            e.target.files.length > 0) {
            setSelectedFile(
                e.target.files[0]);
        }
    }

    async function handleUploadResume() {
        if (!selectedFile) {
            alert(
                "Please select a file.");

            return;
        }

        try {
            await uploadResume(selectedFile);

            const result = await getProfile();

            setProfile(result);

            alert(
                "Resume uploaded successfully.");

           // loadProfile();
        }
        catch {
            alert(
                "Resume upload failed.");
        }
    }

    useEffect(() => {

        async function loadProfile() {

            const result = await getProfile();

            setProfile(result);

            const allSkills = await getAllSkills();

            setSkills(allSkills);

            const mySkills = await getMySkills();

            setSelectedSkillIds(mySkills.map(x => x.skillId));
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

                    <div className="card">

                        <h3>
                            Resume
                        </h3>

                        <input
                            type="file"
                            accept=".pdf,.doc,.docx"
                            onChange={handleFileChange}
                        />

                        <button
                            onClick={handleUploadResume}
                        >
                            Upload Resume
                        </button>

                        {
                            profile.resumeFileName &&
                            (
                                <p>

                                    <br />

                                    <b>
                                        Uploaded Resume:
                                    </b>

                                    {" "}

                                    {
                                        profile.resumeFileName
                                    }

                                </p>
                            )
                        }

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

                    {
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
                        ))
                    }

                    <div
                        style={{marginTop: "20px" }}
                    >

                        <button
                            className="primary-button"
                            onClick={handleSaveSkills}
                        >
                            Save Skills
                        </button>

                    </div>
                    <p></p>
                    <div>
                        {
                            profile.mobileNumber &&
                            !profile.mobileVerified &&

                            <div
                                style={{
                                    marginTop: "15px"
                                }}
                            >
                                <button
                                    type="button"
                                    className="secondary-button"
                                    onClick={() => {

                                        window.sendOtp(
                                            "91" + profile.mobileNumber,
                                            (data: any) => {

                                                console.log("SUCCESS", data);

                                                setShowOtpBox(true);
                                                alert("OTP sent successfully.");
                                            },
                                            (error: any) => {

                                                console.log("ERROR", error);

                                                alert("Unable to send OTP.");
                                            });
                                    }}
                                >
                                    Verify Mobile
                                    </button>
                                    {
                                        showOtpBox &&

                                        <div style={{ marginTop: "15px" }}>

                                            <input
                                                type="text"
                                                placeholder="Enter OTP"
                                                value={otp}
                                                onChange={(e) => setOtp(e.target.value)}
                                            />

                                            <button
                                                type="button"
                                                className="secondary-button"
                                                style={{ marginLeft: "10px" }}
                                                onClick={() => {

                                                    window.verifyOtp(
                                                        otp,
                                                        (data: any) => {

                                                            console.log("VERIFY SUCCESS");
                                                            console.log(data);

                                                            alert("OTP verified successfully.");

                                                        },
                                                        (error: any) => {

                                                            console.log("VERIFY FAILED");
                                                            console.log(error);

                                                            alert("Invalid OTP.");

                                                        }
                                                    );

                                                }}
                                            >
                                                Verify OTP
                                            </button>

                                        </div>
                                    }
                            </div>
                        }
                        {
                            profile.mobileVerified &&

                            <div
                                style={{
                                    marginTop: "15px",
                                    color: "green",
                                    fontWeight: "bold"
                                }}
                            >
                                ✓ Mobile Verified
                            </div>
                        }
                    </div>
                </div>

            </div>

        </div>
    );

}