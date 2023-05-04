import {makeStyles} from "@mui/styles";

export const useStyles = makeStyles({
  footer: {
    display: "flex",
    alignItems: "center",
    justifyContent: "space-around",
    backgroundColor: "#2D315B",
    height: 60,
    "& .MuiTypography-root": {
      color: "#FFFFFF",
      opacity: "80%",
    },
  },
  aboutUsLink: {
    "&.MuiLink-root": {
      color: "#FFFFFF",
      opacity: "80%",
    },
  },
});