import React, { useContext, useState } from "react";
import Menu from "./Menu";
import { ConnectionContext, SetShowModalContext } from "../pages/OrdersPage";
import { format } from "date-fns";

export const AddedItemsContext = React.createContext();

function AddOrderForm({ orderData }) {
  const [addedItems, setAddedItems] = useState([]);
  const [tableId, setTableId] = useState(orderData.tables[0].id);
  const [waiterId, setWaiterId] = useState(orderData.waiters[0].id);
  const [note, setNote] = useState("");

  const setShowModal = useContext(SetShowModalContext);
  const { connection } = useContext(ConnectionContext);

  function removeItem(foodId) {
    setAddedItems((prevItems) => prevItems.filter((item) => item.foodId != foodId));
  }

  function submitAddOrderForm(e) {
    e.preventDefault();

    if (!validateAddOrderForm()) {
      return;
    }

    const now = format(new Date(), "yyyy-MM-dd'T'HH:mm:ss.SS");

    const newOrder = {
      tableId: Number(tableId),
      waiterId: Number(waiterId),
      note: note,
      orderItems: addedItems,
      start: now,
    };

    console.log(newOrder);
    connection.invoke("OrderCreated", newOrder).catch((error) => console.log("Failed to invoke OrderCreated hub method: " + error));

    setShowModal(false);
  }

  function validateAddOrderForm() {
    if (addedItems.length < 1) {
      alert("Lista jela je prazna.");
      return false;
    }
    if (!tableId) {
      alert("Izaberite sto.");
      return false;
    }
    if (!waiterId) {
      alert("Izaberite konobara.");
      return false;
    }

    return true;
  }

  return (
    <form className="flex items-center flex-col gap-3 justify-around flex-1">
      {/* first row */}
      <div className="flex justify-evenly">
        {/* table */}
        <div className="rounded bg-stone-100 shadow-md shadow-stone-400 py-2 px-8">
          <label htmlFor="table" className="pr-4">
            Oznaka stola:
          </label>
          <select id="table" value={tableId} onChange={(e) => setTableId(e.target.value)}>
            {orderData.tables.map((table) => (
              <option key={table.id} value={table.id}>
                {table.number}
              </option>
            ))}
          </select>
        </div>

        {/* waiter */}
        <div className="rounded bg-stone-100 shadow-md shadow-stone-400 py-2 px-8">
          <label htmlFor="waiter" className="pr-4">
            Konobar:
          </label>
          <select id="waiter" value={waiterId} onChange={(e) => setWaiterId(e.target.value)}>
            {orderData.waiters.map((waiter) => (
              <option key={waiter.id} value={waiter.id}>
                {waiter.displayName}
              </option>
            ))}
          </select>
        </div>
      </div>

      {/* second row */}
      <div className="flex rounded justify-between bg-stone-100 shadow-md shadow-stone-400 w-5/6 p-2 h-3/5">
        {/* added items */}
        <div className="overflow-y-auto">
          Jela:
          {addedItems.map((item) => (
            <div key={item.foodId} className="m-2 p-2 border-b border-neutral-800 flex justify-between">
              <div>
                {item.quantity} {item.foodName}
              </div>
              <button className="ml-1 font-bold text-red-600 hover:text-red-900" onClick={() => removeItem(item.foodId)}>
                &#10005;
              </button>
            </div>
          ))}
        </div>

        {/* all items */}
        <AddedItemsContext.Provider value={{ addedItems, setAddedItems }}>
          <Menu orderData={orderData} />
        </AddedItemsContext.Provider>
      </div>

      {/* third row */}
      <div className="rounded bg-stone-100 shadow-md shadow-stone-400 p-2 w-5/6 flex">
        <label htmlFor="note">Napomena:</label>
        <input type="text" id="note" className="rounded shadow-inner shadow-stone-400 px-2 ml-3 flex-1" maxLength={500} value={note} onChange={(e) => setNote(e.target.value)} />
      </div>

      {/* buttons */}
      <div className="text-white w-5/6 text-center">
        <button
          className="rounded bg-neutral-700 py-1 w-1/4 mr-3 hover:bg-neutral-800"
          onClick={(e) => {
            e.preventDefault();
            setShowModal(false);
          }}
        >
          ODUSTANI
        </button>
        <button className="rounded bg-lime-500 py-1 w-1/4 ml-3 hover:bg-lime-600" onClick={submitAddOrderForm}>
          DODAJ
        </button>
      </div>
    </form>
  );
}

export default AddOrderForm;
