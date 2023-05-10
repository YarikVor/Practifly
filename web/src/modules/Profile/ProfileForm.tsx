import {Box, Typography} from "@mui/material";
import { Image } from "mui-image";

import {FC, useMemo} from "react";

import {useForm} from "react-hook-form";

import {yupResolver} from "@hookform/resolvers/yup";

import {MyInput} from "../../UIComponents/Input/MyInput";

import {UserProfileData} from "../../types/user.interface";

import { profileSchema } from "../../validations/profile.schema";

import {useStyles} from "./styles";


export const ProfileForm: FC = () => {
  const styles = useStyles();

  const validationSchema = useMemo(() => {
    return profileSchema;
  }, []);

  const {
    register,
  } = useForm<UserProfileData>(
    {
      mode: "onChange",
      resolver: yupResolver(validationSchema),
    }
  );

  return (
    <>
      <Box className={styles.rootForm}>
        <Box className={styles.infoBlock}>
          <MyInput outsideLabel="Логін" width={300} name="username" register={register}/>
          <MyInput outsideLabel="Ім'я" width={300} name="firstname" register={register}/>
          <MyInput outsideLabel="Прізвище" width={300} name="lastname" register={register}/>
          <MyInput outsideLabel="Email" width={310} name="email" register={register}/>
          <MyInput outsideLabel="Номер телефону" width={310} name="phoneNumber" register={register}/>
          <MyInput outsideLabel="Дата народження" width={310} register={register} name="birthday" type="date"/>
          <MyInput outsideLabel="Пароль" type="password" width={310} name="password" register={register}/>
        </Box>
        <Box className={styles.rightBlock}>
          <Typography>Фото профіля</Typography>
          <Box className={styles.imageWrapper}>
            <Image width={300} height={270} src="https://img.freepik.com/premium-photo/pakora-bhajiya_57665-861.jpg?w=2000" />
          </Box>
          <Box className={styles.registrationDate}>
            <Typography style={{color: "#808485"}}>Дата реєстрації</Typography>
            <MyInput width={200} register={register} name="registerDate" type="date"/>
          </Box>
        </Box>
      </Box>
    </>
  );
};

