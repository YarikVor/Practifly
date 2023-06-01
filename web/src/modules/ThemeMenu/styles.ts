import {makeStyles} from "tss-react/mui";

export const useStyles = makeStyles()(() => ({
  root: {
    minWidth: 250,
  },
  listSubheader: {
    textAlign: "center",
    fontSize: 22,
  },

  paperRoot: {
    maxHeight: 650,
    overflow: "auto",
    boxShadow: "0px 0px 9px 1px rgba(173,173,173,1)",
    borderRadius: 10,
  },
  disabledCheckbox:{
    "&.Mui-checked":{
      color:"green",
      "&.Mui-disabled":{
        color: "red",
      },
    },
  },
}
));