import {Box, Button, CircularProgress, InputAdornment, InputLabel, TextField, Typography} from "@mui/material";

import React, {Fragment} from "react";


import {useNavigate} from "react-router-dom";

import {useMyCourses} from "../../hooks/courses/useMyCourses";
import ukraine from "../../assets/ukraine.png";
import cells from "../../assets/cells.png";

import {useStyles} from "./styles";

export const MyCourses = () => {
  const [myCourses, isCoursesLoading] = useMyCourses();//викликаємо хук, який відправляє запит на сервер на отримання курсів, до яких причетний користувач
  const {classes} = useStyles();
  const navigate = useNavigate();
  return (
    <Box className={classes.myCourseContainer}>
      {isCoursesLoading ? (//Якщо курси завантажуються - показуємо лоудер, інакше - перебираємо всі курси за допомогою методу масива(циклу) мар та відображаємо кожний
        <CircularProgress />
      ) : (
        myCourses.map((course) => {
          return (
            <Fragment key={course.courseId}>
              <Box className={classes.mainWrapper}>
                <Box className={classes.main}>
                  <Box className={classes.nameWrapper}>
                    <Box className={classes.iconWrapper}>
                      <img className={classes.icon} src={ukraine}/>
                    </Box>
                    <TextField
                      className={`${classes.textField} ${classes.textFieldWidth}`}
                      defaultValue={course.name}
                      InputProps={{
                        startAdornment: <InputAdornment position="start">Назва:</InputAdornment>,
                        endAdornment:
                            <Box className={classes.endAdornment}>
                              <Typography className={classes.courseProgress}>{course.countProgress}/{course.countThemes}</Typography>
                              <Typography className={classes.endPoint}/>
                            </Box>,
                      }}/>
                  </Box>
                  <Box className={classes.descriptionWrapper}>
                    <InputLabel className={classes.descriptionLabel} shrink htmlFor={course.name}>
                      Опис
                    </InputLabel>
                    <TextField
                      fullWidth
                      id={course.name}
                      className={classes.descriptionInput}
                      defaultValue={course.description}
                      multiline
                    />
                  </Box>
                  <Box></Box>
                  <Box className={classes.gradeWrapper}>
                    <Box className={classes.gradeAverageWrapper}>
                      <Typography className={classes.gradeAverageTypography}>{course.isCompleted ? "Оцінка за курс" : "Середня оцінка за курс"}</Typography>
                      <TextField
                        defaultValue={course.gradeAverage}
                        inputProps={{
                          style: {
                            width: 45,
                            height: 15,
                            color: "green",
                            textAlign: "center",
                          },
                        }}
                        className={classes.textField} />
                      {course.isCompleted &&
                          <Box className={classes.gradeIconWrapper}>
                            <img src={cells}/> 
                          </Box>
                      }
                    </Box>
                    <Button onClick={() => navigate(`../courseDetails/${course.courseId}`)} className={classes.button}>Переглянути курс</Button>
                  </Box>
                </Box>
              </Box>
            </Fragment>
          );
        })
      )
      }
    </Box>
  );
};