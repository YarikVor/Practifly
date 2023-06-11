import {
  Avatar,
  Box,
  Button,
  Divider,
  IconButton,
  ListItemIcon,
  Menu,
  MenuItem,
  Tooltip,
  Typography,
} from "@mui/material";
import {useState} from "react";
import {Logout, Settings} from "@mui/icons-material";
import {Link} from "react-router-dom";

import LibraryBooks from "@mui/icons-material/LibraryBooks";

import accountImage from "../../assets/AccountMenu.png";

export const AccountMenu = () => {
  const [anchorEl, setAnchorEl] = useState(null);
  const open = Boolean(anchorEl);

  const handleClick = (event: any) => {
    setAnchorEl(event.currentTarget);
  };
  const handleClose = () => {
    setAnchorEl(null);
  };

  return (
    <Box style={{display: "flex", justifyContent: "space-around", padding: "30px 0 40px 0"}}>
      <Box>МІСЦЕ ДЛЯ ЛОГО</Box>
      <Box>
        <h1>КОРИСТУВАЧ</h1>
      </Box>
      <Box>
        <Tooltip title="Профіль">
          <IconButton
            onClick={handleClick}
            size="small"
            aria-controls={open ? "account-menu" : undefined}
            aria-haspopup="true"
            aria-expanded={open ? "true" : undefined}
          >
            Hi, username
            <Avatar style={{border: "1px solid #9062F2", padding: 1, borderRadius: 99}} src={accountImage}/>
          </IconButton>
        </Tooltip>
      </Box>
      <Menu
        anchorEl={anchorEl}
        id="account-menu"
        open={open}
        onClose={handleClose}
      >
        <MenuItem disableRipple >
          <Box style={{display: "flex", gap: 15}}>
            <Link onClick={() => {setAnchorEl(null);}}  to="/profile">
              <Avatar style={{border: "1px solid #9062F2", padding: 1, borderRadius: 99, width: 65, height: 65}} src={accountImage}/>
            </Link>
            <Box style={{display: "flex", flexDirection: "column", gap: 10}}>
              <Typography>Мій профіль</Typography>
              <Button style={{color: "white", backgroundColor: "#9062F2"}}>Змінити профіль</Button>
            </Box>
          </Box>
        </MenuItem>
        <Divider style={{width: "90%", margin: "0 auto", backgroundColor: "#9062F2"}}/>
        <MenuItem>
          <Link to="/profile/edit">
            <Box style={{display: "flex", alignItems: "center"}}>
              <ListItemIcon>
                <Settings fontSize="small"/>
              </ListItemIcon>
              Налаштування
            </Box>
          </Link>
        </MenuItem>
        <MenuItem>
          <ListItemIcon>
            <Logout fontSize="small"/>
          </ListItemIcon>
                    Вийти
        </MenuItem>
        <MenuItem>
          <ListItemIcon>
            <LibraryBooks fontSize="small"/>
          </ListItemIcon>
          Мої курси
        </MenuItem>
      </Menu>
    </Box>
  );
};