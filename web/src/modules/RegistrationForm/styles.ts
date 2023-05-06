import {makeStyles} from "@mui/styles";

export const useStyles = makeStyles({
  insideFormWrapper: {
    display: "flex",
    flexWrap: "wrap",
  },
  datePickerLabel: {
    "& $notchedOutline": {
      backgroundColor: "white",
    },
    "&.MuiFormLabel-root": {
      "&.MuiInputLabel-root": {
        "&.Mui-focused": {
          color: "rgba(120, 63, 214, 0.5)",
        },
      },
      position: "relative",
      display: "block",
      margin: "0 0 -10px -10px",
      fontSize: 18,
      fontWeight: 500,
    },
  },
  registrationForm: {
    maxWidth: 800,
    margin: "0 auto",
    display: "flex",
    flexWrap: "wrap",
    background: "linear-gradient(180deg, rgba(128, 132, 133, 0.8) -74.09%, rgba(158, 136, 171, 0) 86%)",
    border: "1px solid rgba(81, 95, 217, 0.5)",
    borderRadius: "15px",
    padding: "35px 35px 35px 45px",
    "& .MuiFormControl-root": {
      width: 350,
      margin: 25,
    },
  },
  submitButton: {
    "&.MuiButtonBase-root": {
      margin: "0 auto",
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
"";