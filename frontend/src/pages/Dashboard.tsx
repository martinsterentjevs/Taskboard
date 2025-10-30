export function Dashboard() {
  const token = localStorage.getItem("accessToken");
  return (
    <div className="p-6 text-center">
      <h1 className="text-3xl font-bold">Dashboard</h1>
      <p className="text-muted-foreground mt-2">
        {token ? "Authenticated ✅" : "Please log in"}
      </p>
    </div>
  );
}
