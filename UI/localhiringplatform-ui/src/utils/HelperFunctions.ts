import { applyToJob } from "../services/JobApplicationService";
import { getErrorMessage } from "./errorHelper";

export async function handleApply(
    jobId: string) {
    try {
        await applyToJob({
            jobId
        });

        alert("Application submitted");
    }
    catch (error) {
        alert(getErrorMessage(error));
    }
}