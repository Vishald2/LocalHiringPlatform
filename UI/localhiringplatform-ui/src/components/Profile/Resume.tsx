import { useEffect, useState }
    from "react";

import type { CandidateProfile }
    from "../../types/CandidateProfile";
import { getProfile, uploadResume } from "../../services/CandidateProfileService";
import { API_BASE_URL } from "../../config/api";

export default function Resume() {

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
        });

    const [selectedFile,
        setSelectedFile] =
        useState<File | null>(null);
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

            alert("Resume uploaded successfully.");

            // loadProfile();
        }
        catch {
            alert("Resume upload failed.");
        }
    }

    useEffect(() => {

        async function loadProfile() {

            const result = await getProfile();
            setProfile(result);
        }

        loadProfile();

    }, []);

    return (

        <div>

            <h3>
                Resume
            </h3>

            <input
                type="file"
                accept=".pdf,.doc,.docx"
                onChange={handleFileChange}
            />

            <button className="primary-button"
                style={{ width: "150px" }}
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
                            <a
                                href={`${API_BASE_URL}${profile.resumeFilePath}`}
                                target="_blank"
                                rel="noreferrer"
                            >
                                {profile.resumeFileName}
                            </a>
                        }
                    </p>
                )
            }
        </div>
    )
}