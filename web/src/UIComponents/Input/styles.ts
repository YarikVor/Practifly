import {makeStyles} from "tss-react/mui";

export const useStyles = makeStyles<{width?: string | number, isBlock: boolean}>()((
  theme, {width, isBlock}) => ({
  wrapper:{
    width: isBlock ? width : "",
  },
  image: {
    marginLeft: 10,
  },
  root: {
    "& .MuiInputLabel-root": {
      "&.Mui-focused": {
        color: "rgba(120, 63, 214, 0.5)",
        fontWeight: "bold",
      },
    },
    "& .MuiOutlinedInput-root": {
      width: !isBlock ? width : "",
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
  labelStyles: {
    display: "flex",
    alignItems: "center",
    color: "#808485",
    "&.MuiFormLabel-root": {
      fontSize: 23,
      marginLeft: 2,
      fontWeight: 500,
    },
  },
  error: {
    color: "red",
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
}));