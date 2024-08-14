import { Pie } from "react-chartjs-2";
import { Chart as ChartJS, ArcElement, Tooltip, Legend } from "chart.js";

function PieChart() {
  ChartJS.register(ArcElement, Tooltip, Legend);
  const data = {
    labels: ["Jovana", "Marko M.", "Petar", "Marko J."],
    datasets: [
      {
        data: [302, 290, 201, 183],
        backgroundColor: ["rgb(255, 44, 44)", "rgb(44, 127, 255)", "rgb(255, 239, 44)", "rgb(54, 218, 79)"],
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
