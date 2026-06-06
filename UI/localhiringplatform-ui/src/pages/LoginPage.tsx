import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import { login } from "../services/AuthService";

export default function LoginPage() {

    const [form, setForm] = useState({
        emailOrMobile: "",
        password: ""
    });

    const navigate = useNavigate();

    async function handleLogin() {

        try {

            const result = await login({
                emailOrMobile: form.emailOrMobile,
                password: form.password
            });

            localStorage.setItem(
                "token",
                result.token);

            localStorage.setItem(
                "role",
                result.role);

            localStorage.setItem("token", result.token);

            navigate("/dashboard");
        }
        catch (error) {

            if (error instanceof Error) {
                alert(error.message);
            }
        }
    }

    return (
        <div className="center-page">

            <div className="form-card">

                <h2 className="form-title">
                    Candidate Login
                </h2>

                <div className="form-group">

                    <label className="form-label">
                        Email or Mobile
                    </label>

                    <input
                        className="form-control"
                        type="text"
                        value={form.emailOrMobile}
                        onChange={(e) =>
                            setForm({
                                ...form,
                                emailOrMobile: e.target.value
                            })
                        }
                    />
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
                            setForm({
                                ...form,
                                password: e.target.value
                            })
                        }
                    />
                </div>

                <button
                    className="primary-button"
                    onClick={handleLogin}>
                    Login
                </button>

                <div className="form-footer">

                    <span>
                        Don't have an account?
                    </span>

                    <br />

                    <a href="/register">
                        Register Here
                    </a>

                </div>

            </div>

        </div>
    );
}