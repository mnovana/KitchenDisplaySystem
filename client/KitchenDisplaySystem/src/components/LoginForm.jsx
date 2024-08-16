import waiterImg from "../assets/waiter.svg";
import chefImg from "../assets/chef.svg";
import adminImg from "../assets/admin.png";
import { useState } from "react";
import { useNavigate } from "react-router-dom";

function LoginForm() {
  const [waiter, setWaiter] = useState(false);
  const [kitchen, setKitchen] = useState(false);
  const [admin, setAdmin] = useState(false);
  const [password, setPassword] = useState("");
  const [validationMessage, setValidationMessage] = useState("");

  const navigate = useNavigate();

  function submitForm(e) {
    e.preventDefault();

    if (!validateForm()) {
      return;
    }

    let username;
    if (waiter) {
      username = "waiter";
    } else if (kitchen) {
      username = "kitchen";
    } else if (admin) {
      username = "admin";
    }

    const user = { username: username, password: password };
    console.log(user);
    const serverUrl = import.meta.env.VITE_SERVER_URL;

    fetch(`${serverUrl}/login`, { method: "POST", headers: { "Content-Type": "application/json" }, body: JSON.stringify(user) })
      .then((response) => {
        if (response.status == 200) {
          response.json().then((data) => {
            sessionStorage.setItem("user", JSON.stringify(data));
            console.log("Successful login. Token = " + data.token + " User = " + data.username);
            if (data.username == "admin") {
              return navigate("/admin");
            } else {
              return navigate("/");
            }
          });
        } else {
          setValidationMessage("Neuspešna prijava.");
        }
      })
      .catch((error) => alert(error));
  }

  function validateForm() {
    setValidationMessage("");

    if (!waiter && !kitchen && !admin) {
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
    <div
      className="h-screen w-screen flex flex-col justify-center items-center gap-8"
      onClick={() => {
        setWaiter(false);
        setKitchen(false);
        setAdmin(false);
      }}
    >
      <div className="flex items-center h-auto  w-1/2 gap-5">
        <div
          className={`${waiter ? "bg-gray-200" : "bg-neutral-600"} rounded h-full w-full hover:bg-gray-200 transition-colors`}
          onClick={(event) => {
            event.stopPropagation();
            setWaiter(true);
            setKitchen(false);
            setAdmin(false);
            setValidationMessage("");
          }}
        >
          <img src={waiterImg} />
        </div>
        <div
          className={`${kitchen ? "bg-gray-200" : "bg-neutral-600"} rounded h-full w-full hover:bg-gray-200 transition-colors`}
          onClick={(event) => {
            event.stopPropagation();
            setKitchen(true);
            setWaiter(false);
            setAdmin(false);
            setValidationMessage("");
          }}
        >
          <img src={chefImg} />
        </div>
      </div>
      <div
        className={`${admin ? "bg-gray-200" : "bg-neutral-600"} rounded w-1/2 h-16 hover:bg-gray-200 transition-colors overflow-hidden`}
        onClick={(event) => {
          event.stopPropagation();
          setAdmin(true);
          setKitchen(false);
          setWaiter(false);
          setValidationMessage("");
        }}
      >
        <img src={adminImg} className="h-16 ml-[15%] mt-2" />
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
          onClick={(e) => e.stopPropagation()}
        />
        <br />
        <br />
        {validationMessage && <span className="text-red-600">{validationMessage}</span>}
        <br />
        <input type="submit" value="Prijava" className="bg-neutral-600 text-white p-2 m-3 rounded hover:bg-black transition-colors" onClick={(event) => event.stopPropagation()} />
      </form>
    </div>
  );
}

export default LoginForm;
