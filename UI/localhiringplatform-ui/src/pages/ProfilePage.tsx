import { useState } from "react";

import ProfileMenu from "../components/Profile/ProfileMenu";
import BasicInfo from "../components/Profile/BasicInfo";
import Skills from "../components/Profile/Skills";
import Resume from "../components/Profile/Resume";
import ContactDetails from "../components/Profile/ContactDetails";
import Education from "../components/Profile/Education";

export default function ProfilePage() {

    const [selectedMenu, setSelectedMenu] =
        useState("BasicInfo");

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
                        selectedMenu === "ContactDetails" &&
                        <ContactDetails />
                    }
                    {
                        selectedMenu === "Education" &&
                        <Education />
                    }
                </div>

            </div>

        </div>

    );

}