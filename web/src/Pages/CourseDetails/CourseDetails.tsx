import {Box, Typography} from "@mui/material";

import {useParams} from "react-router-dom";

import {ThemeMenu} from "../../modules/ThemeMenu/ThemeMenu";
import {MaterialDetail} from "../../modules/MaterialDetail/MaterialDetail";

import {useCourseDetails} from "../../hooks/courses/useCourseDetails";

import {useStyles} from "./styles";

export const CourseDetails = () => {
  const {classes} = useStyles();
  const {id} = useParams(); // За допомогою хука useParams відслідковуємо ідентифікатор з url
  const [courseDetails, isCourseDetailsLoading] = useCourseDetails(Number(id));// за допомогою відслідкованого ідентифікатора, використовуючу хук useCourseDetails отримуємо теми матеріали курсу за айдішкою курсу
  return (
    <>
      <Typography variant="h1" style={{textAlign: "center"}}>{courseDetails?.name}</Typography>
      <Box className={classes.root}>
        <ThemeMenu isLoading={isCourseDetailsLoading} courseDetails={courseDetails}/>{/* Отриманий курс з темами та матеріалами передаємо в компоненту ThemeMenu. */}
        <MaterialDetail /> {/* Компонента з назвою та описом певного матеріалу, який вибере користувач */}
      </Box>
    </>

  );
};

