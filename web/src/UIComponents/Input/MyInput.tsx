import React, {FC} from "react";
import {Box, InputLabel, TextField} from "@mui/material";

import {IInput} from "./input.interface";
import {useStyles} from "./styles";

export const MyInput: FC<IInput> = ({
  label,
  type,
  name,
  register,
  error,
  helperText,
  width,
  outsideLabel,
}) => {
  const {classes} = useStyles({"width": width, "isBlock": Boolean(outsideLabel)});
  return (
    <>
      {outsideLabel ?
        (<Box className={classes.wrapper}>
          <InputLabel className={classes.labelStyles} shrink htmlFor={name}>{outsideLabel}</InputLabel>
          <TextField
            id={name}
            fullWidth
            error={error}
            helperText={helperText}
            {...register(name)}
            type={type}
            className={classes.root}
          />
        </Box>
        ) : (
          <TextField
            id={name}
            label={label}
            error={error}
            helperText={helperText}
            {...register(name)}
            type={type}
            className={classes.root}
          />
        )
      }
    </>
  );
};