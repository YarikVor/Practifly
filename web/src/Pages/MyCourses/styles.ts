import {makeStyles} from "tss-react/mui";

export const useStyles = makeStyles()(() => ({
  mainWrapper: {
    maxWidth: 650,
    padding: 10,
    backgroundColor: "#F1F0FA",
    boxShadow: "0px 0px 10px rgba(0, 0, 0, 0.25)",
    borderRadius: 10,
  },
  main:{
    maxWidth: 650,
    background: "#FFFFFF",
    border: "0.5px solid #9062F2",
    borderRadius: 10,
    padding: "20px 20px 10px",
  },
  nameWrapper: {
    display: "flex",
    width: "100%",
    alignItems:"center",
    gap: 10,
    justifyContent: "end",
  },
  textField:{
    "& .MuiInputLabel-root": {
      "&.Mui-focused": {
        color: "rgba(120, 63, 214, 0.5)",
        fontWeight: "bold",
      },
    },
    "& .MuiOutlinedInput-root": {
      borderRadius: 15,
      color: "#9062F2",
      fontWeight: "bold",
      fontSize: 20,
      background: "#F1F0FA",
      "&:hover fieldset": {
        borderColor: "rgba(120, 63, 214, 0.5)",
      },
      "&.Mui-focused fieldset": {
        borderColor: "rgba(120, 63, 214, 0.5)",
      },
    },
  },
  textFieldWidth: {
    width: "85%",
  },
  iconWrapper:{
    padding: 10,
    backgroundColor: "#F1F0FA",
    width: 35,
    height: 20,
    borderRadius: 5,
  },
  icon: {
    width: "100%",
    height: "100%",
  },
  endAdornment:{
    display: "flex",
    alignItems: "center",
    gap: 10,
  },
  courseProgress:{
    color:"green",
    fontWeight: "bold",
  },
  endPoint:{
    width: 10,
    height: 10,
    borderRadius: 999,
    backgroundColor: "green",
  },
  descriptionLabel:{
    "&.MuiFormLabel-root": {
      fontWeight: 500,
      fontSize: 20,
      color: "#808485",
    },
  },
  gradeWrapper:{
    display: "flex",
    justifyContent: "space-between",
  },
  descriptionInput: {
    "& .MuiInputLabel-root": {
      "&.Mui-focused": {
        color: "rgba(120, 63, 214, 0.5)",
        fontWeight: "bold",
      },
    },
    "& .MuiOutlinedInput-root": {
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
  descriptionWrapper:{
    marginBottom: 15,
  },
  gradeAverageTypography:{
    color: "#808485",
    fontWeight: 500,
    fontSize: 20,
  },
  gradeAverageWrapper:{
    display: "flex",
    alignItems: "center",
    gap: 10,
  },
  gradeIconWrapper:{
    padding: "5px 15px 5px",
    backgroundColor: "#F1F0FA",
    borderRadius: 10,
  },
  button:{
    backgroundColor: "#F1F0FA",
    borderRadius: 10,
    color: "#9062F2",
    fontSize: 20,
    fontWeight: 600,
    width: 150,
  },
}
));