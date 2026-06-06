import { useState } from "react";
import { Link } from "react-router-dom";
import type { CandidateRegisterRequest } from "../types/CandidateRegisterRequest";
import type { CandidateRegisterErrors } from "../types/CandidateRegisterErrors";
import { registerCandidate } from "../services/AuthService";
import { useNavigate } from "react-router-dom";

export default function CandidateRegisterPage() {

    const [form, setForm] = useState<CandidateRegisterRequest>({
        email: "",
        mobileNumber: "",
        password: "",
        confirmPassword: "",

        acceptTerms: false,
        role: 0
    });

    const passwordsMatch =
        form.password === form.confirmPassword;

    const [errors, setErrors] =
        useState<CandidateRegisterErrors>({
            email: "",
            mobileNumber: "",
            password: "",
            confirmPassword: "",
            acceptTerms: "",
            role: ""
        });

    const navigate = useNavigate();

    function handleInputChange(
        field: keyof CandidateRegisterRequest,
        value: string | boolean | number
    ) {
        let v: string | boolean | number = value;

        if (field === "role") {
            // ensure role is stored as a number
            v = Number(value);
        }

        setForm(prev => ({ ...prev, [field]: v } as CandidateRegisterRequest));
    }

    async function handleRegister() {

        console.log("Form Data:", form);

        const isValid = validateForm();

        if (!isValid) {
            console.log("Form validation failed. Errors:", errors);
            return;
        }


        console.log("Form is valid. Proceeding with registration...");
        try {

            // await registerCandidate(form);
            await registerCandidate({
                fullname: "",
                email: form.email,
                mobileNumber: form.mobileNumber,
                password: form.password,
                role: Number(form.role)
            });

            alert(
                "Candidate registered successfully");

            navigate("/login");
        }
        catch (error) {
            if (error instanceof Error) {
                alert(error.message);
            }
        }
    }
    function validateForm() {

        const newErrors: CandidateRegisterErrors = {
            email: "",
            mobileNumber: "",
            password: "",
            confirmPassword: "",
            acceptTerms: "",
            role: ""
        };

        // Full Name

        //if (!form.fullName || !form.fullName.trim()) {
        //    newErrors.fullName = "Full Name is required";
        //}
        //else if (form.fullName.trim().length < 3) {
        //    newErrors.fullName =
        //        "Full Name must be at least 3 characters";
        //}
        //else if (form.fullName.trim().length > 100) {
        //    newErrors.fullName =
        //        "Full Name cannot exceed 100 characters";
        //}

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

        if (form.role == 0) {
            newErrors.role =
                "Register As is required";
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
        <div className="center-page">

            <div className="form-card form-card-large">

                <h2 className="form-title">
                    Candidate Registration
                </h2>

                <div className="form-group">

                    <label className="form-label">
                        Email
                    </label>

                    <input
                        className="form-control"
                        type="text"
                        value={form.email}
                        onChange={(e) =>
                            handleInputChange(
                                "email",
                                e.target.value)
                        }
                    />

                    {
                        errors.email &&
                        <span className="validation-error">
                            {errors.email}
                        </span>
                    }

                </div>

                <div className="form-group">

                    <label className="form-label">
                        Mobile Number
                    </label>

                    <input
                        className="form-control"
                        type="text"
                        value={form.mobileNumber}
                        onChange={(e) =>
                            handleInputChange(
                                "mobileNumber",
                                e.target.value)
                        }
                    />

                    {
                        errors.mobileNumber &&
                        <span className="validation-error">
                            {errors.mobileNumber}
                        </span>
                    }

                </div>

                <div className="form-group">

                    <label className="form-label">
                        Register As
                    </label>

                    <select
                        className="form-control"
                        value={form.role}
                        onChange={(e) =>
                            handleInputChange(
                                "role",
                                e.target.value)
                        }
                    >
                        <option value="0">
                            ---Select Role ---
                        </option>
                        <option value="1">
                            Candidate
                        </option>

                        <option value="2">
                            Employer
                        </option>

                    </select>
                    {
                        errors.role &&
                        <span className="validation-error">
                            {errors.role}
                        </span>
                    }
                </div>

                <div className="form-group">

                    <label className="form-label">
                        Password
                    </label>

                    <input
                        className="form-control"
                        type="password"
                        value={form.password}
                        onChange={(e) =>
                            handleInputChange(
                                "password",
                                e.target.value)
                        }
                    />

                    {
                        errors.password &&
                        <span className="validation-error">
                            {errors.password}
                        </span>
                    }

                </div>

                <div className="form-group">

                    <label className="form-label">
                        Confirm Password
                    </label>

                    <input
                        className="form-control"
                        type="password"
                        value={form.confirmPassword}
                        onChange={(e) =>
                            handleInputChange(
                                "confirmPassword",
                                e.target.value)
                        }
                    />

                    {
                        errors.confirmPassword &&
                        <span className="validation-error">
                            {errors.confirmPassword}
                        </span>
                    }

                    {
                        !passwordsMatch &&
                        !errors.confirmPassword &&
                        form.confirmPassword.length > 0 &&
                        (
                            <span className="validation-error">
                                Passwords do not match
                            </span>
                        )
                    }

                </div>

                <div className="form-group checkbox-group">

                    <label className="checkbox-label">

                        <input
                            className="form-checkbox"
                            type="checkbox"
                            checked={form.acceptTerms}
                            onChange={(e) =>
                                handleInputChange(
                                    "acceptTerms",
                                    e.target.checked
                                )
                            }
                        />

                        I accept Terms & Conditions

                    </label>

                    {
                        errors.acceptTerms &&
                        <span className="validation-error">
                            {errors.acceptTerms}
                        </span>
                    }

                </div>

                <button
                    className="primary-button"
                    onClick={handleRegister}
                >
                    Register
                </button>

                <div className="form-footer">

                    <span>
                        Already have an account?
                    </span>

                    <br />

                    <Link to="/login">
                        Login
                    </Link>

                </div>

            </div>

        </div>
    );
}