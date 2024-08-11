import React from "react";
import { createBrowserRouter, createRoutesFromElements, Route, RouterProvider } from "react-router-dom";
import OrdersPage from "./pages/OrdersPage";
import LoginPage from "./pages/LoginPage";
import PanelPage from "./pages/PanelPage";
import NotFoundPage from "./pages/NotFoundPage";
import ProtectedRoute from "./components/ProtectedRoute";

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
          <Route path="/admin" element={<PanelPage />} />
        </Route>

        <Route path="*" element={<NotFoundPage />} />
      </Route>
    )
  );

  return <RouterProvider router={router} />;
}

export default App;
