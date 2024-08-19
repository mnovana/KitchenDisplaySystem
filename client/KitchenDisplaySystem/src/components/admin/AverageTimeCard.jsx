import { useContext, useEffect, useState } from "react";
import whiteClockImg from "../../assets/white_clock.png";
import { UserContext } from "../../pages/PanelPage";

function AverageTimeCard() {
  const user = useContext(UserContext);
  const [avgTime, setAvgTime] = useState(0);

  useEffect(() => {
    const serverUrl = import.meta.env.VITE_SERVER_URL;

    const headers = {};
    headers.Authorization = "Bearer " + user.token;

    fetch(`${serverUrl}/orders/time`, { headers: headers })
      .then((response) => {
        if (response.status == 200) {
          response.json().then(setAvgTime);
        } else {
          alert("Average prepare time fetch failed");
        }
      })
      .catch((error) => console.log("Average time card: " + error));
  }, []);

  return (
    <>
      <span>Prosečno vreme pripreme</span>
      <img src={whiteClockImg} className="w-1/3" />
      <span className="text-6xl font-light">{avgTime} min</span>
    </>
  );
}

export default AverageTimeCard;
