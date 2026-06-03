import { useState } from "react";
import type { CandidateRegisterRequest } from "../types/CandidateRegisterRequest";

export default function CandidateRegisterPage() {

    const [form, setForm] = useState<CandidateRegisterRequest>({
        fullName: "",
        email: "",
        mobileNumber: "",
        password: "",
        confirmPassword: "",
        acceptTerms: false
    });

    console.log(form);

    return (
        <div>
            <h1>Candidate Registration</h1>
            <pre>
                {JSON.stringify(form, null, 2)}
            </pre>

            <label>Full Name</label>

            <input
                type="text"
                value={form.fullName}
                onChange={(e) =>
                    setForm({
                        ...form,
                        fullName: e.target.value
                    })
                }
            />
        </div>
    );
}