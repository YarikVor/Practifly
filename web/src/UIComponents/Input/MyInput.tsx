import {FC} from "react";
import {FormControl, InputLabel, OutlinedInput, Typography} from "@mui/material";

import {IInput} from "./input.interface";
import {useStyles} from "./styles";

export const MyInput: FC<IInput> = ({
  label,
  type,
  id,
  placeholder,
  onChange,
  value,
  inputStyles,
  labelStyles,
  name,
  register,
  error,
  helperText,
}) => {
  const styles = useStyles();
  return (
    <FormControl>
      <InputLabel className={`${styles.labelStyles} ${labelStyles}`} shrink>{label}</InputLabel>
      <OutlinedInput
        id={id}
        onChange={onChange}
        fullWidth
        error={error}
        name={name}
        type={type}
        {...register(name)}
        placeholder={placeholder}
        value={value}
        className={`${styles.inputStyles} ${inputStyles}`}
      />
      {helperText &&
      <Typography className={styles.error}>{helperText}</Typography>
      }
    </FormControl>
  );
};