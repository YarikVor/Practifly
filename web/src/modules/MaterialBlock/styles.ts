// styles.ts
import { makeStyles } from "@mui/styles";

export const useStyles = makeStyles(() => ({
  container: {
    maxHeight: "calc(70vh - 40px)",
    width: "100%",
    left: "80px",
    top: "20px",
    backgroundColor: "#F1F0FA",
    padding: "20px",
    overflowY: "scroll",
    boxShadow: "0 2px 4px rgba(0, 0, 0, 0.1)",
    borderRadius: "10px",
    marginTop: "20px",
  },
  headerWrapper: {
    marginBottom: "20px",
    backgroundColor: "#F1F0FA",
    display: "flex",
    justifyContent: "center",
    alignItems: "center",
    borderBottom: "1px solid #CCCCCC",
    borderTopLeftRadius: "10px",
    borderTopRightRadius: "10px",
  },
  headerText: {
    fontSize: "18px",
    fontWeight: "bold",
  },
  list: {
    padding: "0",
    margin: "0",
    listStyle: "none",
  },
  materialItem: {
    padding: "10px",
    marginBottom: "5px",
    cursor: "pointer",
    borderBottom: "1px solid #FFFFFF",
  },
  selectedMaterial: {
    backgroundColor: "#9062F2",
    color: "#FFFFFF",
  }, 
}));

