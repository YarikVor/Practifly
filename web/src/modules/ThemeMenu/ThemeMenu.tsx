import {
  Box, Checkbox, CircularProgress,
  Collapse,
  List,
  ListItemButton,
  ListItemText,
  ListSubheader, Paper, TextField,
  Typography,
} from "@mui/material";

import React, {FC, useState} from "react";

import {ExpandLess, ExpandMore} from "@mui/icons-material";

import {CourseDetails} from "../../types/course.interface";

import {useAppDispatch} from "../../hooks/hooks";
import {getCurrentMaterials} from "../../redux/slices/course/course.slice";

import {CustomCheckedIcon, CustomUncheckedIcon} from "../../asd";

import {useStyles} from "./styles";

interface ThemeMenuProps { // Описуємо інтерфейсов пропсів, що приходять в компоненту
  courseDetails: CourseDetails | null;
  isLoading: boolean;
}

interface OpenState {//Описуємо інтерфейс динамічного стану, для поджальшого використання
  [themeName: string]: boolean;
}

export const ThemeMenu:FC<ThemeMenuProps> = ({ //Дестрикторизуємо пропси, що приходять в компоненту
  courseDetails,
  isLoading,
}) => {
  const {classes} = useStyles();
  const dispatch = useAppDispatch();
  const [open, setOpen] = useState<OpenState>({});//Ініціалізуємо стан для нашого меню, в залежності від якого будемо відкривати чи закривати меню тем.
  const handleClick = (themeId: number) => {//функція для того, щоб динамічно вказати стану його ключі та значення, які відповідають за відкрите те чи інше меню
    setOpen((prevState) => ({
      ...prevState,//Розгортаємо попередній стан
      [themeId]: !prevState[themeId],// Та добавляємо до нього за допомогою оператора spread ще одне значення, якщо добавляється якась тема.
    }));
  };

  return (
    <Box className={classes.root}>
      {isLoading  ? (
        <CircularProgress />
      ) : (
        <Paper className={classes.paperRoot} sx={{
          "&::-webkit-scrollbar": {
            display: "block",
            width: 5,
            overflow: "auto",
          },
          "&::-webkit-scrollbar-track": {
            borderRadius: 20,
            backgroundColor: "#808485",
            border: "4px double #FCFCFC",
          },
          "&::-webkit-scrollbar-track:hover": {
            backgroundColor: "#808485",
          },

          "&::-webkit-scrollbar-track:active": {
            backgroundColor: "#808485",
          },

          "&::-webkit-scrollbar-thumb": {
            backgroundColor: "#9062F2",
            borderRadius: 50,
            height: 120,
          },

          "&::-webkit-scrollbar-thumb:hover": {
            backgroundColor: "#9062F2",
          },

          "&::-webkit-scrollbar-thumb:active": {
            backgroundColor: "#9062F2",
          },
        }}>
          <List
            key={Math.random()}
            sx={{ width: "100%", maxWidth: 360, backgroundColor: "white"}}
            component="nav"
            aria-labelledby="nested-list-subheader"
            subheader={
              <ListSubheader style={{padding: "2px 2px 0 2px"}} component="div" id="nested-list-subheader">
                <Typography style={{backgroundColor: "#F1F0FA", height: 60, display: "flex",alignItems:"center", justifyContent:"center", borderRadius: "10px 10px 0 0"}} variant="h5">Меню тем</Typography>
              </ListSubheader>
            }
          >
            {courseDetails?.themes && courseDetails?.themes.map((theme) => (//Якщо дані з серверу прийшли, то ми ітеруємо масив тем, та відмальовуємо їх в меню
              <React.Fragment key={theme.name + Math.random()}>
                <ListItemButton style={{margin: 2, backgroundColor: "#F1F0FA"}} onClick={() => handleClick(theme.id)}>{/* При натисканні на одну з тем передаємо до нашого динамічного стану нове значення з цією темою для подальшого відслідковування чи вона відкрита */}
                  <Checkbox
                    checked={theme.isCompleted}
                    icon={<CustomUncheckedIcon />}
                    checkedIcon={<CustomCheckedIcon/>}
                  />
                  <ListItemText primary={theme.name} />
                  {theme.materials.length !== 0 && (open[theme.id] ? <ExpandLess /> : <ExpandMore />)}{/* Якщо меню теми закрите, то відмальовуємо картинку-стрілку, яка показує чи відкрите або закрите меню теми*/}
                </ListItemButton>
                {theme.materials.length !== 0 && ( //Якщо у теми присутні матеріали, то ми в подальшому будемо їх відмальовувати
                  <Collapse in={open[theme.id]} timeout="auto" unmountOnExit>
                    {theme.materials.map((material) => ( //Ітеруємо всі матеріали, які є в темі та відмальовуємо їх.
                      <List
                        component="div"
                        disablePadding
                        key={Math.random()}
                      >
                        <ListItemButton onClick={() => dispatch(getCurrentMaterials(material))} style={{gap: 10, margin:"0 2px", paddingLeft: 40, backgroundColor: "#F1F0FA"}}>{/* При натисканні на один з матеріалів теми - записуємо її дані в Redux сховище після чого записуємо ці дані в наші інпути */}
                          <Checkbox
                            checked={material.isCompleted}
                            icon={<CustomUncheckedIcon />}
                            checkedIcon={<CustomCheckedIcon/>}
                          />
                          <ListItemText primary={material.name} />
                          <TextField
                            defaultValue={material.grade}
                            disabled
                            sx={{
                              "& .MuiInputBase-input.Mui-disabled": {
                                WebkitTextFillColor: `${material.grade >= 50 ? "green" : "red"}`,
                              },
                            }}
                            inputProps={{
                              style: {
                                width: 45,
                                height: 15,
                                color: "green",
                                textAlign: "center",
                              },
                            }}
                            className={classes.textField} />
                        </ListItemButton>
                      </List>
                    ))}
                  </Collapse>
                )}
              </React.Fragment>
            ))}
          </List>
        </Paper>
      )}
    </Box>
  );
};

