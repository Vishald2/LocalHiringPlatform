interface EducationEditorProps {

    entityId: string | null;

    onClose: () => void;
}

export default function EducationEditor({

    entityId,

    onClose

}: EducationEditorProps) {

    const isEditMode =
        entityId !== null;

    return (

        <div className="card">

            <h2>

                {
                    isEditMode
                        ? "Edit Education"
                        : "Add Education"
                }

            </h2>

            <hr />

            <p>

                Education Editor Component

            </p>

            {
                isEditMode &&

                <p>

                    Entity Id:
                    {" "}
                    {entityId}

                </p>
            }

            <button
                className="secondary-button"
                onClick={onClose}
            >
                Back
            </button>

        </div>
    );
}