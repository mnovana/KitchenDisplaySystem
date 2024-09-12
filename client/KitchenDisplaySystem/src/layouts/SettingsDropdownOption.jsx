import { NavLink } from "react-router-dom";

function SettingsDropdownOption({ path, text }) {
  return (
    <li className="py-4 px-10 hover:bg-neutral-900 transition-colors">
      <NavLink to={path} className={({ isActive }) => isActive && "font-bold"}>
        {text}
      </NavLink>
    </li>
  );
}

export default SettingsDropdownOption;
