import {Avatar, Box, Typography} from "@mui/material";

import {ChangeEvent, FC, memo, useEffect, useMemo, useRef, useState} from "react";

import {useForm} from "react-hook-form";

import {yupResolver} from "@hookform/resolvers/yup";

import {MyInput} from "../../UIComponents/Input/MyInput";

import {UserProfileData} from "../../types/user.interface";

import {profileSchema} from "../../validations/profile.schema";

import {useAppDispatch} from "../../hooks/hooks";
import {fetchMe, uploadPhoto} from "../../redux/slices/auth/auth";

import {useStyles} from "./styles";


export const ProfileForm: FC = () => {
  const styles = useStyles();
  const dispatch = useAppDispatch();

  const inputAvatarRef = useRef<HTMLInputElement>(null);
  const [image, setImage]
    = useState("");
  const [key, setKey] = useState(0);
  const incKey = () => setKey(paramKey => paramKey + 1);

  const validationSchema = useMemo(() => {
    return profileSchema;
  }, []);

  const {
    register,
    reset,
  } = useForm<UserProfileData>(
    {
      mode: "onChange",
      resolver: yupResolver(validationSchema),
    },
  );

  const handleImageChange = async (e: ChangeEvent<HTMLInputElement>) => {
    const formData = new FormData();
    formData.append("file", e.target.files![0]);
    const {payload} = (await dispatch(uploadPhoto(formData))) as {url: string};
    setImage(payload.url + "?" + key);
    console.log("key", key);
    console.log(payload);
    incKey();
  };

  useEffect( () => {
    const fetchData = async () => {
      const {payload} = await dispatch(fetchMe());
      if(!payload || typeof payload === "string"){
        return;
      }
      reset({
        username: payload?.username,
        firstName: payload?.firstName,
        lastName: payload?.lastName,
        email: payload?.email,
        phoneNumber: payload?.phoneNumber,
        birthday: payload?.birthday,
        registrationDate: payload?.registrationDate,
      });
      console.log(payload.filePhoto);
      setImage(payload.filePhoto);
    };
    fetchData();
  }, []);

  return (
    <>
      <Box className={styles.rootForm}>
        <Box className={styles.infoBlock}>
          <MyInput
            outsideLabel="Логін"
            width={300}
            name="username"
            disabled
            register={register}/>
          <MyInput
            outsideLabel="Ім'я"
            width={300}
            name="firstName"
            register={register}/>
          <MyInput
            outsideLabel="Прізвище"
            width={300}
            name="lastName"
            register={register}/>
          <MyInput
            outsideLabel="Email"
            width={310}
            name="email"
            register={register}/>
          <MyInput
            outsideLabel="Номер телефону"
            width={310}
            name="phoneNumber"
            register={register}/>
          <MyInput
            outsideLabel="Дата народження"
            width={310}
            register={register}
            name="birthday"
            type="date"/>
          <MyInput
            outsideLabel="Пароль"
            type="password"
            width={310}
            name="password"
            register={register}/>
        </Box>
        <Box className={styles.rightBlock}>
          <Typography>Фото профіля</Typography>
          <Box className={styles.imageWrapper}>
            <Avatar
              onClick={() => inputAvatarRef.current?.click()}
              sx={{ borderRadius: 0, width: 250, height: 200 }}
              src={image}
            />
            <input
              ref={inputAvatarRef}
              type="file"
              accept="image/*"
              onChange={handleImageChange}
              name="avatarUrl"
              hidden
            />
          </Box>
          <Box className={styles.registrationDate}>
            <Typography style={{color: "#808485"}}>Дата реєстрації</Typography>
            <MyInput
              width={200}
              register={register}
              name="registrationDate"
              type="date"/>
          </Box>
        </Box>
      </Box>
    </>
  );
};

