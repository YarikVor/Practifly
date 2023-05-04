import {ChangeEvent} from "react";

export interface IInput {
  id?: string;
  label?: string;
  variant?: "standard" | "filled" | "outlined";
  placeholder?: string;
  name?: string;
  onChange?: (event: ChangeEvent<HTMLInputElement>) => void;
  labelStyles?: string;
  value?: string;
  inputStyles?: string;
}
