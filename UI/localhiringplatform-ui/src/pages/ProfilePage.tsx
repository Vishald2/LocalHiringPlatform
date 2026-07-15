import { useState } from "react";

import ProfileMenu from "../components/Profile/ProfileMenu";
import BasicInfo from "../components/Profile/BasicInfo";
import Skills from "../components/Profile/Skills";
import Resume from "../components/Profile/Resume";
import ContactDetails from "../components/Profile/ContactDetails";
import Education from "../components/Profile/Education";
import Employment from "../components/Profile/Employment";
import EmploymentEditor from "../components/Profile/EmploymentEditor";

export default function ProfilePage() {

    const [selectedMenu, setSelectedMenu] =
        useState("BasicInfo");

    const [candidateExperienceEntityId,
        setCandidateExperienceEntityId] =
        useState<string | undefined>();

    function openEmploymentEditor(
        entityId?: string,
        mode?: string) {

        if (mode === "EmploymentAdd") {
            setCandidateExperienceEntityId(undefined);
            setSelectedMenu("EmploymentAdd");
        }
        else if (mode === "EmploymentEdit") {
            setCandidateExperienceEntityId(entityId);
            setSelectedMenu("EmploymentEdit");
        }
    }

    function showEmploymentList() {

        setCandidateExperienceEntityId(undefined);

        setSelectedMenu("Employment");
    }

    return (

        <div className="page-container">

            <div className="profile-layout">

                <ProfileMenu
                    selectedMenu={selectedMenu}
                    onMenuChanged={setSelectedMenu}
                />

                <div className="card">
                    {
                        selectedMenu === "BasicInfo" &&
                        <BasicInfo />
                    }
                    {
                        selectedMenu === "Skills" &&
                        <Skills />
                    }
                    {
                        selectedMenu === "Resume" &&
                        <Resume />
                    }
                    {
                        selectedMenu === "Employment" &&
                        <Employment openEmploymentEditor={openEmploymentEditor} />
                    }
                    {
                        selectedMenu === "ContactDetails" &&
                        <ContactDetails />
                    }
                    {
                        selectedMenu === "Education" &&
                        <Education />
                    }
                    {
                        selectedMenu === "EmploymentAdd" &&
                        <EmploymentEditor
                            mode={selectedMenu}
                            candidateExperienceEntityId=""
                            onCancel={showEmploymentList}
                            onSaved={showEmploymentList}
                        />
                    }

                    {
                        selectedMenu === "EmploymentEdit" &&
                        <EmploymentEditor
                            mode={selectedMenu}
                            candidateExperienceEntityId={candidateExperienceEntityId || ""}
                            onCancel={showEmploymentList}
                            onSaved={showEmploymentList}
                        />
                    }
                </div>

            </div>

        </div>

    );

}