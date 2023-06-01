import React, {useEffect} from "react";

import {Outlet} from "react-router-dom";

import {Container as MUIContainer} from "@mui/material";

import "react-toastify/dist/ReactToastify.css";

import {toast, ToastContainer} from "react-toastify";

import {Footer} from "../../UIComponents/Footer/Footer";

import {useAppSelector} from "../../hooks/hooks";

import {useStyles} from "./styles";




const Container = () => {
  const styles = useStyles();
  const isError = useAppSelector(store => store.user.status);
  const message = useAppSelector(store => store.user.errorMessage);
  useEffect(() => {
    if(isError === "error") {
      toast.error(message, {position: "top-center"});
    }
  },[isError]);

  return (
    <>
      <ToastContainer position="top-center"/>
      <MUIContainer className={styles.root} maxWidth={false}>
        <Outlet/>
      </MUIContainer>
      <Footer/>
    </>
  );
};

export default Container;