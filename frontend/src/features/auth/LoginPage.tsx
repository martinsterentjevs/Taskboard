import { Card, CardContent, CardFooter } from "@/components/ui/card.tsx";
import { LoginForm } from "../LoginForm.tsx";
import { ThemeToggle } from "@/components/theme-toggle.tsx";

export function LoginPage() {
  return (
    <div>
      <div className="flex justify-end p-4">
        <ThemeToggle />
      </div>
      <div className="flex min-h-screen items-center justify-center bg-background">
        <Card className="w-[400px] shadow-md">
          <CardContent>
            <LoginForm />
          </CardContent>
          <CardFooter className="text-center text-sm">
            Don’t have an account?{" "}
            <a href="/register" className="underline underline-offset-4">
              Sign up
            </a>
          </CardFooter>
        </Card>
      </div>
    </div>
  );
}
