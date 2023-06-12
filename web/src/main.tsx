import React, {StrictMode} from "react";
import ReactDOM from "react-dom/client";
import {ThemeProvider} from "@mui/material/styles";

import "./main.css";
import {Provider} from "react-redux";

import {theme} from "./theme";
import store from "./redux/store";
import {App} from "./modules/App/App";

ReactDOM.createRoot(document.getElementById("root") as HTMLElement).render(
  <StrictMode>
    <Provider store={store}>
      <ThemeProvider theme={theme}>
        <App />
      </ThemeProvider>
    </Provider>
  </StrictMode>,
);
