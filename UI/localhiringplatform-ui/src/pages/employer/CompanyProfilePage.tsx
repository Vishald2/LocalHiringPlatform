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

            handleError(error);
        }
    }

    return (
        <div className="container mt-4">

            <h2>
                Employer Profile
            </h2>

            <form
                onSubmit={handleSubmit}>

                <div className="mb-3">

                    <label>
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

                <div className="mb-3">

                    <label>
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

                <div className="mb-3">

                    <label>
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

                <div className="mb-3">

                    <label>
                        Company Description
                    </label>

                    <textarea
                        className="form-control"
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

                <div className="mb-3">

                    <strong>
                        Email Verification:
                    </strong>

                    {" "}

                    {profile.isEmailVerified
                        ? "Verified"
                        : "Not Verified"}
                </div>

                <button
                    type="submit"
                    className="btn btn-primary">

                    Save Profile

                </button>

            </form>

        </div>
    );
}