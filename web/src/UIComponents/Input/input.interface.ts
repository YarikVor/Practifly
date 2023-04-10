export interface IInput {
  id?: string;
  label?: string;
  variant?: "standard" | "filled" | "outlined";
  placeholder?: string;
  onChange?: (event: React.ChangeEvent<HTMLTextAreaElement | HTMLInputElement>) => void;
  labelStyles?: string;
  value?: string;
  inputStyles?: string;
}
