interface Props {

    selectedMenu: string;

    onMenuChanged: (menu: string) => void;

}

export default function ProfileMenu({

    selectedMenu,

    onMenuChanged

}: Props) {

    return (

        <div className="card">

            <button
                className={
                    selectedMenu === "BasicInfo"
                        ? "profile-menu-item active"
                        : "profile-menu-item"
                }
                onClick={() => onMenuChanged("BasicInfo")}
            >
                Basic Information
            </button>

            <button className={
                selectedMenu === "Skills"
                    ? "profile-menu-item active"
                    : "profile-menu-item"
            }
                onClick={() => onMenuChanged("Skills")}
            >
                Skills
            </button>

            <button className="profile-menu-item">
                Education
            </button>

            <button className={
                selectedMenu === "Resume"
                    ? "profile-menu-item active"
                    : "profile-menu-item"
            }
                onClick={() => onMenuChanged("Resume")}>
                Resume
            </button>

            <button className="profile-menu-item">
                Security
            </button>

            <button className={
                selectedMenu === "ContactDetails"
                    ? "profile-menu-item active"
                    : "profile-menu-item"
            }
                onClick={() => onMenuChanged("ContactDetails")}>
                Contact Details
            </button>

            

        </div>

    );

}