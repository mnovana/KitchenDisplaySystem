import { useContext } from "react";
import { AddedItemsContext } from "./AddOrderForm";

function FoodMenu({ setShowFoodMenu, foodTypeId, food }) {
  const { addedItems, setAddedItems } = useContext(AddedItemsContext);

  // if the food exists increment the quantity, else add the food
  // foodName is added to the object so it can be presented to the user inside <AddOrderForm />
  function addFoodItemToArray(e, foodId, foodName) {
    e.preventDefault();

    if (addedItems.find((item) => item.foodId == foodId)) {
      const updatedArray = addedItems.map((item) => (item.foodId == foodId ? { foodId: item.foodId, foodName: foodName, quantity: item.quantity + 1 } : item));
      setAddedItems(updatedArray);
    } else {
      setAddedItems((prevItems) => [...prevItems, { foodId: foodId, foodName: foodName, quantity: 1 }]);
    }
    console.log(addedItems);
  }

  return (
    <>
      <button className="min-w-24 bg-yellow-300 py-2 px-3 shadow-md shadow-stone-400 hover:brightness-110" onClick={() => setShowFoodMenu(false)}>
        ...
      </button>

      {food.map(
        (foodItem) =>
          foodItem.foodTypeId == foodTypeId && (
            <button
              key={foodItem.id}
              className="min-w-24 bg-yellow-300 py-2 px-3 shadow-md shadow-stone-400 hover:brightness-110"
              onClick={(e) => addFoodItemToArray(e, foodItem.id, foodItem.name)}
            >
              {foodItem.name}
            </button>
          )
      )}
    </>
  );
}

export default FoodMenu;
