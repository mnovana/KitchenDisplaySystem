import React, { useState } from "react";
import Orders from "../components/Orders";
import Sidebar from "../components/Sidebar";
import AddOrderModal from "../components/AddOrderModal";

export const UserContext = React.createContext();
export const SetShowModalContext = React.createContext();
export const ConnectionContext = React.createContext();

function OrdersPage() {
  const user = JSON.parse(sessionStorage.getItem("user")) || null;
  const [showModal, setShowModal] = useState(false);
  // connection is set inside <Orders/> and used inside <Orders/> and <AddOrderModal/>.<AddOrderForm/>
  const [connection, setConnection] = useState(null);

  return (
    <UserContext.Provider value={user}>
      <SetShowModalContext.Provider value={setShowModal}>
        <ConnectionContext.Provider value={{ connection, setConnection }}>
          <div className="flex h-screen">
            <Sidebar />
            <Orders />
            {showModal && <AddOrderModal />}
          </div>
        </ConnectionContext.Provider>
      </SetShowModalContext.Provider>
    </UserContext.Provider>
  );
}

export default OrdersPage;
