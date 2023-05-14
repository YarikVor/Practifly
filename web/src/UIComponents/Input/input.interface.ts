import {ChangeEvent} from "react";
import {UseFormRegister } from "react-hook-form";
import * as React from "react";

export interface IInput {
  id?: string;
  label?: string;
  type?:string;
  outsideLabel?: string;
  variant?: "standard" | "filled" | "outlined";
  placeholder?: string;
  disabled?: boolean;
  width?: React.CSSProperties["width"] | number | undefined;
  name: string;
  error?: boolean;
  helperText?: string;
  register?: UseFormRegister<any>;
  onChange?: (event: ChangeEvent<HTMLInputElement>) => void;
  labelStyles?: string;
  value?: string;
  inputStyles?: string;
}
