import PieChart from "../components/admin/PieChart";
import Card from "../components/admin/Card";
import AverageTimeCard from "../components/admin/AverageTimeCard";
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
          <Card headerColor="bg-lime-600" title="Broj porudžbina danas" number="129" />
          <Card headerColor="bg-yellow-400" title="Broj porudžbina do 12h" number="22" />
          <Card headerColor="bg-sky-700" title="Broj porudžbina 12h-17h" number="47" />
          <Card headerColor="bg-red-600" title="Broj porudžbina posle 17h" number="60" />
        </div>

        {/* orders */}
        <DailyOrders />

        {/* time and pie chart */}
        <div className="flex flex-wrap gap-5 w-4/5 justify-center items-center">
          {/* time */}
          <AverageTimeCard time={19} />

          {/* pie chart */}
          <div className="flex-1 flex flex-col justify-center items-center bg-white shadow-md rounded">
            <div className="text-2xl pt-5 pb-9 text-center">Broj porudžbina prema konobarima</div>
            <div>
              <PieChart />
            </div>
          </div>
        </div>

        {/* line chart */}
        <MonthlyOrders />
      </div>
    </UserContext.Provider>
  );
}

export default PanelPage;
