import OrderButton from "./OrderButton";
import OrderHeader from "./OrderHeader";
import OrderItem from "./OrderItem";
import Note from "./Note";
import { useContext, useEffect, useState } from "react";
import { ConnectionContext, UserContext } from "../App";
import bell_four from "../assets/bell_four.mp3";

function Order({ order }) {
  const [end, setEnd] = useState(order.end);
  const { connection } = useContext(ConnectionContext);
  const user = useContext(UserContext);

  function PreparedButtonHandler() {
    const now = new Date();
    connection.invoke("OrderPrepared", order.id, now).catch((error) => console.log("Failed to invoke OrderPrepared hub method: " + error));
  }

  function ReadyOrder(id, updatedEnd) {
    if (order.id == id) {
      if (user.username == "waiter") {
        const audio = new Audio(bell_four);
        audio.pause();
        audio.currentTime = 0;
        audio.play().catch((error) => console.log("Failed to play audio: " + error));
      }
      setEnd(updatedEnd);
    }
  }

  // signalR
  useEffect(() => {
    connection.on("ReadyOrder", (id, updatedEnd) => ReadyOrder(id, updatedEnd));
  });

  return (
    <div className="bg-gray-50 w-72 overflow-y-auto rounded">
      {/* without 'key={end}' OrderHeader wouldn't be re-renderd with the Order re-render */}
      {/* new Order would use the previous OrderHeader */}
      <OrderHeader key={end} id={order.id} waiterDisplayName={order.waiterDisplayName} tableNumber={order.tableNumber} start={order.start} end={end} />

      {order.orderItems.map((orderItem) => (
        <OrderItem key={orderItem.foodName} foodName={orderItem.foodName} quantity={orderItem.quantity} />
      ))}

      {order.note && <Note noteText={order.note} />}

      <OrderButton isPrepared={end} preparedButtonHandler={PreparedButtonHandler} orderId={order.id} />
    </div>
  );
}

export default Order;
