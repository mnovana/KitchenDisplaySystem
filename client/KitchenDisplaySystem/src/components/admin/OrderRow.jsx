import waiterImg from "../../assets/waiter.png";
import tableImg from "../../assets/table.png";
import { useState } from "react";
import { FaStopwatch } from "react-icons/fa";

function OrderRow({ order }) {
  const [showItems, setShowItems] = useState(false);

  let color;
  let text;

  if (!order.end) {
    color = "bg-neutral-400";
    text = "U pripremi";
  } else if (!order.served) {
    color = "bg-yellow-300";
    text = "Spremno";
  } else if (order.served) {
    color = "bg-lime-400";
    text = "Servirano";
  }

  return (
    <>
      <div className="flex p-4 border-b-2 rounded-xl hover:bg-zinc-100" onClick={() => setShowItems((prev) => !prev)}>
        <span className="flex-1 flex justify-between">
          <span className="font-bold text-2xl">#{order.id}</span>
          <span className="font-light italic text-2xl">{getTimeString(order.start)}</span>
          <span>
            <img src={tableImg} className="w-6 inline" />
            {order.tableNumber}
          </span>
          <span className="basis-1/3">
            <img src={waiterImg} className="w-6 inline" />
            {order.waiterDisplayName}
          </span>
        </span>
        <span className="flex-1 text-right my-auto">
          <span className={`${color} rounded-xl px-3 font-medium`}>{text}</span>
        </span>
      </div>

      {showItems && (
        <div className="flex px-10 mb-7 w-full">
          <ul className="flex-1 list-none">
            {order.orderItems.map((orderItem) => (
              <li>
                {orderItem.quantity} x {orderItem.foodName}
              </li>
            ))}
          </ul>

          {order.note && (
            <span className="flex-1">
              <span className="font-bold">NAPOMENA:</span> {order.note}
            </span>
          )}

          {order.end && (
            <div className="font-light">
              {getTimeString(order.start)} - {getTimeString(order.end)} <FaStopwatch className="inline" /> {getPrepareTime(order.start, order.end)} min
            </div>
          )}
        </div>
      )}
    </>
  );
}

function getTimeString(dateString) {
  const date = new Date(dateString);
  const hours = date.getHours().toString().padStart(2, "0");
  const minutes = date.getMinutes().toString().padStart(2, "0");

  return `${hours}:${minutes}`;
}

function getPrepareTime(startString, endString) {
  const start = new Date(startString);
  const end = new Date(endString);

  const minutes = Math.round((end - start) / 60000);

  return minutes;
}

export default OrderRow;
