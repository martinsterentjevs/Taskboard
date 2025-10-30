import { Card, CardContent, CardFooter } from "@/components/ui/card.tsx";
import { RegisterForm } from "../RegisterForm.tsx";
import { ThemeToggle } from "@/components/theme-toggle.tsx";

export function RegisterPage() {
  return (
    <div>
      <div className="flex justify-end p-4">
        <ThemeToggle />
      </div>
      <div className="flex min-h-screen items-center justify-center bg-surface">
        <Card className="w-[400px] shadow-md">
          <CardContent>
            <RegisterForm />
          </CardContent>
          <CardFooter className="text-center text-sm">
            <p>
              Already have an account?{" "}
              <a href="/login" className="underline hover:underline">
                Log in
              </a>
            </p>
          </CardFooter>
        </Card>
      </div>
    </div>
  );
}
