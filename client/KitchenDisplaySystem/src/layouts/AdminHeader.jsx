import { NavLink, Outlet, useLocation, useNavigate } from "react-router-dom";
import SettingsDropdownMenu from "./SettingsDropdownMenu";

function AdminHeader() {
  const navigate = useNavigate();
  const location = useLocation();
  const paths = ["/settings/orders", "/settings/food", "/settings/foodtypes", "/settings/waiters", "/settings/tables"];
  const settingsIsActive = paths.includes(location.pathname);

  return (
    <>
      <div className="w-full h-fit bg-neutral-800 text-white">
        <div className="text-right p-3">
          <NavLink to="/admin" className={({ isActive }) => (isActive ? "mx-20 underline" : "mx-20 hover:underline")}>
            Panel
          </NavLink>

          {/* dropdown menu */}
          <div className="relative group inline-block mx-20">
            <button className={settingsIsActive && "underline"}>Podešavanja</button>
            <SettingsDropdownMenu />
          </div>

          <button
            className="px-5 py-1 rounded-xl bg-neutral-900 hover:bg-neutral-950"
            onClick={() => {
              sessionStorage.removeItem("user");
              return navigate("/login");
            }}
          >
            Odjava
          </button>
        </div>
      </div>

      <Outlet />
    </>
  );
}

export default AdminHeader;
