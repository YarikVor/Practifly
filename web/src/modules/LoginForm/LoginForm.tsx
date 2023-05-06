import {useMemo} from "react";
import {Form} from "react-router-dom";

import {Box, Button, Typography} from "@mui/material";

import {useForm} from "react-hook-form";

import {yupResolver} from "@hookform/resolvers/yup";

import {MyInput} from "../../UIComponents/Input/MyInput";


import {UserLoginData} from "../../redux/slices/auth/auth.interfaces";

import {useAppDispatch, useAppSelector} from "../../hooks/hooks";
import {fetchLogin} from "../../redux/slices/auth/auth";

import {loginSchema} from "../../validations/login.schema";

import {statusTypes} from "../../types/status.types";

import {useStyles} from "./styles";

const LoginForm = () => {
  const styles = useStyles();
  const dispatch = useAppDispatch();
  const isSubmitted = useAppSelector(store => store.auth.status === statusTypes.LOADING);

  const validationSchema = useMemo(() => {
    return loginSchema;
  }, []);

  const {
    formState: {errors},
    handleSubmit,
    register,
  } = useForm<UserLoginData>(
    {
      resolver: yupResolver(validationSchema),
    }
  );

  console.log(errors);

  const customSubmit = async (data: UserLoginData) => {
    await dispatch(fetchLogin(data));
  };

  return (
    <Form onSubmit={handleSubmit(customSubmit)} className={styles.loginForm}>
      <MyInput
        placeholder="Email"
        error={Boolean(errors.email?.message)}
        helperText={errors.email?.message}
        register={register}
        name="email"
      />
      <MyInput
        placeholder="Password"
        error={Boolean(errors.password?.message)}
        helperText={errors.password?.message}
        register={register}
        name="password"
      />
      <Box className={styles.buttonWrapper}>
        <Typography>Забули пароль?</Typography>
        <Button
          type="submit"
          disabled={isSubmitted}
          variant="contained"
          className={styles.submitButton}
          children="Ввійти"
        />
      </Box>
    </Form>
  );
};

export default LoginForm;