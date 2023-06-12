import {makeStyles} from "@mui/styles";

export const useStyles = makeStyles({
  rootForm:{
    marginTop: "20px",
    boxShadow: "0px 0px 10px rgba(0, 0, 0, 0.25)",
    borderRadius: "10px",
    display: "flex",
    maxWidth: 1500,
    margin: "0 auto",
    minHeight: 450,
    padding: "20px 20px 10px",
  },
  infoBlock: {
    maxWidth: 1100,
    display: "flex",
    flexWrap: "wrap",
    justifyContent: "space-between",
  },
  imageWrapper: {
    padding: 20,
    backgroundColor: "#F1F0FA",
  },
  image:{
    height: 270,
    width: 250,
  },
  dateInput:{
    "&MuiFormControl-root":{
      display: "flex",
      flexDirection: "row",
    },
  },
  rightBlock:{
    padding: "0 20px",
    margin: "0 auto",
  },
  registrationDate:{
    marginTop: 20,
    display: "flex",
    alignItems: "center",
    justifyContent: "space-around",
  },
});