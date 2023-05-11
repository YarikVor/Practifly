import {createBrowserRouter} from "react-router-dom";

import {Box} from "@mui/material";

import Registration from "../Pages/Registration/Registration";

import Container from "../modules/Container/Container";
import Login from "../Pages/Login/Login";
import {Profile} from "../Pages/Profile/Profile";
import {CourseDetails} from "../Pages/CourseDetails/CourseDetails";

export const router = createBrowserRouter([
  {
    path: "/",
    element: <Container/>,
    children: [
      {
        path: "/home",
        element: <Box>Hello Kitty!!!</Box>,
      },
      {
        path: "/registration",
        element: <Registration/>,
      },
      {
        path: "/login",
        element: <Login/>,
      },
      {
        path: "/profile",
        element: <Profile/>,
      },
      {
        path: "/courseDetails",
        element: <CourseDetails/>,
      },
    ],
  },
]);