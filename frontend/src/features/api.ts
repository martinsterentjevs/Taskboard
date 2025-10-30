export const API_URL = import.meta.env.VITE_API_URL ?? "http://localhost:8080";

export type LoginResponse = {
  accessToken: string;
  refreshToken: string;
};

export async function login(
  email: string,
  password: string,
): Promise<LoginResponse> {
  const res = await fetch(`${API_URL}/auth/login`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ email, password }),
  });
  if (!res.ok) throw new Error("Invalid credentials");
  return res.json();
}
export async function register( //incomplete
  fullName: string,
  username: string,
  email: string,
  password: string,
): Promise<LoginResponse> {
  const res = await fetch(`${API_URL}/auth/register`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ fullName, username, email, password }),
  });
  if (!res.ok) throw new Error("Invalid credentials");
  return res.json();
}
