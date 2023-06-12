import {makeStyles} from "tss-react/mui";

export const useStyles = makeStyles()(() => ({
  root: {
    minWidth: 250,
  },
  listSubheader: {
    textAlign: "center",
    fontSize: 22,
  },
  textField:{
    "& fieldset":{
      border: "none",
    },
    "& .MuiInputLabel-root": {
      "&.Mui-focused": {
        color: "rgba(120, 63, 214, 0.5)",
        fontWeight: "bold",
      },
    },
    "& .MuiOutlinedInput-root": {
      borderRadius: 15,
      color: "green",
      fontWeight: "bold",
      fontSize: 20,
      background: "white",
    },
  },

  paperRoot: {
    maxHeight: 650,
    overflow: "auto",
    boxShadow: "0px 0px 9px 1px rgba(173,173,173,1)",
    borderRadius: 10,
  },
  checkbox:{
    "&.Mui-checked":{
      color:"green",
      "&.Mui-disabled":{
        color: "green",
      },
    },
  },
}
));