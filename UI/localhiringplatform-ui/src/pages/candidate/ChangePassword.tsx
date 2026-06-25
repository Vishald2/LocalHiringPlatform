import { useState } from "react";
import { changePassword } from "../../services/AuthService";

export default function ChangePasswordPage() {

    const [currentPassword, setCurrentPassword] =
        useState("");

    const [newPassword, setNewPassword] =
        useState("");

    const [confirmPassword, setConfirmPassword] =
        useState("");

    const [loading, setLoading] =
        useState(false);

    const [successMessage, setSuccessMessage] =
        useState("");

    const [errorMessage, setErrorMessage] =
        useState("");

    const handleSubmit = async (
        e: React.FormEvent<HTMLFormElement>) => {

        e.preventDefault();

        setLoading(true);

        setSuccessMessage("");

        setErrorMessage("");

        try {

            await changePassword({
                currentPassword,
                newPassword,
                confirmPassword
            });

            setSuccessMessage(
                "Password changed successfully.");

            setCurrentPassword("");

            setNewPassword("");

            setConfirmPassword("");

        }
        catch (error: any) {

            setErrorMessage(
                error.response?.data?.message ??
                error.message ??
                "Unable to change password.");

        }
        finally {

            setLoading(false);
        }
    }

    return (

        <div className="page-container">

            <div className="form-card form-card-large">

                <h2 className="form-title">
                    Change Password
                </h2>

                <form onSubmit={handleSubmit}>

                    <div className="form-group">

                        <label className="form-label">
                            Current Password
                        </label>

                        <input
                            className="form-control"
                            type="password"
                            value={currentPassword}
                            onChange={
                                (e) =>
                                    setCurrentPassword(
                                        e.target.value)}
                            required
                        />
                    </div>

                    <div className="form-group">


                        <label className="form-label">
                            New Password
                        </label>


                        <input
                            className="form-control"
                            type="password"
                            value={newPassword}
                            onChange={
                                (e) =>
                                    setNewPassword(
                                        e.target.value)}
                            required
                        />


                    </div>


                    <div className="form-group">


                        <label className="form-label">
                            Confirm Password
                        </label>


                        <input
                            className="form-control"
                            type="password"
                            value={confirmPassword}
                            onChange={
                                (e) =>
                                    setConfirmPassword(
                                        e.target.value)}
                            required
                        />


                    </div>

                    {
                        successMessage &&

                        <div className="validation-success">
                            {successMessage}
                        </div>
                    }

                    {
                        errorMessage &&

                        <div className="validation-error">
                            {errorMessage}
                        </div>
                    }

                    <div
                        style={
                            {
                                marginTop: "20px"
                            }
                        }
                    >

                        <button
                            type="submit"
                            className="primary-button"
                            disabled={loading}
                        >
                            {
                                loading
                                    ? "Changing Password..."
                                    : "Change Password"
                            }
                        </button>

                    </div>

                </form>

            </div>

        </div>

    );
}
