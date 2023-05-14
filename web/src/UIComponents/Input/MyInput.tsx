import React, {FC} from "react";
import {Box, InputLabel, TextField} from "@mui/material";

import {Image} from "mui-image";

import lockImage from "../../assets/lock.png";

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
  disabled,
}) => {
  const {classes} = useStyles({"width": width, "isBlock": Boolean(outsideLabel)});
  return (
    <>
      {outsideLabel ?
        (<Box className={classes.wrapper}>
          <InputLabel className={classes.labelStyles} shrink htmlFor={name}>
            {outsideLabel}
            {disabled &&
                <Image className={classes.image} width={20} height={25} src={lockImage} />
            }
          </InputLabel>
          <TextField
            disabled={disabled}
            id={name}
            fullWidth
            error={error}
            helperText={helperText}
            {...!register ? "" : {...register(name)}}
            type={type}
            className={classes.root}
          />
        </Box>
        ) : (
          <TextField
            id={name}
            disabled={disabled}
            label={label}
            error={error}
            helperText={helperText}
            {...!register ? "" : {...register(name)}}
            type={type}
            className={classes.root}
          />
        )
      }
    </>
  );
};