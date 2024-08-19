import { useContext, useEffect, useState } from "react";
import OrderRow from "./OrderRow";
import { UserContext } from "../../pages/PanelPage";
import { FaSearch } from "react-icons/fa";

function DailyOrders() {
  const user = useContext(UserContext);
  const [orders, setOrders] = useState([]);

  const [date, setDate] = useState(() => {
    const today = new Date();
    return new Date(today.getTime() - today.getTimezoneOffset() * 60000).toISOString().slice(0, 10);
  });

  function fetchOrdersByDate(dateString) {
    const date = new Date(dateString);
    if (date.getFullYear < 2023 || date.getDate() > new Date().getDate()) {
      console.alert("Invalid date");
      return;
    }

    const serverUrl = import.meta.env.VITE_SERVER_URL;

    const headers = {};
    headers.Authorization = "Bearer " + user.token;

    fetch(`${serverUrl}/orders?date=${dateString}`, { headers: headers })
      .then((response) => {
        if (response.status == 200) {
          response.json().then(setOrders);
        } else {
          alert("Daily orders fetch failed");
        }
      })
      .catch((error) => console.log("Daily orders: " + error));
  }

  useEffect(() => {
    fetchOrdersByDate(date);
  }, []);

  return (
    <div className="bg-white w-4/5 shadow-md rounded">
      <div className="pt-5 pb-9 text-center">
        <span className="text-4xl">Porudžbine</span>
        <form
          className="float-end mr-5"
          onSubmit={(e) => {
            e.preventDefault();
            fetchOrdersByDate(date);
          }}
        >
          <input
            type="date"
            value={date}
            className="mr-5 shadow-inner shadow-neutral-400 rounded px-2"
            onChange={(e) => setDate(new Date(e.target.value).toISOString().slice(0, 10))}
          />
          <button type="submit">
            <FaSearch className="hover:text-neutral-600 h-6 w-6" />
          </button>
        </form>
      </div>

      {orders.length > 0 ? orders.map((order) => <OrderRow key={order.id} order={order} />) : <div className="text-center m-10">Ne postoje podaci za uneti datum.</div>}
    </div>
  );
}

export default DailyOrders;
