import { Link } from "react-router-dom";
import notfoundImg from "../assets/notfound.png";

function NotFoundPage() {
  return (
    <div className="flex flex-col h-screen justify-center items-center text-white text-2xl">
      <img src={notfoundImg} className="w-1/5" />

      <br />
      <span>Tražena stranica nije pronađena</span>

      <Link to="/" className="underline hover:font-medium">
        Idi na početnu
      </Link>
    </div>
  );
}

export default NotFoundPage;
