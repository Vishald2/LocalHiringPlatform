import { useEffect, useState } from "react";
import { Link, useSearchParams } from "react-router-dom";
import { verifyEmail } from "../services/AuthService";

export default function VerifyEmailPage() {

    const [searchParams] =
        useSearchParams();

    const [success,
        setSuccess] =
        useState(false);

    const [message,
        setMessage] =
        useState("Verifying email...");

    useEffect(() => {

        async function verify() {

            try {

                const token =
                    searchParams.get(
                        "token");

                if (!token) {

                    setMessage(
                        "Verification token is missing.");

                    return;
                }

                await verifyEmail(
                    token);

                setSuccess(true);

                setMessage("Email verified successfully.");
            }
            catch {

                setSuccess(false);

                setMessage("Invalid or expired verification link.");
            }
        }

        verify();

    }, []);

    return (

        <div className="page-container">

            <div className="card">

                <h2>

                    {
                        success
                            ? "Email Verified"
                            : "Verification Failed"
                    }

                </h2>

                <p>
                    {message}
                </p>

                <Link
                    to="/login"
                    className="primary-button">

                    Go To Login

                </Link>

            </div>

        </div>
    );
}