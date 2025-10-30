import { useForm } from "react-hook-form";
import { z } from "zod";
import { zodResolver } from "@hookform/resolvers/zod";
import { useState } from "react";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { cn } from "@/lib/utils";
import { register } from "./api";
import {
  Field,
  FieldDescription,
  FieldGroup,
  FieldLabel,
  FieldLegend,
  FieldSeparator,
  FieldSet,
} from "@/components/ui/field";

const schema = z.object({
  fullName: z.string(),
  username: z.string(),
  email: z.string().email(),
  password: z.string().min(4),
});
type FormData = z.infer<typeof schema>;

export function RegisterForm() {
  const form = useForm<FormData>({ resolver: zodResolver(schema) });
  const [error, setError] = useState<string | null>(null);

  const onSubmit = async (data: FormData) => {
    try {
      const tokens = await register(
        data.fullName,
        data.username,
        data.email,
        data.password,
      );
      localStorage.setItem("accessToken", tokens.accessToken);
      localStorage.setItem("refreshToken", tokens.refreshToken);
      window.location.href = "/";
    } catch (e: unknown) {
      setError(e instanceof Error ? e.message : "Login failed");
    }
  };

  return (
    <form
      onSubmit={form.handleSubmit(onSubmit)}
      className={cn("space-y-4 items-center")}
    >
      <FieldGroup>
        <FieldSet>
          <FieldLegend className="items-center">
            {" "}
            Register Your account
          </FieldLegend>
          <FieldDescription>Fill out all these fields</FieldDescription>
          <FieldGroup>
            <Field>
              <FieldLabel>Full Name</FieldLabel>
              <Input id="" placeholder="John Doe" />
            </Field>
            <FieldSeparator />
            <Field>
              <FieldLabel>Username</FieldLabel>
              <Input id="username" placeholder="JohnDoe123" />
            </Field>

            <Field>
              <FieldLabel>E-Mail</FieldLabel>
              <Input
                id="email"
                type="email"
                placeholder="example@example.com"
              />
            </Field>
            <Field>
              <FieldLabel>Password</FieldLabel>
              <Input id="password" type="password" />
            </Field>
          </FieldGroup>
        </FieldSet>
      </FieldGroup>
      {error && <p className="text-sm text-red-500">{error}</p>}
      <Button type="submit" className="w-full">
        Register
      </Button>
    </form>
  );
}
