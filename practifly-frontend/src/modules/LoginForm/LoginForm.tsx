import React from "react";
import {Form} from "react-router-dom";

import {Box, Button, Typography} from "@mui/material";

import {MyInput} from "../../UIComponents/Input/MyInput";

import {useStyles} from "./styles";

const LoginForm = () => {
  const styles = useStyles();
  return (
    <Form className={styles.loginForm}>
      <MyInput placeholder="Email"/>
      <MyInput placeholder="Пароль"/>
      <Box className={styles.buttonWrapper}>
        <Typography>Забули пароль?</Typography>
        <Button variant="contained" className={styles.submitButton}>Ввійти</Button>
      </Box>
    </Form>
  );
};

export default LoginForm;