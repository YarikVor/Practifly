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
  value,
  outsideLabel,
  defaultValue,
  disabled,
  isCenter,
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
            InputProps={{
              inputProps: {
                style: {
                  textAlign: isCenter ? "center" : "unset",
                  WebkitTextFillColor: (disabled && isCenter) ? "green" : "",
                  fontWeight: (disabled && isCenter) ? "bold" : "",
                },
              },
            }}
            disabled={disabled}
            defaultValue={defaultValue}
            id={name}
            fullWidth
            value={value}
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
            defaultValue={defaultValue}
            label={label}
            error={error}
            value={value}
            InputProps={{
              inputProps: {
                style: {
                  textAlign: isCenter ? "center" : "unset",
                  WebkitTextFillColor: (disabled && isCenter) ? "green" : "",
                  fontWeight: (disabled && isCenter) ? "bold" : "",
                },
              },
            }}
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