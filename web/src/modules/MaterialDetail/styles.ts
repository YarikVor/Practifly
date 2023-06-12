import {makeStyles} from "tss-react/mui";

export const useStyles = makeStyles()(() => ({
  root:{
    display: "flex",
    flexDirection: "column",
    backgroundColor: "white",
    borderRadius: 10,
    gap: 10,
    maxWidth: 900,
    width: "100%",
    boxShadow: "0px 0px 10px rgba(0, 0, 0, 0.25)",
    padding: 20,
  },
  nameWrapper:{
    display: "flex",
    alignItems: "center",
    gap: 10,
  },
  descriptionWrapper:{
    display: "flex",
    gap: 16,
  },
  textField:{
    "& .MuiInputLabel-root": {
      "&.Mui-focused": {
        color: "rgba(120, 63, 214, 0.5)",
        fontWeight: "bold",
      },
    },
    "& .MuiOutlinedInput-root": {
      borderRadius: 15,
      color: "#9062F2",
      fontWeight: "bold",
      fontSize: 20,
      background: "#F1F0FA",
      "&:hover fieldset": {
        borderColor: "rgba(120, 63, 214, 0.5)",
      },
      "&.Mui-focused fieldset": {
        borderColor: "rgba(120, 63, 214, 0.5)",
      },
    },
  },

  textFieldModal:{
    marginTop: 20,
    "& .MuiInputLabel-root": {
      "&.Mui-focused": {
        color: "rgba(120, 63, 214, 0.5)",
        fontWeight: "bold",
      },
    },
    "& .MuiOutlinedInput-root": {
      borderRadius: 15,
      background: "#F1F0FA",
      "&:hover fieldset": {
        borderColor: "rgba(120, 63, 214, 0.5)",
      },
      "&.Mui-focused fieldset": {
        borderColor: "rgba(120, 63, 214, 0.5)",
      },
    },
  },
  descriptionFont:{
    "& .MuiOutlinedInput-root": {
      color: "black",
      fontWeight: 300,
    },
  },
  modalBox:{
    position: "absolute",
    top: "50%",
    left: "50%",
    transform: "translate(-50%, -50%)",
    width: 400,
    padding: 25,
    backgroundColor: "white",
    border: "2px solid #000",
  },
  buttonsWrapper:{
    display: "flex",
    justifyContent: "space-between",
    alignSelf: "end",
    width: "100%",
  },
  goToMaterialButton:{
    backgroundColor: "#9062F2",
    color: "white",
    display: "flex",
    alignItems: "center",
    gap: 10,
    boxShadow: "0px 0px 10px rgba(0, 0, 0, 0.25)",
    borderRadius: 5,
    "&.MuiButton-root":{
      "&:hover":{
        backgroundColor: "#9062F2",
      },
    },
  },
}));