function OrderItem({ foodName, quantity }) {
  return (
    <div className="px-4">
      {/* two divs for a shorter bottom border */}
      <div className="border-b border-black flex py-2">
        <div className="mr-5 text-2xl font-bold">{quantity}</div>
        <div className="mt-1">{foodName}</div>
      </div>
    </div>
  );
}

export default OrderItem;
