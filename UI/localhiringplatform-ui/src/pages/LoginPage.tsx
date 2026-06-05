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
        <div>
            <h2>Candidate Login</h2>

            <div>
                <label>Email or Mobile</label>

                <input
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

            <div>
                <label>Password</label>

                <input
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
                type="button"
                onClick={handleLogin}>
                Login
            </button>
        </div>
    );
}