import { useContext } from "react";
import { ConnectionContext, UserContext } from "../pages/OrdersPage";

function OrderButton({ isPrepared, preparedButtonHandler, orderId }) {
  const user = useContext(UserContext);
  const { connection } = useContext(ConnectionContext);
  let buttonText;

  // switch the button if the waiter is logged in
  if (user.username == "waiter") {
    isPrepared = !isPrepared;
    if (isPrepared) {
      buttonText = "U PRIPREMI";
    } else {
      buttonText = "UKLONI";
    }
  } else {
    buttonText = "SPREMNO";
  }

  function serveButtonHandler() {
    connection.invoke("OrderServed", orderId).catch((error) => console.log("Failed to invoke OrderPrepared hub method: " + error));
  }

  return (
    <div className="text-center py-4">
      <button
        className={`
          ${isPrepared ? "bg-slate-400" : "bg-greenBtn hover:bg-opacity-90"} 
          text-white 
          py-1 
          px-6 
          rounded-md 
          shadow-md 
          shadow-gray-500`}
        disabled={isPrepared ? true : false}
        onClick={user.username == "waiter" ? serveButtonHandler : preparedButtonHandler}
      >
        {buttonText}
      </button>
    </div>
  );
}

export default OrderButton;
