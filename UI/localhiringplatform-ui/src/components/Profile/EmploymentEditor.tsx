export interface EmploymentEditorProps {

    mode: "add" | "edit";

}

export default function EmploymentEditor(
    props: EmploymentEditorProps) {

    return (

        <div>

            <h1>
                {
                    props.mode === "add"
                        ? "Add Employment"
                        : "Edit Employment"
                }
            </h1>

        </div>
    );
}