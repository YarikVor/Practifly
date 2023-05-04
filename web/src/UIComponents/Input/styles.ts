import {makeStyles} from "@mui/styles";

export const useStyles = makeStyles({
  labelStyles: {
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
  inputStyles: {
    "&.MuiInputBase-root": {
      "&.Mui-focused fieldset": {
        borderColor: "rgba(120, 63, 214, 0.5)",
      },
      "&:hover": {
        borderColor: "#F1F0FA",
      },
      "& .MuiOutlinedInput-input": {
        color: "black",
        position: "relative",
        zIndex: 9999,
      },
      "& .MuiOutlinedInput-notchedOutline": {
        borderColor: "#F1F0FA",
        borderRadius: 15,
        backgroundColor: "white",
      },
      "&.MuiOutlinedInput-root": {
        "&:hover": {
          "& .MuiOutlinedInput-notchedOutline": {
            border: "2px solid rgba(120, 63, 214, 0.5)",
          },
        },
      },
    },
  },
  notchedOutline: {},
});