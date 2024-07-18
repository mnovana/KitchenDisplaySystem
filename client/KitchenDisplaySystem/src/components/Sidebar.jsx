import logoutImg from "../assets/logout.png";
import addImg from "../assets/add.png";
import { SetShowModalContext, UserContext } from "../App";
import { useContext } from "react";

function Sidebar({ setUser }) {
  const setShowModal = useContext(SetShowModalContext);
  const user = useContext(UserContext);

  return (
    <div className={`h-screen w-20 flex flex-col p-1 ${user.username == "waiter" ? "justify-between" : "justify-end"}`}>
      {/* ADD ORDER button only for the waiter */}
      {user.username == "waiter" && <img src={addImg} className="invert opacity-50 hover:opacity-100" onClick={() => setShowModal(true)} />}
      <img
        src={logoutImg}
        className="invert opacity-50 hover:opacity-100"
        onClick={() => {
          sessionStorage.removeItem("user");
          setUser(null);
        }}
      />
    </div>
  );
}

export default Sidebar;
