import {ChangeEvent} from "react";

export interface IInput {
  id?: string;
  label?: string;
  type?:string;
  variant?: "standard" | "filled" | "outlined";
  placeholder?: string;
  name?: string;
error?: boolean;
helperText?: string;
  register?: any
  onChange?: (event: ChangeEvent<HTMLInputElement>) => void;
  labelStyles?: string;
  value?: string;
  inputStyles?: string;
}
