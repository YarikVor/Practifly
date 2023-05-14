import * as yup from "yup";
import moment from "moment";

const phoneRegExp = /^([+]?[\s0-9]+)?(\d{3}|[(]?[0-9]+[)])?([-]?[\s]?[0-9])+$/;
const minDate = moment("01-01-1900").format("DD/MM/YYYY");

export const registrationSchema = yup.object({
  username: yup.string().max(64, "Username length cannot be more, that 64").required("Field is required"),
  firstname: yup.string().max(128, "Firstname length cannot be more, that 128").required("Field is required"),
  lastname: yup.string().max(128, "Lastname length cannot be more, that 128").required("Field is required"),
  email: yup.string().email("Email must be a valid Email. For example: 'example@gmail.com' ").max(64, "Email length cannot be more, that 64").required("Field is required"),
  phoneNumber: yup.string().matches(phoneRegExp, "Phone number is not valid").required("Field is required").required("A phone number is required"),
  birthday: yup.date().min(minDate, "Date cannot be les than 1900 year").max(moment(), "Please select correct date").required("Field is required").typeError("Incorrect date!"),
  password: yup.string().min(8, "Password cannot be less, than 8 symbols").max(256, "Password length cannot be more, that 256").required("Field is required"),
});