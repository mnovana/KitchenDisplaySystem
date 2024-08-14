import LineChart from "./LineChart";

function MonthlyOrders() {
  const years = [];
  for (let i = 2022; i <= new Date().getFullYear(); i++) {
    // unshift instead of push
    // so that the last year inserted (current year) stays first and becomes a selected option by default
    years.unshift(i);
  }

  return (
    <div className="w-4/5 h-[500px] flex flex-col justify-center items-center mb-10 bg-white shadow-md rounded">
      <div className="text-2xl pt-5 pb-9">
        <span>Mesečni broj porudžbina</span>
      </div>
      <select>
        {years.map((year) => (
          <option value={year}>{year}</option>
        ))}
      </select>
      <div className="flex-1 w-5/6 h-60 mb-10">
        <LineChart />
      </div>
    </div>
  );
}

export default MonthlyOrders;
