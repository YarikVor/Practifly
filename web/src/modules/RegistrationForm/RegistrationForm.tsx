import React from "react";
import {Form} from "react-router-dom";

import {Box, Button} from "@mui/material";

import {MyInput} from "../../UIComponents/Input/MyInput";

import {useStyles} from "./styles";

const RegistrationForm = () => {
  const styles = useStyles();
  return (
    <Form className={styles.registrationForm}>
      <Box>
        <MyInput label="Ім'я"/>
        <MyInput label="Прізвище"/>
        <MyInput label="Email"/>
        <MyInput label="Телефон"/>
        <MyInput label="Дата народження"/>
        <MyInput label="Пароль"/>
        <MyInput label="Підтвердження пароля"/>
      </Box>
      <Button variant="contained" className={styles.submitButton}>Зареєструватися</Button>
    </Form>
  );
};

export default RegistrationForm;