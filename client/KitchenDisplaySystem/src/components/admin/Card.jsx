import { SyncLoader } from "react-spinners";

function Card({ headerColor, title, number = null }) {
  return (
    <div className="h-40 w-56 rounded-md bg-white shadow-md">
      <div className={`${headerColor} rounded-t-md text-white py-3 mb-8`}>{title}</div>
      {number === null ? <SyncLoader color="#ebebeb" /> : <span className="text-5xl text-neutral-800">{number}</span>}
    </div>
  );
}

export default Card;
