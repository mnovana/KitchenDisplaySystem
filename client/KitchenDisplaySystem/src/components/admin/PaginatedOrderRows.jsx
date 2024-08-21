import { useEffect, useState } from "react";
import OrderRow from "./OrderRow";

function PaginatedOrderRows({ orders }) {
  const [currentPage, setCurrentPage] = useState(1);

  // set current page back to 1 whenever new orders are fetched
  useEffect(() => setCurrentPage(1), [orders]);

  const ordersPerPage = 10;
  const numberOfPages = Math.ceil(orders.length / ordersPerPage);
  const numbers = [];
  for (let i = 1; i <= numberOfPages; i++) {
    numbers.push(i);
  }

  const startIndex = ordersPerPage * (currentPage - 1);
  const endIndex = startIndex + ordersPerPage;

  return (
    <div className="flex flex-col h-5/6">
      <div className="flex-1">
        {orders.slice(startIndex, endIndex).map((order) => (
          <OrderRow key={order.id} order={order} />
        ))}
      </div>
      <div className="text-center my-5 text-2xl">
        {numbers.map((number) => (
          <button key={number} className={`${currentPage == number && "font-bold"} mx-3`} onClick={() => setCurrentPage(number)}>
            {number}
          </button>
        ))}
      </div>
    </div>
  );
}

export default PaginatedOrderRows;
