import { createStyles, makeStyles } from "@mui/styles";

export const useStyles = makeStyles(() =>
  createStyles({
    container: {
      maxHeight: "calc(90vh - 40px)",
      width: "100%",
      left: "20px",
      top: "20px",
      backgroundColor: "#F1F0FA",
      padding: "20px",
      overflowY: "scroll",
      boxShadow: "0 2px 4px rgba(0, 0, 0, 0.1)",
      borderRadius: "10px",
      marginLeft: "50px",
      marginTop: "20px",
    },
    menuHeader: {
      marginBottom: "20px",
      backgroundColor: "#F1F0FA",
      display: "flex",
      justifyContent: "center",
      alignItems: "center",
      borderBottom: "1px solid #CCCCCC",
      borderTopLeftRadius: "10px",
      borderTopRightRadius: "10px",
    },
    menuHeaderText: {
      fontSize: "18px",
      fontWeight: "bold",
    },
    list: {
      listStyle: "none",
      padding: 0,
      margin: 0,
    },
    rubricItem: {
      backgroundColor: "#F1F0FA",
      padding: "8px",
      marginBottom: "4px",
      cursor: "pointer",
      borderRadius: "10px",
      border: "1px solid #FFFFFF",
    },
    selectedRubric: {
      backgroundColor: "#9062F2",
      color: "#FFFFFF",
    },
    buttons: {
      marginTop: "10px",
      display: "flex",
      justifyContent: "space-around ",
    },
    divider: {
      border: "none",
      borderTop: "1px solid #FFFFFF",
      margin: "8px 0px",
    },
    button: {
      marginLeft: "10px",
      padding: "8px 12px",
      color: "#9062F2",
      borderRadius: "5px",
      cursor: "pointer",
    },
  })
);
