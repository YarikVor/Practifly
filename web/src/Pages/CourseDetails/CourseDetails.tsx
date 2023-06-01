import {Box, Typography} from "@mui/material";

import {useParams} from "react-router-dom";

import {ThemeMenu} from "../../modules/ThemeMenu/ThemeMenu";
import {MaterialDetail} from "../../modules/CourseDetail/MaterialDetail";

import {useCourseDetails} from "../../hooks/courses/useCourseDetails";

import {useStyles} from "./styles";

export const CourseDetails = () => {
  const {classes} = useStyles();
  const {id} = useParams();
  const [courseDetails, isCourseDetailsLoading] = useCourseDetails(Number(id));
  return (
    <>
      <Typography variant="h1" style={{textAlign: "center"}}>{courseDetails?.name}</Typography>
      <Box className={classes.root}>
        <ThemeMenu isLoading={isCourseDetailsLoading} courseDetails={courseDetails}/>
        <MaterialDetail />
      </Box>
    </>

  );
};

