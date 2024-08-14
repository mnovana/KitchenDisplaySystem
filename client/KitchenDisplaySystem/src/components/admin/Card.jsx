function Card({ headerColor, title, number }) {
  return (
    <div className="h-40 w-56 rounded-md bg-white shadow-md">
      <div className={`${headerColor} rounded-t-md text-white py-3 mb-8`}>{title}</div>
      <span className="text-5xl text-neutral-800">{number}</span>
    </div>
  );
}

export default Card;
