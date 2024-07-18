function FoodTypeMenu({ setShowFoodMenu, setFoodTypeId, foodTypes }) {
  return (
    <>
      {foodTypes.map((foodType) => (
        <button
          key={foodType.id}
          className="min-w-24 bg-orange-400 py-2 px-3 shadow-md shadow-stone-400 hover:brightness-110"
          onClick={() => {
            setFoodTypeId(foodType.id);
            setShowFoodMenu(true);
          }}
        >
          {foodType.name.toUpperCase()}
        </button>
      ))}
    </>
  );
}

export default FoodTypeMenu;
