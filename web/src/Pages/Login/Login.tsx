
import {Box, Typography} from "@mui/material";


import {NavLink, useNavigate} from "react-router-dom";

import {useEffect} from "react";

import LoginForm from "../../modules/LoginForm/LoginForm";

import {useAppSelector} from "../../hooks/hooks";

import {useStyles} from "./styles";

const Login = () => {
  const styles = useStyles();
  const isAuth = useAppSelector(store => store.user.profileData);
  const navigate = useNavigate();
  useEffect(() => {
    if(isAuth) {
      navigate("/");
    }
  }, [isAuth]);

  return (
    <Box className={styles.wrapper}>
      <Typography className={styles.h2} variant="h2">Вхід</Typography>
      <Typography className={styles.h2} variant="h4">до особистого кабінету</Typography>
      <LoginForm/>
      <Typography className={styles.loginLink} variant="h6">Забули пароль?</Typography>
      <Typography className={styles.loginLink} variant="h6">
        <NavLink to="/registration">Створити обліковий запис</NavLink>
      </Typography>
    </Box>
  );
};

export default Login;