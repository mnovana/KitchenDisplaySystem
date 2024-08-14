import waiterImg from "../../assets/waiter.png";
import tableImg from "../../assets/table.png";

function OrderRow({ order }) {
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
    <div className="flex m-5 border-b-2">
      <span className="flex-1 flex justify-between">
        <span className="font-bold text-2xl">#{order.id}</span>
        <span className="font-light italic text-2xl">{order.start}</span>
        <span>
          <img src={tableImg} className="w-6 inline" />
          {order.tableNumber}
        </span>
        <span className="basis-1/3">
          <img src={waiterImg} className="w-6 inline" />
          {order.waiterDisplayName}
        </span>
      </span>
      <span className="flex-1 text-right">
        <span className={`${color} rounded-xl px-3 font-medium`}>{text}</span>
      </span>
    </div>
  );
}

export default OrderRow;
