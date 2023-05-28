import React from "react";

import CourseMenu from "../../modules/CourseMenu/CourseMenu";
import RubricsSubRubricsMenu from "../../modules/RubricsSubRubricsMenu/RubricsSubRubricsMenu";

import MaterialBlock from "../../modules/MaterialBlock/MaterialBlock";

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
    {
      id: 1,
      name: "Rubric 1",
      subRubrics: [
        { id: "1", name: "SubRubric 1" },
        { id: "1", name: "SubRubric 2" },
      ],
    },
    { id: 2, name: "Rubric 2", subRubrics: [] },
    { id: 3, name: "Rubric 3", subRubrics: [] },
    { id: 4, name: "Rubric 4", subRubrics: [] },
    { id: 5, name: "Rubric 5", subRubrics: [] },
    { id: 6, name: "Rubric 6", subRubrics: [] },
    { id: 7, name: "Rubric 7", subRubrics: [] },
    { id: 8, name: "Rubric 8", subRubrics: [] },
  ];
  const materials =[
    { id: 1, name: "Material 1" },
    { id: 2, name: "Material 2" },
    { id: 3, name: "Material 3" },
    { id: 4, name: "Material 4" },
    { id: 5, name: "Material 5" },
    { id: 6, name: "Material 6" },
    { id: 7, name: "Material 7" },
    { id: 8, name: "Material 8" },
    { id: 9, name: "Material 9" },
  ]


  return (
    <div className={classes.container}>
      <div className={classes.courseMenu}>
        <CourseMenu courses={courses} />
      </div>
      <div className={classes.rubricSubrubricMenu}>
        <RubricsSubRubricsMenu rubrics={rubrics} />
      </div>
      <div className={classes.materialMenu}>
        <MaterialBlock materials={materials}/>

      </div>

    </div>
  );
};

export default RubricsCourse;
