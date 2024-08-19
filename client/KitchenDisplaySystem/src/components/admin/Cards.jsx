import Card from "./Card";
import { useContext, useEffect, useState } from "react";
import { UserContext } from "../../pages/PanelPage";

function Cards() {
  const user = useContext(UserContext);
  const [numOfOrders, setNumOfOrders] = useState({ ordersBreakfast: null, ordersLunch: null, ordersDinner: null });

  useEffect(() => {
    const serverUrl = import.meta.env.VITE_SERVER_URL;

    const headers = {};
    headers.Authorization = "Bearer " + user.token;

    fetch(`${serverUrl}/orders/today`, { headers: headers })
      .then((response) => {
        if (response.status == 200) {
          response.json().then(setNumOfOrders);
        } else {
          alert("Number of orders fetch failed");
          setNumOfOrders({ ordersBreakfast: 0, ordersLunch: 0, ordersDinner: 0 });
        }
      })
      .catch((error) => {
        console.log("Number of orders: " + error);
        setNumOfOrders({ ordersBreakfast: 0, ordersLunch: 0, ordersDinner: 0 });
      });
  }, []);

  return (
    <>
      <Card
        headerColor="bg-lime-600"
        title="Broj porudžbina danas"
        number={numOfOrders.ordersBreakfast && numOfOrders.ordersBreakfast + numOfOrders.ordersLunch + numOfOrders.ordersDinner}
      />
      <Card headerColor="bg-yellow-400" title="Broj porudžbina do 12h" number={numOfOrders.ordersBreakfast} />
      <Card headerColor="bg-sky-700" title="Broj porudžbina 12h-17h" number={numOfOrders.ordersLunch} />
      <Card headerColor="bg-red-600" title="Broj porudžbina posle 17h" number={numOfOrders.ordersDinner} />
    </>
  );
}

export default Cards;
