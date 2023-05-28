import React, { useState } from "react";
import { AppBar, Toolbar, Typography, ButtonBase } from "@mui/material";

import { useStyles } from "./styles";

const Header: React.FC = () => {
  const classes = useStyles();
  const [activeButton, setActiveButton] = useState("");

  const handleButtonClick = (buttonName: string) => {
    setActiveButton(buttonName);
  };

  return (
    <>
      <AppBar position="static" className={classes.appBar}>
        <Toolbar className={classes.toolbar}>
          <Typography variant="h6" className={classes.title}>
                        Current Page Title
          </Typography>
          {/* <div className={classes.avatar}> Add user avatar here </div> */}
          <div className={classes.logo}>{/* Add logo here */}</div>
        </Toolbar>
      </AppBar>
      <AppBar position="static" className={classes.navBar}>
        <Toolbar className={classes.toolbar}>
          <ButtonBase
            className={`${classes.button} ${
              activeButton === "Button 1" ? "active" : ""
            }`}
            onClick={() => handleButtonClick("Button 1")}
          >
                        Button 1
          </ButtonBase>
          <ButtonBase
            className={`${classes.button} ${
              activeButton === "Button 2" ? "active" : ""
            }`}
            onClick={() => handleButtonClick("Button 2")}
          >
                        Button 2
          </ButtonBase>
          <ButtonBase
            className={`${classes.button} ${
              activeButton === "Button 3" ? "active" : ""
            }`}
            onClick={() => handleButtonClick("Button 3")}
          >
                        Button 3
          </ButtonBase>
          <ButtonBase
            className={`${classes.button} ${
              activeButton === "Button 4" ? "active" : ""
            }`}
            onClick={() => handleButtonClick("Button 4")}
          >
                        Button 4
          </ButtonBase>
          <ButtonBase
            className={`${classes.button} ${
              activeButton === "Button 5" ? "active" : ""
            }`}
            onClick={() => handleButtonClick("Button 5")}
          >
                        Button 5
          </ButtonBase>
          <ButtonBase
            className={`${classes.button} ${
              activeButton === "Button 6" ? "active" : ""
            }`}
            onClick={() => handleButtonClick("Button 6")}
          >
                        Button 6
          </ButtonBase>
          <ButtonBase
            className={`${classes.button} ${
              activeButton === "Button 7" ? "active" : ""
            }`}
            onClick={() => handleButtonClick("Button 7")}
          >
                        Button 7
          </ButtonBase>
        </Toolbar>
      </AppBar>
    </>
  );
};

export default Header;
