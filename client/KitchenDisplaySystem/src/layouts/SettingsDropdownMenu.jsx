import SettingsDropdownOption from "./SettingsDropdownOption";

function SettingsDropdownMenu() {
  return (
    <ul className="absolute hidden group-hover:block bg-neutral-800 text-left pt-3">
      <SettingsDropdownOption path="/settings/orders" text="Porudžbine" />
      <SettingsDropdownOption path="/settings/food" text="Jela" />
      <SettingsDropdownOption path="/settings/foodtypes" text="Tipovi jela" />
      <SettingsDropdownOption path="/settings/waiters" text="Konobari" />
      <SettingsDropdownOption path="/settings/tables" text="Stolovi" />
    </ul>
  );
}

export default SettingsDropdownMenu;
