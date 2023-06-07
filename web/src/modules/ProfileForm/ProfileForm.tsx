import {Avatar, Box, Button, CircularProgress, Typography} from "@mui/material";

import React, {ChangeEvent, FC, useLayoutEffect, useMemo, useState} from "react";


import {useForm} from "react-hook-form";

import {yupResolver} from "@hookform/resolvers/yup";

import {toast} from "react-toastify";

import _ from "lodash";

import {Image} from "mui-image";

import {Form} from "react-router-dom";

import moment from "moment/moment";

import editIcon from "../../assets/edit.png";

import defaultAvatar from "../../assets/defaultAvatar.png";

import {MyInput} from "../../UIComponents/Input/MyInput";


import {profileSchema} from "../../validations/profile.schema";

import {useAppDispatch, useAppSelector} from "../../hooks/hooks";
import {fetchMe, updateProfile, uploadPhoto} from "../../redux/slices/user/user.slice";


import lockImage from "../../assets/lock.png";

import {ProfileData, UpdateProfile} from "../../types/user.interface";

import {statusTypes} from "../../types/enums";

import {useStyles} from "./styles";

export const ProfileForm: FC = () => {
  const styles = useStyles();
  const dispatch = useAppDispatch();
  const [image, setImage] = useState("");
  const isFetching = useAppSelector((store) => store.user.status);
  const validationSchema = useMemo(() => {
    return profileSchema;
  }, []);

  const {
    register,
    reset,
    handleSubmit,
    formState,
  } = useForm<ProfileData>(
    {
      mode: "onChange",
      resolver: yupResolver(validationSchema),
    },
  );

  const handleImageChange = async (e: ChangeEvent<HTMLInputElement>) => {
    if(!e.target.files) {
      return toast.error("please choose file");
    }
    const formData = new FormData();
    formData.append("file", e.target.files[0]);
    const {url} = await dispatch(uploadPhoto(formData)).unwrap();
    if(!url){
      return toast.error("Something went wrong");
    }
    setImage(`${url}?${Math.random()}`);
  };

  const handleImageError = () => {
    setImage(defaultAvatar);
  };

  const customSubmit = async (data: ProfileData) => {
    const dataForCompare = {...data, birthday: moment(data.birthday).format("YYYY-MM-DD"), registrationDate: moment(data.registrationDate).format("YYYY-MM-DD")};
    if(_.isEqual(dataForCompare, formState.defaultValues)){
      toast.error("You must change the data for update the profile info");
    } else {
      const rewrittenData: UpdateProfile = _.omit(dataForCompare, ["registrationDate", "countCompleted", "countInProgress", "averageGrade"]);
      const response = await dispatch(updateProfile(rewrittenData)).unwrap();
      toast.success(response);
      const myData = await dispatch(fetchMe()).unwrap();
      reset({
        username: myData.username,
        firstName: myData.firstName,
        lastName: myData.lastName,
        email: myData.email,
        phoneNumber: myData.phoneNumber,
        birthday: myData.birthday,
        registrationDate: myData.registrationDate,
        countCompleted: myData.countCompleted,
        countInProgress: myData.countCompleted,
        averageGrade: myData.averageGrade,
      });
    }
  };

  useLayoutEffect(() => {
    const fetchData = async () => {
      const response = await dispatch(fetchMe()).unwrap();
      reset({
        username: response.username,
        firstName: response.firstName,
        lastName: response.lastName,
        email: response.email,
        phoneNumber: response.phoneNumber,
        birthday: response.birthday,
        registrationDate: response.registrationDate,
        countCompleted: response.countCompleted,
        countInProgress: response.countCompleted,
        averageGrade: response.averageGrade,
      });
      setImage(response.filePhoto);
    };
    fetchData().catch((e) => {
      toast.error(e.message);
    });
  }, []);

  return (
    <Form onSubmit={handleSubmit(customSubmit)}>
      <Box className={styles.rootForm}>
        {(isFetching === statusTypes.LOADING) ? (
          <CircularProgress style={{margin: "auto"}}/>
        ) : (
          <>
            <Box>
              <Box className={styles.infoBlock}>
                <MyInput
                  outsideLabel="Логін"
                  width={310}
                  name="username"
                  disabled
                  register={register}/>
                <MyInput
                  disabled
                  outsideLabel="Ім'я"
                  width={310}
                  name="firstName"
                  register={register}/>
                <MyInput
                  disabled
                  outsideLabel="Прізвище"
                  width={310}
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
                  disabled
                  outsideLabel="Дата народження"
                  width={310}
                  register={register}
                  name="birthday"
                  type="date"/>
              </Box>
              <Box style={{display: "flex", alignItems: "center", justifyContent:"space-between", marginTop: 20}}>
                <Box style={{display: "flex", alignItems: "center"}}>
                  <Typography style={{margin: "auto 20px auto 0"}}>Курсів пройдено/в процесі</Typography>
                  <MyInput
                    name="countCompleted"
                    width={60}
                    register={register}
                    isCenter={true}
                    disabled/>
                  <MyInput
                    name="countInProgress"
                    width={60}
                    register={register}
                    isCenter={true}
                    disabled/>
                </Box>
                <Box style={{display: "flex", alignItems: "center"}}>
                  <Typography style={{margin: "auto 20px auto 0"}}>Середня оцінка за всі курси</Typography>
                  <MyInput
                    disabled
                    width={60}
                    isCenter={true}
                    name="averageGrade"
                    register={register}
                  />
                </Box>
                <Button type="submit">
                  <img src={editIcon}/>
                </Button>
              </Box>
            </Box>
            <Box className={styles.rightBlock}>
              <Typography variant="h6" style={{marginBottom: 10}}>Фото профіля</Typography>
              <Box className={styles.imageWrapper}>
                <label htmlFor="filePhoto">
                  <Avatar
                    sx={{ borderRadius: 0, width: "100%", height: 200 }}
                    srcSet={image}
                    imgProps={{
                      onError: handleImageError,
                    }}
                  />
                </label>
                <input
                  type="file"
                  accept="image/*"
                  onChange={handleImageChange}
                  name="filePhoto"
                  hidden
                  id="filePhoto"
                />
              </Box>
              <Box className={styles.registrationDate}>
                <Typography variant="h6" style={{color: "#808485", fontSize: 18, paddingRight: 20, display: "flex", alignItems: "center"}}>
                  Дата реєстрації
                  <Image width={20} height={20} style={{marginLeft: 10}} src={lockImage}/>
                </Typography>
                <MyInput
                  width={150}
                  register={register} 
                  disabled
                  name="registrationDate"
                  type="date"/>
              </Box>
            </Box>
          </>
        )}
      </Box>
    </Form>
  );
};

