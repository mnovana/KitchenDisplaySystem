import waiterImg from "../assets/waiter.svg";
import chefImg from "../assets/chef.svg";
import { useState } from "react";

function LoginForm({ setUser }) {
  const [waiter, setWaiter] = useState(false);
  const [chef, setChef] = useState(false);
  const [password, setPassword] = useState("");
  const [validationMessage, setValidationMessage] = useState("");

  function submitForm(e) {
    e.preventDefault();

    if (!validateForm()) {
      return;
    }

    const user = { username: waiter ? "waiter" : "kitchen", password: password };
    console.log(user);

    fetch("https://localhost:7141/login", { method: "POST", headers: { "Content-Type": "application/json" }, body: JSON.stringify(user) })
      .then((response) => {
        if (response.status == 200) {
          response.json().then((data) => {
            sessionStorage.setItem("user", JSON.stringify(data));
            setUser(data);
            console.log("Successful login. Token = " + data.token + " User = " + data.username);
          });
        } else {
          setValidationMessage("Neuspešna prijava.");
        }
      })
      .catch((error) => alert(error));
  }

  function validateForm() {
    setValidationMessage("");

    if (!waiter && !chef) {
      setValidationMessage("Izaberite korisnika.");
      return false;
    } else if (!password) {
      setValidationMessage("Unesite lozinku.");
      return false;
    } else if (password.length < 8) {
      setValidationMessage("Unesite minimum 8 karaktera.");
      return false;
    }

    return true;
  }

  return (
    <div className="h-screen w-screen flex flex-col justify-center items-center gap-8">
      <div className="flex flex-row items-center h-auto  w-1/2 gap-5">
        <div
          className={`${waiter ? "bg-gray-200" : "bg-neutral-600"} rounded h-full w-full hover:bg-gray-200 transition-colors`}
          onClick={() => {
            setWaiter(true);
            setChef(false);
            setValidationMessage("");
          }}
        >
          <img src={waiterImg} />
        </div>
        <div
          className={`${chef ? "bg-gray-200" : "bg-neutral-600"} rounded h-full w-full hover:bg-gray-200 transition-colors`}
          onClick={() => {
            setChef(true);
            setWaiter(false);
            setValidationMessage("");
          }}
        >
          <img src={chefImg} />
        </div>
      </div>
      <form onSubmit={submitForm} className="text-center">
        <input
          type="password"
          placeholder="Password"
          className="p-1 rounded focus:outline-lime-500"
          onChange={(e) => {
            setPassword(e.target.value);
            setValidationMessage("");
          }}
        />
        <br />
        <br />
        {validationMessage && <span className="text-red-600">{validationMessage}</span>}
        <br />
        <input type="submit" value="Prijava" className="bg-neutral-600 text-white p-2 m-3 rounded hover:bg-black transition-colors" />
      </form>
    </div>
  );
}

export default LoginForm;
