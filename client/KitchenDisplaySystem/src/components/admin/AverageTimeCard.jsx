import whiteClockImg from "../../assets/white_clock.png";

function AverageTimeCard({ time }) {
  return (
    <div className="w-52 min-h-48 self-stretch bg-blue-500 shadow-md rounded text-white flex flex-col justify-around items-center">
      <span>Prosečno vreme pripreme</span>
      <img src={whiteClockImg} className="w-1/3" />
      <span className="text-6xl font-light">{time} min</span>
    </div>
  );
}

export default AverageTimeCard;
