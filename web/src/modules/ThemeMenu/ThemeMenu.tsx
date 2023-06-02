import {
  Box, Checkbox, CircularProgress,
  Collapse,
  List,
  ListItemButton,
  ListItemText,
  ListSubheader, Paper,
  Typography,
} from "@mui/material";

import React, {FC, useState} from "react";


import {ExpandLess, ExpandMore} from "@mui/icons-material";

import {CourseDetails} from "../../types/course.interface";

import {useAppDispatch} from "../../hooks/hooks";
import {getCurrentMaterials} from "../../redux/slices/course/course.slice";

import {useStyles} from "./styles";

interface ThemeMenuProps {
  courseDetails: CourseDetails | null;
  isLoading: boolean;
}

interface OpenState {
  [themeName: string]: boolean;
}

export const ThemeMenu:FC<ThemeMenuProps> = ({ 
  courseDetails,
  isLoading,
}) => {
  const {classes} = useStyles();
  const dispatch = useAppDispatch();

  const [open, setOpen] = useState<OpenState>({});
  const handleClick = (themeId: number) => {
    setOpen((prevState) => ({
      ...prevState,
      [themeId]: !prevState[themeId],
    }));
  };

  return (
    <Box className={classes.root}>
      {isLoading ? (
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
                <Typography style={{backgroundColor: "#F1F0FA", textAlign: "center", borderRadius: "10px 10px 0 0"}} variant="h5">Меню тем</Typography>
              </ListSubheader>
            }
          >
            {courseDetails?.themes.map((theme) => (
              <React.Fragment key={theme.name + Math.random()}>
                <ListItemButton style={{margin: 2, backgroundColor: "#F1F0FA"}} onClick={() => handleClick(theme.id)}>
                  <Checkbox
                    checked={theme.isCompleted}
                    className={classes.disabledCheckbox}
                  />
                  <ListItemText primary={theme.name} />
                  {theme.materials.length !== 0 && (open[theme.id] ? <ExpandLess /> : <ExpandMore />)}
                </ListItemButton>
                {theme.materials.length !== 0 && (
                  <Collapse in={open[theme.id]} timeout="auto" unmountOnExit>
                    {theme.materials.map((material) => (
                      <List
                        component="div"
                        disablePadding
                        key={Math.random()}
                      >
                        <ListItemButton onClick={() => dispatch(getCurrentMaterials(material))} style={{margin: 2, paddingLeft: 40, backgroundColor: "#F1F0FA"}}>
                          <Checkbox
                            disabled
                            checked={material.isCompleted}
                            className={classes.disabledCheckbox}
                          />
                          <ListItemText primary={material.name} />
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

