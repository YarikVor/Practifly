import {Box, Button, Modal, TextField, Typography} from "@mui/material";

import React, {useEffect, useState} from "react";


import {useAppDispatch, useAppSelector} from "../../hooks/hooks";

import booksButton from "../../assets/booksButton.png";
import skrepka from "../../assets/skrepka.png";
import checked2 from "../../assets/cheked2.png";


import {MaterialStatus} from "../../types/course.interface";

import {changeMaterialStatus} from "../../redux/slices/course/course.slice";

import {useStyles} from "./styles";

export const MaterialDetail = () => {
  const currentMaterials = useAppSelector((state) => state.course.currentMaterials.data);//Дістаємо попередньо записаний матеріал з сховища та звписуємо його дані в інпути
  const {classes} = useStyles();
  const dispatch = useAppDispatch();
  const DEFAULT_INPUT_WIDTH = 60;//Встановлюємо стартове значення ширина інпута для оцінки
  const [url, setUrl] = useState("");
  const [statusData, setStatusData] = useState<MaterialStatus>({
    id: 0,
    resultUrl: "",
    isCompleted: false,
  });

  const [inputWidth, setInputWidth] = useState(DEFAULT_INPUT_WIDTH);//Створюємо стан ширини інпута оцінки

  useEffect(() => {//За допомогою хука ЮЗЕФЕКТ дивимось за оцінкою матеріалу - якщо вона більше 99, то встановлюємо ширину інпуту яка буде доцільно виглядати і так для решти оцінок
    if (!currentMaterials?.grade) {
      setInputWidth(DEFAULT_INPUT_WIDTH);
    } else {
      if (currentMaterials.grade > 99) {
        setInputWidth(85);
      } else {
        setInputWidth(75);
      }
    }
  }, [currentMaterials?.grade]);
  const handleSubmitData = async () =>{
    if(currentMaterials) {
      setStatusData({id: currentMaterials.id, resultUrl: url, isCompleted: !currentMaterials.isCompleted});
    }
    console.log(statusData);
    const data = await dispatch(changeMaterialStatus(statusData));
  };

  const [open, setOpen] = React.useState(false);
  const handleOpen = () => setOpen(true);
  const handleClose = () => setOpen(false);

  return (
    <Box className={classes.root}>
            <Typography>Назва</Typography>
      <Box className={classes.nameWrapper}>
        <TextField
          disabled
          fullWidth
          className={classes.textField}
          value={currentMaterials?.name}
          sx={{
            "& .MuiInputBase-input.Mui-disabled": {
              WebkitTextFillColor: "#9062F2",
            },
          }}
        />
      </Box>
      <Typography>Примітка</Typography>
      <Box className={classes.descriptionWrapper}>
        <TextField
          disabled
          fullWidth
          className={`${classes.textField} ${classes.descriptionFont}`}
          multiline
          rows={15}
          sx={{
            "& .MuiInputBase-input.Mui-disabled": {
              WebkitTextFillColor: "black",
            },
          }}
          value={currentMaterials?.note}/>
      </Box>
      <Box className={classes.buttonsWrapper}>
        <Button
          className={classes.goToMaterialButton}>
          <img src={booksButton}/>
                    Перейти до матеріалів
        </Button>
        <Box style={{display: "flex", alignItems: "center"}}>
          <TextField
            value={`${(!currentMaterials?.grade ? 0 : currentMaterials.grade)}/100`}
            disabled
            sx={{
              "& .MuiInputBase-input.Mui-disabled": {
                WebkitTextFillColor: `${currentMaterials?.grade && (currentMaterials.grade >= 50 ? "green" : "red")}`,
              },
            }}
            inputProps={{
              style: {
                width: `${inputWidth}px`,
                color: "green",
                height: 10,
                textAlign: "end",
              },
            }}
            className={classes.textField}/>
          <Button onClick={handleOpen}>
            <img src={skrepka}/>
          </Button>
          <Modal
            open={open}
            onClose={handleClose}
            aria-labelledby="modal-modal-title"
            aria-describedby="modal-modal-description"
          >
            <Box className={classes.modalBox}>
              <Typography id="modal-modal-title" variant="h6" component="h2">
                        Будь ласка, вставте посилання на виконану роботу
              </Typography>
              <Typography id="modal-modal-description" sx={{ mt: 2 }}>
                        Після вставлення посилання на виконану роботу, натисніть кнопку ✔
              </Typography>
              <TextField
                label="Посилання на виконану роботу"
                fullWidth
                value={url}
                onChange={(e) => setUrl(e.target.value)}
                className={classes.textFieldModal}/>
            </Box>
          </Modal>
          <Button onClick={handleSubmitData}>
            <img
              style={{
                borderRadius: 10,
                width: 45,
                height: 45,
                boxShadow: "0px 0px 10px rgba(0, 0, 0, 0.25)",
              }}
              src={checked2}/>
          </Button>
        </Box>
      </Box>
    </Box>
  );
};
