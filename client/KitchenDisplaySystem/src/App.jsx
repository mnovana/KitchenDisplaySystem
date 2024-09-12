import React from "react";
import { createBrowserRouter, createRoutesFromElements, Route, RouterProvider } from "react-router-dom";
import OrdersPage from "./pages/OrdersPage";
import LoginPage from "./pages/LoginPage";
import PanelPage from "./pages/PanelPage";
import FoodSettingsPage from "./pages/SettingsPages/FoodSettingsPage";
import FoodTypesSettingsPage from "./pages/SettingsPages/FoodTypesSettingsPage";
import OrdersSettingsPage from "./pages/SettingsPages/OrdersSettingsPage";
import TablesSettingsPage from "./pages/SettingsPages/TablesSettingsPage";
import WaitersSettingsPage from "./pages/SettingsPages/WaitersSettingsPage";
import NotFoundPage from "./pages/NotFoundPage";
import ProtectedRoute from "./components/ProtectedRoute";
import AdminHeader from "./layouts/AdminHeader";

function App() {
  const router = createBrowserRouter(
    createRoutesFromElements(
      <Route>
        <Route element={<ProtectedRoute usernames={["waiter", "kitchen"]} />}>
          <Route index element={<OrdersPage />} />
        </Route>
        <Route element={<ProtectedRoute usernames={null} />}>
          <Route path="/login" element={<LoginPage />} />
        </Route>
        <Route element={<ProtectedRoute usernames={["admin"]} />}>
          <Route element={<AdminHeader />}>
            <Route path="/admin" element={<PanelPage />} />
            <Route path="/settings/food" element={<FoodSettingsPage />} />
            <Route path="/settings/foodtypes" element={<FoodTypesSettingsPage />} />
            <Route path="/settings/orders" element={<OrdersSettingsPage />} />
            <Route path="/settings/tables" element={<TablesSettingsPage />} />
            <Route path="/settings/waiters" element={<WaitersSettingsPage />} />
          </Route>
        </Route>

        <Route path="*" element={<NotFoundPage />} />
      </Route>
    )
  );

  return <RouterProvider router={router} />;
}

export default App;
