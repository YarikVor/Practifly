import {Box, Collapse, List, ListItemButton, ListItemIcon, ListItemText, ListSubheader} from "@mui/material";

import {StarBorder} from "@mui/icons-material";
import {useState} from "react";

import {useStyles} from "./styles";

export const ThemeMenu = () => {
  const {classes} = useStyles();

  const [open, setOpen] = useState(true);

  const handleClick = () => {
    setOpen(!open);
  };
  return (
    <Box className={classes.root}>
      <List
        sx={{ width: "100%", maxWidth: 360, bgcolor: "background.paper" }}
        subheader={
          <ListSubheader className={classes.listSubheader}component="div">
              Меню тем
          </ListSubheader>
        }
      >
        <Collapse in={open} timeout="auto" unmountOnExit>
          <List component="div" disablePadding>
            <ListItemButton sx={{ pl: 4 }}>
              <ListItemIcon>
                <StarBorder />
              </ListItemIcon>
              <ListItemText primary="Starred" />
            </ListItemButton>
          </List>
        </Collapse>
      </List>
    </Box>
  );
};

