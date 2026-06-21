import { useEffect, useState } from "react";

import {
    getProfile,
    updateProfile
}
    from "../../services/employerDashboardService";

import type { EmployerProfile } from "../../types/EmployerProfile";

export default function EmployerProfilePage() {

    const [profile,
        setProfile] =
        useState<EmployerProfile>({
            userId: "",
            companyName: "",
            industry: "",
            website: "",
            companyDescription: "",
            isEmailVerified: false
        });

    useEffect(() => {

        async function loadProfile() {

            try {

                const data =
                    await getProfile();

                setProfile(data);
            }
            catch  {

                //handleError(error);
            }
        }

        loadProfile();

    }, []);



    async function handleSubmit(
        e: React.FormEvent) {

        e.preventDefault();

        try {

            await updateProfile(
                profile);

            alert(
                "Profile updated successfully");
        }
        catch (error) {

            alert(error);
        }
    }

    return (

        <div className="page-container">

            <div className="form-card form-card-large">

                <h2 className="form-title">
                    Employer Profile
                </h2>

                <form onSubmit={handleSubmit}>

                    <div className="form-group">

                        <label className="form-label">
                            Company Name
                        </label>

                        <input
                            className="form-control"
                            value={
                                profile.companyName ?? ""
                            }
                            onChange={(e) =>
                                setProfile({
                                    ...profile,
                                    companyName:
                                        e.target.value
                                })
                            }
                        />

                    </div>

                    <div className="form-group">

                        <label className="form-label">
                            Industry
                        </label>

                        <input
                            className="form-control"
                            value={
                                profile.industry ?? ""
                            }
                            onChange={(e) =>
                                setProfile({
                                    ...profile,
                                    industry:
                                        e.target.value
                                })
                            }
                        />

                    </div>

                    <div className="form-group">

                        <label className="form-label">
                            Website
                        </label>

                        <input
                            className="form-control"
                            value={
                                profile.website ?? ""
                            }
                            onChange={(e) =>
                                setProfile({
                                    ...profile,
                                    website:
                                        e.target.value
                                })
                            }
                        />

                    </div>

                    <div className="form-group">

                        <label className="form-label">
                            Company Description
                        </label>

                        <textarea
                            className="form-textarea"
                            rows={5}
                            value={
                                profile.companyDescription ?? ""
                            }
                            onChange={(e) =>
                                setProfile({
                                    ...profile,
                                    companyDescription:
                                        e.target.value
                                })
                            }
                        />

                    </div>

                    <div
                        style={{
                            marginTop: "20px"
                        }}
                    >

                        <strong>
                            Email Verification:
                        </strong>

                        {" "}

                        {profile.isEmailVerified
                            ? "Verified"
                            : "Not Verified"}

                    </div>

                    <div
                        style={{
                            marginTop: "20px"
                        }}
                    >

                        <button
                            type="submit"
                            className="primary-button"
                        >
                            Save Profile
                        </button>

                    </div>

                </form>

            </div>

        </div>
    );
}