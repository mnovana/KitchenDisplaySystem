import OrderRow from "./OrderRow";

function DailyOrders() {
  return (
    <div className="bg-white w-4/5 shadow-md rounded">
      <div className="pt-5 pb-9 text-center">
        <span className="text-4xl">Porudžbine</span>
        <input type="date" value={new Date().toISOString().slice(0, 10)} className="float-end mr-5 shadow-inner shadow-neutral-400 rounded px-2" />
      </div>

      <OrderRow order={{ id: 72, start: "16:22", tableNumber: 13, waiterDisplayName: "Marko M." }} />
      <OrderRow order={{ id: 71, start: "16:17", tableNumber: 10, waiterDisplayName: "Petar." }} />
      <OrderRow order={{ id: 70, start: "16:15", tableNumber: 12, waiterDisplayName: "Marko M.", served: false, end: "16:25" }} />
      <OrderRow order={{ id: 69, start: "16:09", tableNumber: 11, waiterDisplayName: "Marko M.", served: true, end: "16:31" }} />
      <OrderRow order={{ id: 68, start: "15:56", tableNumber: 13, waiterDisplayName: "Jovana", served: true, end: "16:22" }} />
    </div>
  );
}

export default DailyOrders;
