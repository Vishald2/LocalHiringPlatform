import { useEffect, useState }
    from "react";

import { getProfile }
    from "../../services/CandidateProfileService";
import type { CandidateProfile }
    from "../../types/CandidateProfile";

import { verifyMobile }
    from "../../services/mobileVerificationService";

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

export default function ContactDetails() {

    const [otp, setOtp] = useState("");
    const [showOtpBox, setShowOtpBox] = useState(false);

    const [newMobileNumber,
        setNewMobileNumber] = useState("");

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
            email:""
        });

    const handleSendNewMobileOtp = () => {

        if (!newMobileNumber) {

            alert("Please enter mobile number.");

            return;
        }

        window.sendOtp(

            "91" + newMobileNumber,

            () => {

                setShowOtpBox(true);

                alert("OTP sent successfully.");

            },

            () => {

                alert("Unable to send OTP.");
            });
    }


    const handleVerifyOtp = async () => {

        try {

            window.verifyOtp(otp,

                async (data: any) => {

                    const accessToken = data.message;

                    await verifyMobile({

                        accessToken,

                        mobileNumber: newMobileNumber
                    });

                    async function loadProfile() {

                        const result = await getProfile();

                        setProfile(result);
                    }

                    setOtp("");

                    setNewMobileNumber("");

                    setShowOtpBox(false);

                    await loadProfile();

                    alert("Mobile verified successfully.");
                },

                (error: any) => {

                    console.log(error);

                    alert("Invalid OTP.");
                });

        }
        catch (error) {

            console.error(error);

            alert("Unable to verify mobile.");
        }
    };

    useEffect(() => {

        async function loadProfile() {

            const result = await getProfile();

            console.log("getProfile");
            console.log(result);

            setProfile(result);
        }

        loadProfile();

    }, []);


    return (
        <div>

            <div className="form-group">

                <label className="form-label">
                    Email
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
                        title={profile.email}
                        style={{ flex: 1 }}
                    >
                        {profile.email}
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
                            Update Mobile Number
                        </label>

                        <div
                            style={{
                                display: "flex",
                                gap: "10px",
                                alignItems: "center"
                            }}
                        >
                            <input
                                className="form-control"
                                value={newMobileNumber}
                                onChange={(e) =>
                                    setNewMobileNumber(e.target.value)
                                }
                                style={{ flex: 1 }}
                            />

                            <button
                                type="button"
                                className="secondary-button"
                                onClick={handleSendNewMobileOtp}
                                style={{
                                    width: "140px",
                                    whiteSpace: "nowrap"
                                }}
                            >
                                Get OTP
                            </button>
                        </div>

                    </div>

                    <p></p>
                    <div>
                        {
                            <div
                                style={{
                                    marginTop: "15px"
                                }}
                            >
                            
                                    {
                                        showOtpBox &&

                                        <div
                                            style={{
                                                display: "flex",
                                                gap: "10px",
                                                alignItems: "center",
                                                marginTop: "15px"
                                            }}
                                        >

                                            <input
                                                type="text"
                                                className="form-control"
                                                placeholder="Enter OTP"
                                                value={otp}
                                                onChange={(e) => setOtp(e.target.value)}
                                                style={{ flex: 1 }}
                                            />

                                            <button
                                                type="button"
                                                className="secondary-button"
                                                onClick={handleVerifyOtp}
                                                style={{
                                                    width: "160px",
                                                    whiteSpace: "nowrap"
                                                }}
                                            >
                                                Verify OTP
                                            </button>

                                        </div>
                                    }
                            </div>
                        }

            </div>
        </div>
    );
}