import { createBrowserRouter } from "react-router-dom";
import { LoginPage } from "@/features/auth/LoginPage";
import { RegisterPage } from "@/features/auth/RegisterPage";
import { Dashboard } from "@/pages/Dashboard";
import { NotFound } from "@/pages/NotFound";

export const router = createBrowserRouter([
  { path: "/", element: <Dashboard /> },
  { path: "/login", element: <LoginPage /> },
  { path: "*", element: <NotFound /> },
  { path: "/register", element: <RegisterPage /> },
]);
