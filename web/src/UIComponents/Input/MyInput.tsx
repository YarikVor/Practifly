import {FC} from "react";
import {FormControl, InputLabel, OutlinedInput} from "@mui/material";

import {IInput} from "./input.interface";
import {useStyles} from "./styles";

export const MyInput: FC<IInput> = ({
  label,
  id,
  placeholder,
  onChange,
  value,
  inputStyles,
  labelStyles,
}) => {
  const styles = useStyles();
  return (
    <FormControl>
      <InputLabel className={`${styles.labelStyles} ${labelStyles}`} shrink>{label}</InputLabel>
      <OutlinedInput
        id={id}
        onChange={onChange}
        fullWidth
        placeholder={placeholder}
        value={value}
        className={`${styles.inputStyles} ${inputStyles}`}
      />
    </FormControl>
  );
};