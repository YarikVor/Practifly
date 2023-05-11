import {Box} from "@mui/material";

import {ThemeMenu} from "../../modules/ThemeMenu/ThemeMenu";
import {MaterialDetail} from "../../modules/CourseDetail/MaterialDetail";

import {useStyles} from "./styles";

export const CourseDetails = () => {
  const {classes} = useStyles();
  return (
    <Box className={classes.root}>
      <ThemeMenu />
      <MaterialDetail />
    </Box>
  );
};

