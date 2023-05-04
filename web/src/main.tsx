import React, {StrictMode} from "react";
import ReactDOM from "react-dom/client";
import {RouterProvider} from "react-router-dom";
import {ThemeProvider} from "@mui/material/styles";

import "./main.css";
import {Provider} from "react-redux";

import {router} from "./Router/router";
import {theme} from "./theme";
import store from "./redux/store";

ReactDOM.createRoot(document.getElementById("root") as HTMLElement).render(
  <StrictMode>
    <Provider store={store}>
      <ThemeProvider theme={theme}>
        <RouterProvider router={router}/>
      </ThemeProvider>
    </Provider>
  </StrictMode>,
);
