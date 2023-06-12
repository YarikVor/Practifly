import {
  Box,
} from "@mui/material";

import {Link} from "react-router-dom";

import {AccountMenu} from "../Tooltip/AccountMenu";
import {useAppSelector} from "../../hooks/hooks";

export const Header = () => {
  const isAuth = useAppSelector(state => state.user.profileData);
  return (
    <Box style={{display: "flex", alignItems:"center", justifyContent: "space-around", padding: "30px 0 40px 0"}}>
      <Box>МІСЦЕ ДЛЯ ЛОГО</Box>
      <Box>
        <h1>КОРИСТУВАЧ</h1>
      </Box>
      {!isAuth ? (
        <Link style={{textDecorationColor: "green", color: "black",fontSize: 25}} to="/login">Уввійти</Link>
      ) : (
        <AccountMenu />
      )
      }
    </Box>
  );
};