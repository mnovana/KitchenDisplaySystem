import { useState, useEffect, useContext } from "react";
import LineChart from "./LineChart";
import { UserContext } from "../../pages/PanelPage";

function MonthlyOrders() {
  const user = useContext(UserContext);
  const [monthlyOrders, setMonthlyOrders] = useState([]);
  const [year, setYear] = useState(new Date().getFullYear());

  const years = [];
  for (let i = 2023; i <= new Date().getFullYear(); i++) {
    // unshift instead of push
    // so that the last year inserted (current year) stays first and becomes a selected option by default
    years.unshift(i);
  }

  function fetchMonthlyOrders(year) {
    if (year < 2023 || year > new Date().getFullYear()) {
      console.alert("Invalid year");
      return;
    }
    console.log("Fetching with year " + year);
    const serverUrl = import.meta.env.VITE_SERVER_URL;

    const headers = {};
    headers.Authorization = "Bearer " + user.token;

    fetch(`${serverUrl}/orders/monthly?year=${year}`, { headers: headers })
      .then((response) => {
        if (response.status == 200) {
          response.json().then(setMonthlyOrders);
        } else {
          alert("Monthly orders fetch failed");
        }
      })
      .catch((error) => console.log("Monthly orders: " + error));
  }

  useEffect(() => {
    fetchMonthlyOrders(year);
  }, [year]);

  return (
    <div className="w-4/5 h-[500px] flex flex-col justify-center items-center mb-10 bg-white shadow-md rounded">
      <div className="text-2xl pt-5 pb-9">
        <span>Mesečni broj porudžbina</span>
      </div>
      <select onChange={(e) => setYear(e.target.value)}>
        {years.map((year) => (
          <option key={year} value={year}>
            {year}
          </option>
        ))}
      </select>
      <div className="flex-1 w-5/6 h-60 mb-10">
        {monthlyOrders.length > 0 ? <LineChart monthlyOrders={monthlyOrders} /> : <div className="text-center my-20">Ne postoje podaci za unetu godinu.</div>}
      </div>
    </div>
  );
}

export default MonthlyOrders;
