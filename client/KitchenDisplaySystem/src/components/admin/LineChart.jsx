import { useRef } from "react";
import { Line } from "react-chartjs-2";
import "chart.js/auto";

function LineChart({ monthlyOrders }) {
  const ref = useRef();

  const data = {
    labels: monthlyOrders.map((m) => m.month),
    datasets: [
      {
        data: monthlyOrders.map((m) => m.numberOfOrders),
        fill: false,
        borderColor: "rgb(75, 192, 192)",
        tension: 0.1,
      },
    ],
  };

  const options = {
    plugins: {
      legend: {
        display: false,
      },
    },
    maintainAspectRatio: false,
  };

  return <Line ref={ref} options={options} data={data} />;
}

export default LineChart;
