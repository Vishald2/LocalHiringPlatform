import { useState } from "react";

import ProfileMenu from "../components/Profile/ProfileMenu";
import BasicInfo from "../components/Profile/BasicInfo";
import Skills from "../components/Profile/Skills";
import Resume from "../components/Profile/Resume";
import ContactDetails from "../components/Profile/ContactDetails";
import Education from "../components/Profile/Education";
import Employment from "../components/Profile/Employment";
import EmploymentAddPage from "../components/Profile/EmploymentAddPage";
import EmploymentEditPage from "../components/Profile/EmploymentEditPage";

export default function ProfilePage() {

    const [selectedMenu, setSelectedMenu] =
        useState("BasicInfo");

    const [candidateExperienceEntityId,
        setCandidateExperienceEntityId] =
        useState<string | undefined>();

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
                        <Employment />
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
                        selectedMenu === "Employment" &&
                        <Employment
                            onAdd={() => {

                                setCandidateExperienceEntityId(undefined);

                                setSelectedMenu("EmploymentAdd");
                            }}

                            onEdit={(entityId) => {

                                setCandidateExperienceEntityId(entityId);

                                setSelectedMenu("EmploymentEdit");
                            }}
                        />
                    }

                    {
                        selectedMenu === "EmploymentAdd" &&
                        <EmploymentAddPage />
                    }

                    {
                        selectedMenu === "EmploymentEdit" &&
                        <EmploymentEditPage
                            entityId={candidateExperienceEntityId!}
                        />
                    }
                </div>

            </div>

        </div>

    );

}