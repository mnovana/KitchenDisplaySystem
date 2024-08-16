import { useRef } from "react";
import { Line } from "react-chartjs-2";
import "chart.js/auto";
import { scales, Ticks } from "chart.js/auto";

function LineChart() {
  const ref = useRef();

  const data = {
    labels: ["Jan", "Feb", "Mar", "Apr", "Jun", "Jul"],
    datasets: [
      {
        data: [509, 590, 584, 621, 655, 702, 694],
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
