import logoutImg from "../assets/logout.png";
import addImg from "../assets/add.png";
import { SetShowModalContext, UserContext } from "../pages/OrdersPage";
import { useContext } from "react";
import { useNavigate } from "react-router-dom";

function Sidebar() {
  const setShowModal = useContext(SetShowModalContext);
  const user = useContext(UserContext);
  const navigate = useNavigate();

  return (
    <div className={`h-screen w-20 flex flex-col p-1 ${user.username == "waiter" ? "justify-between" : "justify-end"}`}>
      {/* ADD ORDER button only for the waiter */}
      {user.username == "waiter" && <img src={addImg} className="invert opacity-50 hover:opacity-100" onClick={() => setShowModal(true)} />}
      <img
        src={logoutImg}
        className="invert opacity-50 hover:opacity-100"
        onClick={() => {
          sessionStorage.removeItem("user");
          return navigate("/login");
        }}
      />
    </div>
  );
}

export default Sidebar;
