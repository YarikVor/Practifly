import {StrictMode} from "react";
import ReactDOM from "react-dom/client";
import {RouterProvider} from "react-router-dom";
import {ThemeProvider} from "@mui/material/styles";

import "./main.css";
import {router} from "./Router/router";
import {theme} from "./theme";

ReactDOM.createRoot(document.getElementById("root") as HTMLElement).render(
  <StrictMode>
    <ThemeProvider theme={theme}>
      <RouterProvider router={router}/>
    </ThemeProvider>
  </StrictMode>,
);
