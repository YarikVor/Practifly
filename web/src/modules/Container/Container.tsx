import React from "react";

import {Outlet} from "react-router-dom";

import {Container as MUIContainer} from "@mui/material";

import {Footer} from "../../UIComponents/Footer/Footer";

import {useStyles} from "./styles";

const Container = () => {
  const styles = useStyles();
  return (
    <>
      <MUIContainer className={styles.root} maxWidth={false}>
        <Outlet/>
      </MUIContainer>
      <Footer/>
    </>
  );
};

export default Container;