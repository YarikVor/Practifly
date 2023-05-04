import {makeStyles} from "@mui/styles";

export const useStyles = makeStyles({
  h2: {
    "&.MuiTypography-root": {
      textAlign: "center",
      marginBottom: 25,
    },
  },
  loginLink: {
    "&.MuiTypography-root": {
      textAlign: "center",
      marginTop: 20,
    },
  },
  wrapper: {
    display: "flex",
    alignItems: "center",
    flexDirection: "column",
    height: "100%",
  },
});