import { NavLink, Outlet, useNavigate } from "react-router-dom";

function AdminHeader() {
  const navigate = useNavigate();

  return (
    <>
      <div className="w-full h-fit bg-neutral-800 text-white">
        <div className="text-right p-3">
          <NavLink to="/admin" className={({ isActive }) => (isActive ? "mx-20 underline" : "mx-20 hover:underline")}>
            Panel
          </NavLink>
          <NavLink to="/settings" className={({ isActive }) => (isActive ? "mx-20 underline" : "mx-20 hover:underline")}>
            Podešavanja
          </NavLink>
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
