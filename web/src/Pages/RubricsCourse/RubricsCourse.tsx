import React from "react";

import CourseMenu from "../../modules/CourseMenu/CourseMenu";
import RubricsMenu from "../../modules/RubricsMenu/RubricsMenu";

import { useStyles } from "./styles";


const RubricsCourse: React.FC = () => {
  const classes = useStyles();

  const courses = [
    { id: 1, name: "Course 1" },
    { id: 2, name: "Course 2" },
    { id: 3, name: "Course 3" },
    { id: 4, name: "Course 4" },
    { id: 5, name: "Course 5" },
    { id: 6, name: "Course 6" },
    { id: 7, name: "Course 7" },
    { id: 8, name: "Course 8" },
    { id: 9, name: "Course 9" },
  ];

  const rubrics = [
    { id: 1, name: "Rubric 1" },
    { id: 2, name: "Rubric 2" },
    { id: 3, name: "Rubric 3" },
    { id: 4, name: "Rubric 4" },
    { id: 5, name: "Rubric 5" },
    { id: 6, name: "Rubric 6" },
    { id: 7, name: "Rubric 7" },
    { id: 8, name: "Rubric 8" },

  ];

  return (
    <div className={classes.container}>


      <div className={classes.courseMenu}>
        <CourseMenu courses={courses} />
      </div>
      <div className={classes.rubricsMenu}>
        <RubricsMenu rubrics={rubrics} />
      </div>
    </div>
  );
};

export default RubricsCourse;
