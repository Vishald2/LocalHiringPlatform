import { useState } from "react";
import { Link } from "react-router-dom";
import type { CandidateRegisterRequest } from "../types/CandidateRegisterRequest";
import type { CandidateRegisterErrors } from "../types/CandidateRegisterErrors";
import "../styles/CandidateRegisterPage.css";
import { registerCandidate }  from "../services/AuthService";

export default function CandidateRegisterPage() {

    const [form, setForm] = useState<CandidateRegisterRequest>({
        fullName: "",
        email: "",
        mobileNumber: "",
        password: "",
        confirmPassword: "",

        acceptTerms: false
    });

    const passwordsMatch =
        form.password === form.confirmPassword;

    const [errors, setErrors] =
        useState<CandidateRegisterErrors>({
            fullName: "",
            email: "",
            mobileNumber: "",
            password: "",
            confirmPassword: "",
            acceptTerms: ""
        });

    function handleInputChange(
        field: keyof CandidateRegisterRequest,
        value: string | boolean
    ) {
        setForm({
            ...form,
            [field]: value
        });
    }

    async function handleRegister() {

        const isValid = validateForm();

        if (!isValid) {
            return;
        }

        try {

            // await registerCandidate(form);
            await registerCandidate({
                fullName: form.fullName,
                email: form.email,
                mobileNumber: form.mobileNumber,
                password: form.password
            });

            alert(
                "Candidate registered successfully");

        }
        catch (error) {

            console.error(error);

            alert(
                "Registration failed");
        }
    }
    function validateForm() {

        const newErrors: CandidateRegisterErrors = {
            fullName: "",
            email: "",
            mobileNumber: "",
            password: "",
            confirmPassword: "",
            acceptTerms: ""
        };

        // Full Name

        if (!form.fullName.trim()) {
            newErrors.fullName = "Full Name is required";
        }
        else if (form.fullName.trim().length < 3) {
            newErrors.fullName =
                "Full Name must be at least 3 characters";
        }
        else if (form.fullName.trim().length > 100) {
            newErrors.fullName =
                "Full Name cannot exceed 100 characters";
        }

        // Email

        if (!form.email.trim()) {
            newErrors.email = "Email is required";
        }
        else if (
            !/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(form.email)
        ) {
            newErrors.email =
                "Please enter a valid email address";
        }

        // Mobile Number

        if (!form.mobileNumber.trim()) {
            newErrors.mobileNumber =
                "Mobile Number is required";
        }
        else if (
            !/^[0-9]{10}$/.test(form.mobileNumber)
        ) {
            newErrors.mobileNumber =
                "Mobile Number must be exactly 10 digits";
        }

        // Password

        if (!form.password.trim()) {
            newErrors.password = "Password is required";
        }
        else if (form.password.length < 8) {
            newErrors.password =
                "Password must be at least 8 characters";
        }
        else if (
            !/(?=.*[a-z])/.test(form.password)
        ) {
            newErrors.password =
                "Password must contain a lowercase letter";
        }
        else if (
            !/(?=.*[A-Z])/.test(form.password)
        ) {
            newErrors.password =
                "Password must contain an uppercase letter";
        }
        else if (
            !/(?=.*\d)/.test(form.password)
        ) {
            newErrors.password =
                "Password must contain a number";
        }
        else if (
            !/(?=.*[@$!%*?&])/.test(form.password)
        ) {
            newErrors.password =
                "Password must contain a special character";
        }

        // Confirm Password

        if (!form.confirmPassword.trim()) {
            newErrors.confirmPassword =
                "Confirm Password is required";
        }
        else if (
            form.password !== form.confirmPassword
        ) {
            newErrors.confirmPassword =
                "Passwords do not match";
        }

        // Terms

        if (!form.acceptTerms) {
            newErrors.acceptTerms =
                "Please accept Terms & Conditions";
        }

        setErrors(newErrors);

        return Object.values(newErrors)
            .every(error => error === "");
    }

    return (
        <div className="register-page">

            <div className="register-card">

                <h1>Candidate Registration</h1>

                <div className="form-group">
                    <label className="form-label">Full Name</label>

                    <input
                        className="form-input"
                        type="text"
                        value={form.fullName}
                        onChange={(e) =>
                            handleInputChange("fullName", e.target.value)
                        }
                    />

                    {
                        errors.fullName &&
                        <span className="error-message">
                            {errors.fullName}
                        </span>
                    }
                </div>

                <div className="form-group">
                    <label className="form-label">Email</label>

                    <input
                        className="form-input"
                        type="text"
                        value={form.email}
                        onChange={(e) =>
                            handleInputChange("email", e.target.value)
                        }
                    />

                    {
                        errors.email &&
                        <span className="error-message">
                            {errors.email}
                        </span>
                    }
                </div>

                <div className="form-group">
                    <label className="form-label">Mobile Number</label>

                    <input
                        className="form-input"
                        type="text"
                        value={form.mobileNumber}
                        onChange={(e) =>
                            handleInputChange("mobileNumber", e.target.value)
                        }
                    />

                    {
                        errors.mobileNumber &&
                        <span className="error-message">
                            {errors.mobileNumber}
                        </span>
                    }
                </div>

                <div className="form-group">
                    <label className="form-label">Password</label>

                    <input
                        className="form-input"
                        type="password"
                        value={form.password}
                        onChange={(e) =>
                            handleInputChange("password", e.target.value)
                        }
                    />

                    {
                        errors.password &&
                        <span className="error-message">
                            {errors.password}
                        </span>
                    }
                </div>

                <div className="form-group">
                    <label className="form-label">Confirm Password</label>

                    <input
                        className="form-input"
                        type="password"
                        value={form.confirmPassword}
                        onChange={(e) =>
                            handleInputChange("confirmPassword", e.target.value)
                        }
                    />

                    {
                        errors.confirmPassword &&
                        <span className="error-message">
                            {errors.confirmPassword}
                        </span>
                    }

                    {
                        !passwordsMatch &&
                        !errors.confirmPassword &&
                        form.confirmPassword.length > 0 &&
                        (
                            <span className="error-message">
                                Passwords do not match
                            </span>
                        )
                    }
                </div>

                <div className="form-group checkbox-group">

                    <label>
                        <input
                            type="checkbox"
                            checked={form.acceptTerms}
                            onChange={(e) =>
                                handleInputChange(
                                    "acceptTerms",
                                    e.target.checked
                                )
                            }
                        />

                        {" "}I accept Terms & Conditions
                    </label>

                    {
                        errors.acceptTerms &&
                        <span className="error-message">
                            {errors.acceptTerms}
                        </span>
                    }

                </div>

                <button
                    className="register-button"
                    onClick={handleRegister}
                >
                    Register
                </button>
                <p></p>
                <div className="login-link-container">

                    <span>
                        Already have an account?
                    </span>

                    {" "}

                    <Link to="/login">
                        Login
                    </Link>

                </div>
            </div>


        </div>
    );
}