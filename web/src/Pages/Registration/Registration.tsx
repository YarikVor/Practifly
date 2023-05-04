import React from "react";

import {Typography} from "@mui/material";

import {NavLink} from "react-router-dom";

import RegistrationForm from "../../modules/RegistrationForm/RegistrationForm";

import {useStyles} from "./styles";

const Registration = () => {
  const styles = useStyles();
  return (
    <>
      <Typography className={styles.h4} variant="h4">Реєстрація користувача</Typography>
      <RegistrationForm/>
      <Typography className={styles.loginLink}>Уже маєте обліковий запис?</Typography>
      <Typography className={styles.loginLink}>
        <NavLink to="/login">Увійти</NavLink>
      </Typography>
    </>
  );
};

export default Registration;