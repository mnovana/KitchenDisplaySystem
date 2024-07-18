import { useState, useEffect, useContext } from "react";
import React from "react";
import Order from "./Order";
import { HubConnectionBuilder, HttpTransportType } from "@microsoft/signalr";
import bell_one from "../assets/bell_one.mp3";
import { ConnectionContext, UserContext } from "../App";

function Orders() {
  const [orders, setOrders] = useState([]);
  const { connection, setConnection } = useContext(ConnectionContext);
  const user = useContext(UserContext);

  function AddOrder(newOrder) {
    if (user.username == "kitchen") {
      const audio = new Audio(bell_one);
      audio.pause();
      audio.currentTime = 0;
      audio.play().catch((error) => console.log("Failed to play audio: " + error));
    }
    setOrders((prevOrders) => [...prevOrders, newOrder]);
  }

  function ServeOrder(id) {
    setOrders((prevOrders) => prevOrders.filter((order) => order.id != id));
  }

  // fetch orders
  useEffect(() => {
    const headers = {};
    headers.Authorization = "Bearer " + user.token;

    fetch("https://localhost:7141/orders/unserved", { headers: headers })
      .then((response) => {
        if (response.status == 200) {
          response.json().then(setOrders);
        } else {
          alert("Fetch failed");
        }
      })
      .catch((error) => alert(error));
  }, []);

  // signalR
  useEffect(() => {
    const connect = new HubConnectionBuilder()
      .withUrl("https://localhost:7141/hubs/order", {
        accessTokenFactory: () => user.token,
        skipNegotiation: true,
        transport: HttpTransportType.WebSockets,
      })
      .build();

    connect
      .start()
      .then(() => {
        console.log("Connected to the OrderHub");
        setConnection(connect);
      })
      .catch((err) => console.log("Failed to connect to the OrderHub: " + err));

    connect.on("AddOrder", (newOrder) => AddOrder(newOrder));
    connect.on("ServeOrder", (id) => ServeOrder(id));
    connect.on("ReceiveError", (error) => alert(error));

    return () => {
      connect.off("AddOrder");
      connect.off("ServeOrder");
      connect.off("ReceiveError");
      connect.stop();
    };
  }, []);

  if (!connection) {
    return (
      <div className="flex justify-center items-center h-screen w-screen text-white">
        <span>Učitavanje...</span>
      </div>
    );
  } else {
    return (
      <div className="overflow-x-auto flex flex-col flex-wrap gap-5 m-5">
        {orders.map((order) => (
          <Order key={order.id} order={order} />
        ))}
      </div>
    );
  }
}

export default Orders;
