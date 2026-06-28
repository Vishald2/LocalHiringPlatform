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

    const [isLoading, setIsLoading] = useState(true);

    useEffect(() => {

        let mounted = true;

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

                if (mounted) {
                    setSuccess(true);

                    setMessage("Email verified successfully.");
                }
            }
            catch {

                setSuccess(false);

                setMessage("Invalid or expired verification link.");
            }
            finally {
                setIsLoading(false);
            }
        }

        verify();

        return () => {

            mounted = false;
        };

    }, []);

    return (

        <div className="page-container">

            <div className="card">

                <h2>

                        {
                            isLoading
                            ? "Verifying Email..."
                                : success
                                    ? "Email Verified"
                                    : "Verification Failed"
                        }

                </h2>

                <p>
                    {message}
                </p>

                {
                    !isLoading &&

                    <Link
                        to="/login"
                        className="primary-button">

                        Go To Login

                    </Link>
                }

            </div>

        </div>
    );
}