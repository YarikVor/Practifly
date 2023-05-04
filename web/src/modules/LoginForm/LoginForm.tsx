import {ChangeEvent, FormEvent, useState} from "react";
import {Form} from "react-router-dom";

import {Box, Button, Typography} from "@mui/material";


import {MyInput} from "../../UIComponents/Input/MyInput";


import {useAppDispatch} from "../../redux/store";

import {fetchLogin} from "../../redux/slices/auth/auth";


import {useStyles} from "./styles";

const LoginForm = () => {
  const styles = useStyles();
  const dispatch = useAppDispatch();

  const [formData, setFormData] = useState({
    email: "",
    password: "",
  });

  const handleChange = async (event: ChangeEvent<HTMLInputElement>) => {
    setFormData({ ...formData, [event.target.name]: event.target.value });
  };

  const sendFormData = async (event: FormEvent) => {
    event.preventDefault();
    event.stopPropagation();
    const data = await dispatch(fetchLogin(formData));
    if (!data.payload) {
      alert("ПРОВІРЬТЕ ПРАВИЛЬНІСТЬ ВВЕДЕНИХ ДАННИХ");
    } else {
      window.localStorage.setItem("practifly", data.payload.toString());

    }
  };

  return (
    <Form onSubmit={sendFormData} className={styles.loginForm}>
      <MyInput name="email" onChange={handleChange} placeholder="Email"/>
      <MyInput name="password" onChange={handleChange} placeholder="Пароль"/>
      <Box className={styles.buttonWrapper}>
        <Typography>Забули пароль?</Typography>
        <Button type="submit" variant="contained" className={styles.submitButton}>Ввійти</Button>
      </Box>
    </Form>
  );
};

export default LoginForm;