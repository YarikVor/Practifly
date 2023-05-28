import React, { useState } from "react";

import { Box, Typography, List, ListItem } from "@mui/material";

import { useStyles } from "./styles";

interface Course {
    id: number;
    name: string;
}

interface CourseMenuProps { 
    courses: Course[];
}

const CourseMenu: React.FC<CourseMenuProps> = ({ courses }) => {
  const classes = useStyles();
  const [selectedCourse, setSelectedCourse] = useState<Course | null>(null);

  const handleCourseClick = (course: Course) => {
    setSelectedCourse(course);
  };

  return (
    <Box className={classes.container}>
      <Box className={classes.headerWrapper}>
        <Typography className={classes.headerText}>Меню курсів</Typography>
      </Box>
      <List className={classes.list}>
        {courses.map((course) => (
          <ListItem
            key={course.id}
            className={`${classes.courseItem} ${
              selectedCourse?.id === course.id ? classes.selectedCourse : ""
            }`}
            onClick={() => handleCourseClick(course)}
          >
            {course.name}
          </ListItem>
        ))}
      </List>
    </Box>
  );
};

export default CourseMenu;
