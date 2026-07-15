export interface EmploymentEditorProps {

    mode: string;
    candidateExperienceEntityId: string | undefined;

}

export default function EmploymentEditor(
    props: EmploymentEditorProps) {

    return (

        <div>

            <h1>
                {
                    props.mode === "EmploymentAdd"
                        ? "Add Employment"
                        : "Edit Employment"
                }
            </h1>

        </div>
    );
}