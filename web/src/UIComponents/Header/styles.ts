import { makeStyles } from "@mui/styles";

export const useStyles = makeStyles((theme) => ({
  appBar: {
    "&.MuiPaper-root": {
      backgroundColor: "#FFFFFF",
      height: 70,
    },
  },
  navBar: {
    "&.MuiPaper-root": {
      backgroundColor: "#9062F2",
      height: 40,
    },
  },
  toolbar: {
    display: "flex",
    justifyContent: "center",
  },
  button: {
    cursor: "pointer",
    padding: "8px 16px",
    color: "#FFFFFF",
    margin: "0 8px",
    backgroundColor: "rgba(0, 0, 0, 0.3)",
    border: "none",
    borderRadius: "4px",
    opacity: 1,
    transition: "background-color 0.3s, color 0.3s, opacity 0.3s",
    width: 140,
    "&:hover": {
      backgroundColor: "rgba(248, 248, 255, 0.3)",
    },
    "&.active": {
      backgroundColor: "#FFFFFF",
      color: "rgba(0, 0, 0, 0.9)",
      opacity: 0.9,
    },
  },
  title: {
    flexGrow: 1,
    color: "#9062F2",
    textAlign: "center",
  },
  logo: {
    // Add styles for logo if needed
  },
}));