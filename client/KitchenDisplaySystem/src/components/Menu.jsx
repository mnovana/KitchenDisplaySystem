import FoodTypeMenu from "./FoodTypeMenu";
import FoodMenu from "./FoodMenu";
import { useState } from "react";

function Menu({ orderData }) {
  const [showFoodMenu, setShowFoodMenu] = useState(false);
  const [foodTypeId, setFoodTypeId] = useState(0);

  return (
    <div className="bg-white w-2/3 shadow-md flex justify-start items-start flex-wrap gap-3 p-3 overflow-x-auto">
      {showFoodMenu ? (
        <FoodMenu setShowFoodMenu={setShowFoodMenu} foodTypeId={foodTypeId} food={orderData.food} />
      ) : (
        <FoodTypeMenu setShowFoodMenu={setShowFoodMenu} setFoodTypeId={setFoodTypeId} foodTypes={orderData.foodTypes} />
      )}
    </div>
  );
}

export default Menu;
