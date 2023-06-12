import {createBrowserRouter} from "react-router-dom";

import {Box} from "@mui/material";

import Registration from "../Pages/Registration/Registration";

import Container from "../modules/Container/Container";
import Login from "../Pages/Login/Login";
import {Profile} from "../Pages/Profile/Profile";
import {CourseDetails} from "../Pages/CourseDetails/CourseDetails";
import {MyCourses} from "../Pages/MyCourses/MyCourses";
import RubricsCourse from "../Pages/RubricsCourse/RubricsCourse";
import CourseMaterial from "../Pages/CourseMaterial/CourseMaterial";

export const router = (isAuth: boolean) => {
  return createBrowserRouter([
    {
      path: "/",
      element: <Container/>,
      children: [
        {
          path: "/registration",
          element: <Registration/>,
        },

        {
          path: "/login",
          element: <Login/>,
        },
        {
          path: "/home",
          element: <Box>Hello Kitty!!!</Box>,
        },

        {
          path: "/myCourses",
          element: isAuth ? <MyCourses /> : <Login/>,
        },
        {
          path: "/profile",
          element: isAuth ? <Profile /> : <Login/>,
        },
        {
          path: "/courseDetails/:id",
          element: isAuth ? <CourseDetails /> : <Login/>,
        },
        {
          path: "/rubricsCourse",
          element: <RubricsCourse/>,
        },
        {
          path: "/courseMaterial",
          element: <CourseMaterial/>,
        },
      ],
    },
  ]);
};