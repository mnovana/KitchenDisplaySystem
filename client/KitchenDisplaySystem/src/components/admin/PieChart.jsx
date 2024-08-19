import { Pie } from "react-chartjs-2";
import { Chart as ChartJS, ArcElement, Tooltip, Legend } from "chart.js";

function PieChart({ ordersByWaiter }) {
  ChartJS.register(ArcElement, Tooltip, Legend);
  const data = {
    labels: ordersByWaiter.map((o) => o.waiterDisplayName),
    datasets: [
      {
        data: ordersByWaiter.map((o) => o.numberOfOrders),
        backgroundColor: [
          "rgb(255, 44, 44)",
          "rgb(44, 127, 255)",
          "rgb(255, 239, 44)",
          "rgb(54, 218, 79)",
          "rgb(190, 37, 186)",
          "rgb(0, 210, 8)",
          "rgb(218, 137, 14)",
          "rgb(14, 218, 166)",
          "rgb(255, 83, 154)",
        ],
        borderWidth: 1,
      },
    ],
  };
  const options = {
    plugins: {
      legend: {
        position: "right",
      },
    },
  };

  return <Pie data={data} options={options} />;
}

export default PieChart;
