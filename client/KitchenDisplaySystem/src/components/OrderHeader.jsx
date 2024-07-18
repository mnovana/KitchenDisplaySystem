import Stopwatch from "./Stopwatch";
import { useState } from "react";

function OrderHeader({ id, waiterDisplayName, tableNumber, start, end }) {
  const [seconds, setSeconds] = useState(CalculateSeconds(start, end));
  let bgColor;

  // to trigger a re-render from a <Stopwatch /> to change the header color
  const updateSeconds = (newSeconds) => {
    setSeconds(newSeconds);
  };

  // header color:
  //    gray - order is already prepared
  //    red - order wait time is equal or more than 20 minutes
  //    green - order wait time is less than 20 minutes
  if (end) {
    bgColor = "bg-slate-400";
  } else if (seconds >= 1200) {
    bgColor = "bg-red-400";
  } else {
    bgColor = "bg-lime-400";
  }

  return (
    <>
      <div className={`h-16 ${bgColor} px-3`}>
        <div className="w-1/2 inline-block">
          <span className="text-2xl">#{id}</span>
          <br />
          {waiterDisplayName}
        </div>
        <div className="w-1/2 inline-block text-right text-4xl">
          {/* use a stopwatch only if the order isn't prepared yet */}
          {end ? `${Math.floor(seconds / 60)}:${(seconds % 60).toString().padStart(2, "0")}` : <Stopwatch startSeconds={seconds} updateSeconds={updateSeconds} />}
        </div>
      </div>
      <div className="h-5 bg-slate-200 flex items-center justify-center">STO {tableNumber}</div>
    </>
  );
}

function CalculateSeconds(start, end) {
  const startTime = new Date(start);
  const endTime = end ? new Date(end) : new Date();

  const seconds = Math.floor((endTime - startTime) / 1000);

  return seconds;
}

export default OrderHeader;
