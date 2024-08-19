import { useContext, useState, useEffect } from "react";
import PieChart from "./PieChart";
import { UserContext } from "../../pages/PanelPage";

function OrdersByWaiter() {
  const user = useContext(UserContext);
  const [ordersByWaiter, setOrdersByWaiter] = useState([]);

  useEffect(() => {
    const serverUrl = import.meta.env.VITE_SERVER_URL;

    const headers = {};
    headers.Authorization = "Bearer " + user.token;

    fetch(`${serverUrl}/orders/waiters`, { headers: headers })
      .then((response) => {
        if (response.status == 200) {
          response.json().then(setOrdersByWaiter);
        } else {
          alert("Orders by waiter fetch failed");
        }
      })
      .catch((error) => alert(error));
  }, []);

  return (
    <>
      <div className="text-2xl pt-5 pb-9 text-center">Broj porudžbina prema konobarima</div>
      <div>{ordersByWaiter.length > 0 && <PieChart ordersByWaiter={ordersByWaiter} />}</div>
    </>
  );
}

export default OrdersByWaiter;
