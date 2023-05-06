import * as yup from "yup";

export const loginSchema = yup.object({
  email: yup.string().email("Email must be a valid Email. For example: 'example@gmail.com' ").max(64, "Email length cannot be more, that 64").required("Field is required"),
  password: yup.string().min(8, "Password cannot be less, than 8 symbols").max(256, "Password length cannot be more, that 256").required("Field is required"),
});