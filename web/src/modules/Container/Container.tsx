import React, {useEffect} from "react";

import {Outlet} from "react-router-dom";

import {Container as MUIContainer} from "@mui/material";

import "react-toastify/dist/ReactToastify.css";

import {toast, ToastContainer} from "react-toastify";

import {Footer} from "../../UIComponents/Footer/Footer";

import Header from "../../UIComponents/Header/Header";




import {useAppSelector} from "../../hooks/hooks";

import {useStyles} from "./styles";




const Container = () => {
  const styles = useStyles();
  const isError = useAppSelector(store => store.auth.status);
  const message = useAppSelector(store => store.auth.errorMessage);
  useEffect(() => {
    if(isError === "error") {
      toast.error(message, {position: "top-center"});
    }
  },[isError]);

  return (

    <>

      <ToastContainer />
      <Header/>
      <MUIContainer className={styles.root} maxWidth={false}>
        <Outlet/>
      </MUIContainer>

      <Footer/>

    </>
  );
};

export default Container;