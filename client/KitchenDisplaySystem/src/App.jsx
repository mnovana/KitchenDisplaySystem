import Orders from "./components/Orders";
import Sidebar from "./components/Sidebar";
import LoginForm from "./components/LoginForm";
import React, { useState } from "react";
import AddOrderModal from "./components/AddOrderModal";

export const UserContext = React.createContext();
export const SetShowModalContext = React.createContext();
export const ConnectionContext = React.createContext();

function App() {
  const [user, setUser] = useState(JSON.parse(sessionStorage.getItem("user")) || null);
  const [showModal, setShowModal] = useState(false);
  // connection is set inside <Orders/> and used inside <Orders/> and <AddOrderModal/>.<AddOrderForm/>
  const [connection, setConnection] = useState(null);

  return (
    <div className="flex h-screen">
      {sessionStorage.getItem("user") ? (
        <UserContext.Provider value={user}>
          <SetShowModalContext.Provider value={setShowModal}>
            <ConnectionContext.Provider value={{ connection, setConnection }}>
              <Sidebar setUser={setUser} />
              <Orders />
              {showModal && <AddOrderModal />}
            </ConnectionContext.Provider>
          </SetShowModalContext.Provider>
        </UserContext.Provider>
      ) : (
        <LoginForm setUser={setUser} />
      )}
    </div>
  );
}

export default App;
