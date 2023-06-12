import {useEffect, useMemo} from "react";
import {Form, useNavigate} from "react-router-dom";

import {Box, Button, Typography} from "@mui/material";

import {useForm} from "react-hook-form";

import {yupResolver} from "@hookform/resolvers/yup";

import {MyInput} from "../../UIComponents/Input/MyInput";


import {useAppDispatch, useAppSelector} from "../../hooks/hooks";
import {fetchLogin, fetchMe} from "../../redux/slices/user/user.slice";

import {loginSchema} from "../../validations/login.schema";


import {setTokenToLocalStorage} from "../../handlers/handlers";

import {UserLoginData} from "../../types/user.interface";

import {statusTypes} from "../../types/enums";


import {useStyles} from "./styles";

const LoginForm = () => {
  const styles = useStyles();
  const dispatch = useAppDispatch();
  const navigate = useNavigate();
  const isSubmitted = useAppSelector(store => store.user.status === statusTypes.LOADING);

  const validationSchema = useMemo(() => {
    return loginSchema;
  }, []);

  const {
    formState: {errors, isValid},
    handleSubmit,
    register,
  } = useForm<UserLoginData>(
    {
      mode: "onChange",
      resolver: yupResolver(validationSchema),
    }
  );

  const customSubmit = async (data: UserLoginData) => {
    const {token} = await dispatch(fetchLogin(data)).unwrap();
    if(token) {
      setTokenToLocalStorage("token", token);
    }
    const response = await dispatch(fetchMe()).unwrap();
    if(response) {
      navigate("/");
    }
  };

  return (
    <Form onSubmit={handleSubmit(customSubmit)} className={styles.loginForm}>
      <MyInput
        label="Email"
        error={Boolean(errors.email?.message)}
        helperText={errors.email?.message}
        register={register}
        name="email"
      />
      <MyInput
        label="Password"
        error={Boolean(errors.password?.message)}
        helperText={errors.password?.message}
        register={register}
        name="password"
      />
      <Box className={styles.buttonWrapper}>
        <Typography>Забули пароль?</Typography>
        <Button
          type="submit"
          disabled={isSubmitted || !isValid}
          variant="contained"
          className={styles.submitButton}
          children="Ввійти"
        />
      </Box>
    </Form>
  );
};

export default LoginForm;