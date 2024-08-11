import { useContext, useEffect, useState } from "react";
import { ClipLoader } from "react-spinners";
import AddOrderForm from "./AddOrderForm";
import { UserContext } from "../pages/OrdersPage";

function AddOrderModal() {
  const [orderData, setOrderData] = useState(null);
  const user = useContext(UserContext);

  useEffect(() => {
    if (!sessionStorage.getItem("orderData")) {
      const headers = {};
      headers.Authorization = "Bearer " + user.token;
      const serverUrl = import.meta.env.VITE_SERVER_URL;

      fetch(`${serverUrl}/orderdata`, { headers: headers })
        .then((response) => {
          if (response.status == 200) {
            response.json().then((data) => {
              sessionStorage.setItem("orderData", JSON.stringify(data));
              console.log(data);
              setOrderData(data);
            });
          } else {
            console.log("Error occured with code " + response.status);
          }
        })
        .catch((error) => alert("Failed to fetch order data: " + error));
    } else {
      setOrderData(JSON.parse(sessionStorage.getItem("orderData")));
    }
  }, []);

  return (
    <div className="fixed h-screen w-screen bg-black bg-opacity-80 flex justify-center items-center">
      <div className="h-5/6 w-3/4 bg-white rounded flex flex-col">
        {/* header */}
        <div className="bg-neutral-700 text-center rounded-t py-2">
          <span className="text-3xl text-white">Dodaj porudžbinu</span>
        </div>

        {/* form or spinner */}
        {orderData ? <AddOrderForm orderData={orderData} /> : <ClipLoader color="#332e2e" />}
      </div>
    </div>
  );
}

export default AddOrderModal;
