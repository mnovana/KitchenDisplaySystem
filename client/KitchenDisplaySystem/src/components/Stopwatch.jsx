import { useState, useEffect } from "react";

// we pass updateSeconds so that the whole header re-renders after 20 minutes and changes color
function Stopwatch({ startSeconds, updateSeconds }) {
  const [seconds, setSeconds] = useState(startSeconds);
  const minutes = Math.floor(seconds / 60);

  useEffect(() => {
    const intervalId = setInterval(() => {
      setSeconds((prevSeconds) => prevSeconds + 1);
    }, 1000);

    return () => clearInterval(intervalId);
  }, []);

  useEffect(() => {
    if (seconds >= 1200) {
      updateSeconds(seconds);
    }
  }, [seconds]);

  return (
    <>
      {minutes}:{(seconds % 60).toString().padStart(2, "0")}
    </>
  );
}

export default Stopwatch;
