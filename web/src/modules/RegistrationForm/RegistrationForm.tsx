import React, {useMemo} from "react";
import {Form, useNavigate} from "react-router-dom";

import {Box, Button} from "@mui/material";

import { useForm} from "react-hook-form";

import {yupResolver} from "@hookform/resolvers/yup";

import moment from "moment";

import {useAppDispatch, useAppSelector} from "../../hooks/hooks";

import {MyInput} from "../../UIComponents/Input/MyInput";
import {fetchRegistration} from "../../redux/slices/user/user.slice";

import {registrationSchema} from "../../validations/registration.schema";

import {setTokenToLocalStorage} from "../../handlers/handlers";

import {UserRegisterData} from "../../types/user.interface";

import {statusTypes} from "../../types/enums";

import {useStyles} from "./styles";


const RegistrationForm = () => {
  const styles = useStyles();
  const dispatch = useAppDispatch();
  const navigate = useNavigate();
  const isSubmitted = useAppSelector(store => store.user.status === statusTypes.LOADING);

  const validationSchema = useMemo(() => {
    return registrationSchema;
  }, []);


  const {
    formState:{errors, isValid},
    handleSubmit,
    register,
  } = useForm<UserRegisterData>(
    {
      mode: "onChange",
      defaultValues:{
        birthday: moment("01-01-2024").format("DD/MM/YYYY"),
      },
      resolver: yupResolver(validationSchema),
    }
  );

  const customSubmit = async (data: UserRegisterData) => {
    const birthday = moment(data.birthday).format("DD/MM/YYYY");
    const {token} = await dispatch(fetchRegistration({...data, birthday: birthday})).unwrap();
    if(token) {
      await setTokenToLocalStorage("token", token);
      navigate("/");
    }
  };
  return (
    <Form onSubmit={handleSubmit(customSubmit)} className={styles.registrationForm}>
      <Box className={styles.insideFormWrapper}>
        <MyInput
          label="Логін"
          error={Boolean(errors.username?.message)}
          helperText={errors.username?.message}
          register={register}
          name="username"
        />
        <MyInput
          label="Ім`я"
          error={Boolean(errors.firstname?.message)}
          helperText={errors.firstname?.message}
          register={register}
          name="firstname"
        />
        <MyInput
          label="Прізвище"
          error={Boolean(errors.lastname?.message)}
          helperText={errors.lastname?.message}
          register={register}
          name="lastname"
        />
        <MyInput
          label="Email"
          error={Boolean(errors.email?.message)}
          helperText={errors.email?.message}
          register={register}
          name="email"
        />
        <MyInput
          label="Телефон"
          error={Boolean(errors.phoneNumber?.message)}
          helperText={errors.phoneNumber?.message}
          register={register}
          name="phoneNumber"
        />
        <MyInput
          register={register}
          name="birthday"
          type="date"
          error={Boolean(errors.birthday?.message)}
          helperText={errors.birthday?.message}
        />
        <MyInput
          label="Пароль"
          error={Boolean(errors.password?.message)}
          helperText={errors.password?.message}
          register={register}
          name="password"
        />
      </Box>
      <Button type="submit" disabled={isSubmitted || !isValid} variant="contained" className={styles.submitButton}>Зареєструватися</Button> {/* isSubmitted вказує чи форма була відправлена та isValid вказує на те, чи дані, які введені, відповідають валідації. Кнопка відправлення вимкнена, якщо дані не валідні або дані в процесі відправлення. */}
    </Form>
  );
};

export default RegistrationForm;