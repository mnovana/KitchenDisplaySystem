import Cards from "../components/admin/Cards";
import AverageTimeCard from "../components/admin/AverageTimeCard";
import OrdersByWaiter from "../components/admin/OrdersByWaiter";
import MonthlyOrders from "../components/admin/MonthlyOrders";
import DailyOrders from "../components/admin/DailyOrders";
import React from "react";

export const UserContext = React.createContext();

function PanelPage() {
  const user = JSON.parse(sessionStorage.getItem("user")) || null;

  return (
    <UserContext.Provider value={user}>
      <div className="flex flex-col items-center gap-10 bg-neutral-300">
        {/* cards */}
        <div className="text-center flex flex-wrap justify-center gap-10 my-3">
          <Cards />
        </div>

        {/* orders */}
        <DailyOrders />

        {/* time and pie chart */}
        <div className="flex flex-wrap gap-5 w-4/5 justify-center items-center">
          {/* time */}
          <div className="w-52 min-h-48 self-stretch bg-blue-500 shadow-md rounded text-white flex flex-col justify-around items-center">
            <AverageTimeCard />
          </div>

          {/* pie chart */}
          <div className="flex-1 flex flex-col justify-center items-center bg-white shadow-md rounded">
            <OrdersByWaiter />
          </div>
        </div>

        {/* line chart */}
        <MonthlyOrders />
      </div>
    </UserContext.Provider>
  );
}

export default PanelPage;
