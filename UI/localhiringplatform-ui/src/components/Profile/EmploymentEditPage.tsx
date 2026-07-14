import EmploymentEditor from "./EmploymentEditor";

export default function EmploymentEditPage({ entityId }: { entityId: string }) {

    return (
        <EmploymentEditor
            mode="edit"
            entityId={entityId}
        />
    );
}