import {makeStyles} from "@mui/styles";

export const useStyles = makeStyles({
  loginForm: {
    maxWidth: 580,
    margin: "0 auto",
    display: "flex",
    flexWrap: "wrap",
    flexDirection: "column",
    background: "linear-gradient(180deg, rgba(128, 132, 133, 0.8) -74.09%, rgba(158, 136, 171, 0) 86%)",
    border: "1px solid rgba(81, 95, 217, 0.5)",
    borderRadius: "15px",
    padding: 35,
    "& .MuiFormControl-root": {
      width: 580,
      margin: "20px 0 0 0",
    },
  },
  buttonWrapper: {
    display: "flex",
    alignItems: "center",
    justifyContent: "space-between",
    maxWidth: 580,
    marginTop: 20,
  },
  submitButton: {
    "&.MuiButtonBase-root": {
      backgroundColor: "#6C73BB",
      borderRadius: 15,
      width: 200,
      height: 50,
      "&:hover": {
        backgroundColor: "#6C73BB",
      },
    },
  },
});