import { useForm } from "react-hook-form";
import { z } from "zod";
import { zodResolver } from "@hookform/resolvers/zod";
import { useState } from "react";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { cn } from "@/lib/utils";
import { login } from "./api";
import {
  Field,
  FieldDescription,
  FieldGroup,
  FieldLabel,
  FieldLegend,
  FieldSet,
} from "@/components/ui/field.tsx";
import { PasswordInput } from "@/components/ui/password-input.tsx";

const schema = z.object({
  identifier: z.string(),
  password: z.string().min(4),
});
type FormData = z.infer<typeof schema>;

export function LoginForm() {
  const form = useForm<FormData>({ resolver: zodResolver(schema) });
  const [error, setError] = useState<string | null>(null);

  const onSubmit = async (data: FormData) => {
    try {
      const tokens = await login(data.identifier, data.password);
      localStorage.setItem("accessToken", tokens.accessToken);
      localStorage.setItem("refreshToken", tokens.refreshToken);
      window.location.href = "/";
    } catch (e: unknown) {
      setError(e instanceof Error ? e.message : "Login failed");
    }
  };

  return (
    <form onSubmit={form.handleSubmit(onSubmit)} className={cn("space-y-4")}>
      <FieldGroup>
        <FieldSet>
          <FieldLegend className="items-center">
            {" "}
            Log into your account.
          </FieldLegend>
          <FieldDescription>Log in to access your boards</FieldDescription>
          <FieldGroup>
            <Field>
              <FieldLabel>E-Mail or username</FieldLabel>
              <Input id="identifier" {...form.register("identifier")} />
            </Field>
            <Field>
              <FieldLabel>Password</FieldLabel>
              <PasswordInput
                id="password"
                type="password"
                placeholder="********"
                {...form.register("password")}
              />
            </Field>
          </FieldGroup>
        </FieldSet>
      </FieldGroup>
      {error && <p className="text-sm text-red-500">{error}</p>}
      <Button type="submit" className="w-full">
        Login
      </Button>
    </form>
  );
}
